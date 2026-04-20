using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ProductManager.DTOModels.Product;
using ProductManager.DTOModels.Warehouse;
using ProductManager.Messages;
using ProductManager.Pages;
using ProductManager.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.Viewmodels
{
    public partial class WarehousesViewModel : BaseViewModel
    {
        private readonly IWarehouseService _warehouseService;

        private List<WarehouseListDTO> _allWarehouses = new();

        [ObservableProperty]
        private ObservableCollection<WarehouseListDTO> _visibleWarehouses = new();
        [ObservableProperty]
        private WarehouseListDTO _currentWarehouse;

        [ObservableProperty]
        private string _searchQuery;
        public List<string> SortOptions { get; } = new()
        {
            "Name (A-Z)",
            "Name (Z-A)",
            "Lowest Total Cost",
            "Highest Total Cost"
        };

        [ObservableProperty]
        private string _selectedSortOption = "Name (A-Z)";
        public WarehousesViewModel(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        //Load Warehouses on appearing
        [RelayCommand]
        private async Task RefreshData()
        {
            IsBusy = true;
            try
            {
                _allWarehouses.Clear();
                await foreach (var warehouse in _warehouseService.GetAllWarehousesAsync())
                {
                    _allWarehouses.Add(warehouse);
                }
                ApplyFiltersAndSort();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Failed to load departments: {ex.Message}", "Ok");
            }
            finally
            { 
                IsBusy = false; 
            }
        }

        //Choose a Warehouse from WarehousesPage
        [RelayCommand]
        private async Task LoadWarehouse(Guid warehouseId)
        {
            IsBusy = true;
            try
            {
                await Shell.Current.GoToAsync($"{nameof(WarehouseDetailsPage)}", new Dictionary<string, object> { { "WarehouseId", warehouseId } });
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Failed to navigate to warehouse details: {ex.Message}", "Ok");
            }
            finally 
            { 
                IsBusy = false;
            }
        }

        //Add Warehouse
        [RelayCommand]
        private async Task CreateClicked()
        {
            IsBusy = true;
            try
            {
                await Shell.Current.GoToAsync($"{nameof(WarehouseEditorPage)}");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Failed to navigate to warehouse create page: {ex.Message}", "Ok");
            }
            finally
            {
                IsBusy = false;
            }
        }

        //Edit Warehouse
        [RelayCommand]
        private async Task EditWarehouse(Guid warehouseId)
        {
            IsBusy = true;
            try
            {
                await Shell.Current.GoToAsync($"{nameof(WarehouseEditorPage)}", new Dictionary<string, object> { { "WarehouseId", warehouseId } });
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Failed to navigate to warehouse create page: {ex.Message}", "Ok");
            }
            finally
            {
                IsBusy = false;
            }
        }

        //Delete Warehouse
        [RelayCommand]
        private async Task DeleteWarehouse(WarehouseListDTO warehouse)
        {
            IsBusy = true;
            try
            {
                if (await Shell.Current.DisplayAlert("Confirm", "Are you sure you want to delete this product?", "Yes", "No"))
                    await _warehouseService.DeleteWarehouseAsync(warehouse.Id);
                _allWarehouses.Remove(warehouse);
                ApplyFiltersAndSort();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Failed to navigate to product details: {ex.Message}", "Ok");
            }
            finally
            {
                IsBusy = false;
            }
        }

        //Sorts and Filters
        partial void OnSearchQueryChanged(string value) => ApplyFiltersAndSort();
        partial void OnSelectedSortOptionChanged(string value) => ApplyFiltersAndSort();

        private void ApplyFiltersAndSort()
        {
            if (_allWarehouses == null || !_allWarehouses.Any()) return;

            IEnumerable<WarehouseListDTO> filteredList = _allWarehouses;

            if (!string.IsNullOrEmpty(SearchQuery))
            {
                filteredList = filteredList.Where(w => w.Name.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase));
            }

            filteredList = SelectedSortOption switch
            {
                "Name (A-Z)" => filteredList.OrderBy(w => w.Name),
                "Name (Z-A)" => filteredList.OrderByDescending(w => w.Name),
                "Lowest Total Cost" => filteredList.OrderBy(w => w.TotalCost),
                "Highest Total Cost" => filteredList.OrderByDescending(w => w.TotalCost),
            };

            VisibleWarehouses.Clear();
            foreach (var warehouse in filteredList)
            {
                VisibleWarehouses.Add(warehouse);
            }
        }
    }
}
