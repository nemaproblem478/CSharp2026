using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProductManager.DTOModels.Product;
using ProductManager.DTOModels.Warehouse;
using ProductManager.Pages;
using ProductManager.Services;
using System.Collections.ObjectModel;

namespace ProductManager.Viewmodels
{
    public partial class WarehouseDetailsViewModel : ObservableObject, IQueryAttributable
    {
        private readonly IWarehouseService _warehouseService;
        private readonly IProductService _productService;

        [ObservableProperty]
        private WarehouseDetailsDTO _currentWarehouse;
        [ObservableProperty]
        private ObservableCollection<ProductListDTO> _products;

        public WarehouseDetailsViewModel(IWarehouseService warehouseService, IProductService productService)
        {
            _warehouseService = warehouseService;
            _productService = productService;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            var warehouseId = (Guid)query["WarehouseId"];
            CurrentWarehouse = _warehouseService.GetWarehouse(warehouseId);
            Products = new ObservableCollection<ProductListDTO>(_productService.GetProducts(warehouseId));
        }

        //Choosing a Product from WarehouseDetailsPage
        [RelayCommand]
        private void LoadProduct(Guid productId)
        {
            Shell.Current.GoToAsync($"{nameof(ProductDetailsPage)}", new Dictionary<string, object> { { "ProductId", productId } });
        }

        //Add Product
        [RelayCommand]
        private void CreateClicked()
        {
            Shell.Current.GoToAsync($"{nameof(ProductDetailsPage)}", new Dictionary<string, object> { { "WarehouseId", CurrentWarehouse.Id } });
        }
    }
}
