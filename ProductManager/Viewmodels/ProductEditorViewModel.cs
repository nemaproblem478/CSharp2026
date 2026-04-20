using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ProductManager.CommonComponents;
using ProductManager.DTOModels.Product;
using ProductManager.Messages;
using ProductManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ProductManager.Viewmodels
{
    public partial class ProductEditorViewModel : BaseViewModel, IQueryAttributable
    {
        private readonly IProductService _productService;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(DeleteButtonIsVisible))]
        private bool _isEditMode;

        [ObservableProperty]
        private string _pageTitle = "Loading...";

        public bool DeleteButtonIsVisible => IsEditMode;

        private Guid _warehouseId;
        private Guid _productId;

        public double PriceDouble { get; private set; }
        public int QuantityInt { get; private set; }

        public List<EnumWithName<Category>> Categories { get; }

        [ObservableProperty]
        private string _name;
        [ObservableProperty]
        private string _description;
        [ObservableProperty]
        private EnumWithName<Category>? _selectedCategory;
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(TotalCost))]
        public string _quantityText;
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(TotalCost))]
        public string _priceText;

        public double TotalCost => PriceDouble * QuantityInt;

        [ObservableProperty]
        private Dictionary<string, string> _errors;

        public ProductEditorViewModel(IProductService productService)
        {
            _productService = productService;
            Categories = EnumExtension.GetValuesWithNames<Category>().ToList();
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {

            if (query.TryGetValue("ProductId", out var productIdObj))
            {
                IsEditMode = true;
                PageTitle = "Edit Product";
                _productId = (Guid)productIdObj;

                //Load data
                LoadProductAsync(_productId);
            }
            else if (query.TryGetValue("WarehouseId", out var warehouseIdObj))
            {
                IsEditMode = false;
                PageTitle = "Create Product";
                _warehouseId = (Guid)warehouseIdObj;

                //Setup fields
                SetupNewProduct();
            }
        }

        //Load Product data
        private async void LoadProductAsync(Guid id)
        {
            var product = await _productService.GetProductAsync(id);
            _warehouseId = product.WarehouseId;

            //Initialize entries
            Name = product.Name;
            Description = product.Description;
            SelectedCategory = product.Category.GetEnumWithName();
            QuantityText = product.Quantity.ToString();
            PriceText = product.Price.ToString();
        }

        //Setup fields
        private void SetupNewProduct()
        {
            //Setup fields
            Name = string.Empty;
            Description = string.Empty;
            SelectedCategory = null;
            PriceText = string.Empty;
            QuantityText = string.Empty;
        }

        //Price validation
        partial void OnPriceTextChanged(string value)
        {
            //If entry is parseable, then Parse and write to Property
            if (double.TryParse(value, out double parsedValue))
            {
                PriceDouble = parsedValue;
            }
        }

        //Quantity validation
        partial void OnQuantityTextChanged(string value)
        {
            //If entry is parseable, then Parse and write to Property
            if (int.TryParse(value, out int parsedValue))
            {
                QuantityInt = parsedValue;
            }
        }

        [RelayCommand]
        private async Task SaveClicked()
        {
            IsBusy = true;
            var errors =  Validators.ValidateProduct(Name, Description, SelectedCategory?.Value, QuantityText, PriceText);
            Errors = InitErrors();
            if (errors.Count > 0)
            {
                foreach (var error in errors)
                {
                    if (String.IsNullOrWhiteSpace(Errors[error.MemberName]))
                    {
                        Errors[error.MemberName] = error.ErrorMessage;
                        continue;
                    }
                    Errors[error.MemberName] += Environment.NewLine + error.ErrorMessage;
                }
                OnPropertyChanged(nameof(Errors));
                IsBusy = false;
                return;
            }
            try
            {
                if (IsEditMode)
                {
                    var edittedProduct = new ProductDetailsDTO(_productId, _warehouseId, Name, Description, SelectedCategory.Value, QuantityInt, PriceDouble);
                    await _productService.UpdateProductAsync(edittedProduct);
                    WeakReferenceMessenger.Default.Send(new RefreshProductsMessage());
                    await Shell.Current.GoToAsync("..");
                }
                else 
                {
                    var newProduct = new ProductCreateDTO(_warehouseId, Name, Description, SelectedCategory.Value, QuantityInt, PriceDouble);
                    await _productService.CreateProductAsync(newProduct);
                    WeakReferenceMessenger.Default.Send(new RefreshProductsMessage());
                    await Shell.Current.GoToAsync("..");
                }
            }
            catch (Exception ex)
            {
                //Display confirmation message
                await Shell.Current.DisplayAlert("Error", $"Failed to create product: {ex.Message}", "Ok");
            }
            finally
            {
                await Shell.Current.DisplayAlert("Product has been saved!", $"{Name} has been saved!", "Great!");
                IsBusy = false;
            }
        }

        private Dictionary<string, string> InitErrors()
        {
            return new Dictionary<string, string>()
            {
                { nameof(Name), string.Empty },
                { nameof(Description), string.Empty },
                { "Category", string.Empty },
                { nameof(QuantityText), string.Empty },
                { nameof(PriceText), string.Empty }
            };
        }
    }
}
