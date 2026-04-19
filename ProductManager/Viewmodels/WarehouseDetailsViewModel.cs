using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProductManager.DTOModels.Product;
using ProductManager.DTOModels.Warehouse;
using ProductManager.Pages;
using ProductManager.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ProductManager.Viewmodels
{
    public partial class WarehouseDetailsViewModel : BaseViewModel, IQueryAttributable
    {
        private readonly IWarehouseService _warehouseService;
        private readonly IProductService _productService;

        private Task<WarehouseDetailsDTO> _detailsTask;
        private Task<IEnumerable<ProductListDTO>> _productsTask;

        [ObservableProperty]
        private WarehouseDetailsDTO _currentWarehouse;
        [ObservableProperty]
        private ObservableCollection<ProductListDTO> _products;

        private Guid _warehouseId;

        public WarehouseDetailsViewModel(IWarehouseService warehouseService, IProductService productService)
        {
            _warehouseService = warehouseService;
            _productService = productService;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            _warehouseId = (Guid)query["WarehouseId"];

            _detailsTask = _warehouseService.GetWarehouseAsync(_warehouseId);
            _productsTask = _productService.GetProductsByWarehouseAsync(_warehouseId);

            InitializeDataAsync();
        }

        //Initialize data
        private async void InitializeDataAsync()
        {
            IsBusy = true;
            try
            {
                CurrentWarehouse = await _detailsTask ?? throw new Exception("Warehouse does not exist.");
                Products = new ObservableCollection<ProductListDTO>(await _productsTask);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Failed to load data: {ex.Message}", "Ok");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        internal async Task RefreshData()
        {
            IsBusy = true;
            try
            {
                CurrentWarehouse = await _warehouseService.GetWarehouseAsync(_warehouseId) ?? throw new Exception("Warehouse does not exist.");

                var freshProducts = await _productService.GetProductsByWarehouseAsync(_warehouseId);

                Products.Clear();
                foreach (var product in freshProducts)
                {
                    Products.Add(product);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Failed to refresh: {ex.Message}", "Ok");
            }
            finally
            {
                IsBusy = false;
            }
        }

        //Choosing a Product from WarehouseDetailsPage
        [RelayCommand]
        private async Task LoadProduct(Guid productId)
        {
            IsBusy = true;
            try
            {
                await Shell.Current.GoToAsync($"{nameof(ProductEditorPage)}", new Dictionary<string, object> { { "ProductId", productId } });
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

        [RelayCommand]
        private async Task DeleteProduct(ProductListDTO product)
        {
            IsBusy = true;
            try
            {
                if (await Shell.Current.DisplayAlert("Confirm", "Are you sure you want to delete this product?", "Yes", "No"))
                    await _productService.DeleteProductAsync(product.Id);
                Products.Remove(product);
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

        //Add Product
        [RelayCommand]
        private async Task CreateClicked()
        {
            IsBusy = true;
            try
            {
                await Shell.Current.GoToAsync($"{nameof(ProductEditorPage)}", new Dictionary<string, object> { { "WarehouseId", CurrentWarehouse.Id } });
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Failed to navigate to product create page: {ex.Message}", "Ok");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
