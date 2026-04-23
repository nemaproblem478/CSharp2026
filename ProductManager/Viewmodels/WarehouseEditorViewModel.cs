using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ProductManager.CommonComponents;
using ProductManager.DTOModels;
using ProductManager.DTOModels.Product;
using ProductManager.DTOModels.Warehouse;
using ProductManager.Messages;
using ProductManager.Repository;
using ProductManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.Viewmodels
{
    public partial class WarehouseEditorViewModel : BaseViewModel, IQueryAttributable
    {
        private readonly IWarehouseService _warehouseService;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(ProductCountAndTotalCostIsVisible))]
        public partial bool IsEditMode { get; set; }
        [ObservableProperty]
        public partial string PageTitle { get; set; } = "Loading...";

        public bool ProductCountAndTotalCostIsVisible => IsEditMode;

        private Guid _warehouseId;

        public List<EnumWithName<CommonComponents.Location>> Locations { get; } = [.. EnumExtension.GetValuesWithNames<CommonComponents.Location>()];

        [ObservableProperty]
        public partial string Name { get; set; } = string.Empty;
        [ObservableProperty]
        public partial EnumWithName<CommonComponents.Location>? SelectedLocation { get; set; }

        [ObservableProperty]
        public partial int ProductCount { get; set; }
        [ObservableProperty]
        public partial double TotalCost { get; set; }

        [ObservableProperty]
        public partial Dictionary<string, string> Errors { get; set; } = InitErrors();

        public WarehouseEditorViewModel(IWarehouseService warehouseService) 
        {
            _warehouseService = warehouseService;
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

            if (warehouse is null)
            {
                await Shell.Current.DisplayAlert("Error Loading Warehouse", "Warehouse not found. It might have been deleted.", "Ok");

                await Shell.Current.GoToAsync("..");
                return;
            }
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
                    var edittedWarehouse = new WarehouseDetailsDTO(_warehouseId, Name, SelectedLocation!.Value, TotalCost, ProductCount);
                    await _warehouseService.UpdateWarehouseAsync(edittedWarehouse);
                }
                else
                {
                    var newWarehouse = new WarehouseCreateDTO(Name, SelectedLocation!.Value);
                    await _warehouseService.CreateWarehouseAsync(newWarehouse);
                }
                await Shell.Current.DisplayAlert("Warehouse has been saved!", $"{Name} has been saved!", "Great!");
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                
                await Shell.Current.DisplayAlert("Error", $"Failed to create warehouse: {ex.Message}", "Ok");
            }
            finally
            {
                //Display confirmation message
                IsBusy = false;
            }
        }

        private static Dictionary<string, string> InitErrors() => new()
        {
            { nameof(Name), string.Empty },
            { "Location", string.Empty }
        };
        
    }
}
