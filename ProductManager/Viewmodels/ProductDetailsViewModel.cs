using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProductManager.CommonComponents;
using ProductManager.DTOModels.Product;
using ProductManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.Viewmodels
{
    public partial class ProductDetailsViewModel : ObservableObject, IQueryAttributable
    {
        private readonly IProductService _productService;
        private ProductDetailsDTO? _currentProduct;
        private Guid _warehouseId;
        private Guid _productId;

        public double PriceDouble { get; private set; }
        public int QuantityInt { get; private set; }
        public List<EnumWithName<Category>> Categories { get; }

        [ObservableProperty]
        public string _name;
        [ObservableProperty]
        public string _description;
        [ObservableProperty]
        public Category _selectedCategory;
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(TotalCost))]
        public string _quantityText;
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(TotalCost))]
        public string _priceText;

        public double TotalCost => PriceDouble * QuantityInt;

        public ProductDetailsViewModel(IProductService productService)
        {
            _productService = productService;
            Categories = EnumExtension.GetValuesWithNames<Category>().ToList();
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            _productId = query.ContainsKey("ProductId") ? (Guid)query["ProductId"] : Guid.NewGuid();
            _currentProduct = _productService.GetProduct(_productId);
            _warehouseId = query.ContainsKey("WarehouseId") ? (Guid)query["WarehouseId"] : _currentProduct.WarehouseId;

            Name = _currentProduct?.Name;
            Description = _currentProduct?.Description;
            SelectedCategory = _currentProduct is null ? Category.ElectricGuitar : _currentProduct.Category;
            //_currentProduct is null ? _currentProduct?.Category : CommonComponents.Category.ElectricGuitar
            QuantityText = _currentProduct?.Quantity.ToString();
            PriceText = _currentProduct?.Price.ToString();
        }

        partial void OnPriceTextChanged(string value)
        {
            if (double.TryParse(value, out double parsedValue))
            {
                PriceDouble = parsedValue;
            }
        }

        partial void OnQuantityTextChanged(string value)
        {
            if (int.TryParse(value, out int parsedValue))
            {
                QuantityInt = parsedValue;
            }
        }

        [RelayCommand]
        private void SaveClicked()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                Shell.Current.DisplayAlert("Product hasn't been saved!", "Input a Name.", "Ok ig.");
                return;
            }
            if (string.IsNullOrWhiteSpace(Description))
            {
                Shell.Current.DisplayAlert("Product hasn't been saved!", "Input a Description.", "Ok ig.");
                return;
            }
            if (SelectedCategory == null)
            {
                Shell.Current.DisplayAlert("Product hasn't been saved!", "Choose a Category.", "Ok ig.");
                return;
            }
            if (QuantityText == null)
            {
                Shell.Current.DisplayAlert("Product hasn't been saved!", "Input Quantity.", "Ok ig.");
                return;
            }
            if (PriceText == null)
            {
                Shell.Current.DisplayAlert("Product hasn't been saved!", "Input Price.", "Ok ig.");
                return;
            }
            if (_currentProduct == null)
            {
                _currentProduct = new ProductDetailsDTO(_productId, _warehouseId, Name, Description, SelectedCategory, QuantityInt, PriceDouble);
            } else
            {
                _currentProduct.Name = Name;
                _currentProduct.Description = Description;
                _currentProduct.Category = (Category)SelectedCategory;
                _currentProduct.Quantity = QuantityInt;
                _currentProduct.Price = PriceDouble;
            }

            _productService.SaveProduct(_currentProduct);

            Shell.Current.DisplayAlert("Product has been saved!", $"{Name} has been saved!", "Great!");
            return;
        }
    }
}
