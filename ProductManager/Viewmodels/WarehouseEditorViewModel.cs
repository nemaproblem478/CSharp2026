using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProductManager.CommonComponents;
using ProductManager.DTOModels.Product;
using ProductManager.DTOModels;
using ProductManager.Repository;
using ProductManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductManager.DTOModels.Warehouse;

namespace ProductManager.Viewmodels
{
    public partial class WarehouseEditorViewModel : BaseViewModel, IQueryAttributable
    {
        private readonly IWarehouseService _warehouseService;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(ProductCountAndTotalCostIsVisible))]
        private bool _isEditMode;
        [ObservableProperty]
        private string _pageTitle = "Loading...";

        public bool ProductCountAndTotalCostIsVisible => IsEditMode;

        private Guid _warehouseId;

        public List<EnumWithName<CommonComponents.Location>> Locations { get; }

        [ObservableProperty]
        public string _name;
        [ObservableProperty]
        public EnumWithName<CommonComponents.Location>? _selectedLocation;

        [ObservableProperty]
        public int _productCount;
        [ObservableProperty]
        public double _totalCost;

        [ObservableProperty]
        private Dictionary<string, string> _errors;

        public WarehouseEditorViewModel(IWarehouseService warehouseService) 
        {
            _warehouseService = warehouseService;
            Locations = EnumExtension.GetValuesWithNames<CommonComponents.Location>().ToList();
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("WarehouseId", out var warehouseIdObj))
            {
                IsEditMode = true;
                PageTitle = "Edit Warehouse";
                _warehouseId = (Guid)warehouseIdObj;

                LoadWarehouseAsync(_warehouseId);
            }
            else
            {
                IsEditMode = false;
                PageTitle = "Create Warehouse";

                SetupNewWarehouse();
            }
        }

        //Load Warehouse data
        private async void LoadWarehouseAsync(Guid id)
        {
            var warehouse = await _warehouseService.GetWarehouseAsync(id);

            //Initialize entries
            Name = warehouse.Name;
            SelectedLocation = warehouse.Location.GetEnumWithName();
            ProductCount = warehouse.ProductCount;
            TotalCost = warehouse.TotalCost;
        }

        //Setup fields
        private void SetupNewWarehouse()
        {
            //Setup fields
            Name = string.Empty;
            SelectedLocation = null;
            ProductCount = 0;
            TotalCost = 0;
        }

        [RelayCommand]
        private async Task SaveClicked()
        {
            IsBusy = true;
            var errors = Validators.ValidateWarehouse(Name, SelectedLocation?.Value);
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
                    var edittedWarehouse = new WarehouseDetailsDTO(_warehouseId, Name, SelectedLocation.Value, TotalCost, ProductCount);
                    await _warehouseService.UpdateWarehouseAsync(edittedWarehouse);
                    await Shell.Current.GoToAsync("..");
                }
                else
                {
                    var newWarehouse = new WarehouseCreateDTO(Name, SelectedLocation.Value);
                    await _warehouseService.CreateWarehouseAsync(newWarehouse);
                    await Shell.Current.GoToAsync("..");
                }
            }
            catch (Exception ex)
            {
                
                await Shell.Current.DisplayAlert("Error", $"Failed to create warehouse: {ex.Message}", "Ok");
            }
            finally
            {
                //Display confirmation message
                await Shell.Current.DisplayAlert("Warehouse has been saved!", $"{Name} has been saved!", "Great!");
                IsBusy = false;
            }
        }

        private Dictionary<string, string> InitErrors()
        {
            return new Dictionary<string, string>()
            {
                { nameof(Name), string.Empty },
                { "Location", string.Empty }
            };
        }
    }
}
