using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProductManager.DTOModels.Warehouse;
using ProductManager.Pages;
using ProductManager.Services;

namespace ProductManager.Viewmodels
{
    public partial class WarehousesViewModel : ObservableObject
    {
        private readonly IWarehouseService _warehouseService;

        [ObservableProperty]
        private ObservableCollection<WarehouseListDTO> _warehouses;
        public WarehousesViewModel(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
            Warehouses = new ObservableCollection<WarehouseListDTO>(_warehouseService.GetAllWarehouses());
        }

        //Load Warehouses on appearing
        [RelayCommand]
        public void LoadData()
        {
            Warehouses = new ObservableCollection<WarehouseListDTO>(_warehouseService.GetAllWarehouses());
        }

        //Choosing a Warehouse from WarehousesPage
        [RelayCommand]
        private void LoadWarehouse(Guid warehouseId)
        {
            Shell.Current.GoToAsync($"{nameof(WarehouseDetailsPage)}", new Dictionary<string, object> { { "WarehouseId", warehouseId } });
        }
    }
}
