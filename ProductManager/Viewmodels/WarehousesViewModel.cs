using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProductManager.DTOModels.Product;
using ProductManager.DTOModels.Warehouse;
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

        [ObservableProperty]
        private ObservableCollection<WarehouseListDTO> _warehouses;
        [ObservableProperty]
        private WarehouseListDTO _currentWarehouse;
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
                Warehouses = new ObservableCollection<WarehouseListDTO>();
                await foreach (var warehouse in _warehouseService.GetAllWarehousesAsync())
                {
                    Warehouses.Add(warehouse);
                }
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
                Warehouses.Remove(warehouse);
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
    }
}
