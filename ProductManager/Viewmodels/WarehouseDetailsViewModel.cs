using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ProductManager.DTOModels.Product;
using ProductManager.DTOModels.Warehouse;
using ProductManager.Messages;
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
        private Guid _warehouseId;
        private List<ProductListDTO> _allProducts = new();

        [ObservableProperty]
        private WarehouseDetailsDTO _currentWarehouse;
        [ObservableProperty]
        private ObservableCollection<ProductListDTO> _visibleProducts = new();

        [ObservableProperty]
        private string _searchQuery;
        public List<string> SortOptions { get; } = new()
        {
            "Name (A-Z)",
            "Name (Z-A)",
            "Lowest Price",
            "Highest Price"
        };

        [ObservableProperty]
        private string _selectedSortOption;

        public WarehouseDetailsViewModel(IWarehouseService warehouseService, IProductService productService)
        {
            _warehouseService = warehouseService;
            _productService = productService;
            WeakReferenceMessenger.Default.Register<RefreshProductsMessage>(this, async (r, m) =>
            {
                await RefreshData();
            });
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
                foreach (var product in await _productsTask)
                {
                    _allProducts.Add(product);
                }
                SelectedSortOption = "Name (A-Z)";
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

                _allProducts.Clear();
                foreach (var product in freshProducts)
                {
                    _allProducts.Add(product);
                }

                ApplyFiltersAndSort();
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
                _allProducts.Remove(product);
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

        //Sorts and Filters
        partial void OnSearchQueryChanged(string value) => ApplyFiltersAndSort();
        partial void OnSelectedSortOptionChanged(string value) => ApplyFiltersAndSort();

        private void ApplyFiltersAndSort()
        {
            if (_allProducts == null || !_allProducts.Any()) return;

            IEnumerable<ProductListDTO> filteredList = _allProducts;

            if (!string.IsNullOrEmpty(SearchQuery))
            {
                filteredList = filteredList.Where(p => p.Name.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase));
            }

            filteredList = SelectedSortOption switch
            {
                "Name (A-Z)" => filteredList.OrderBy(p => p.Name),
                "Name (Z-A)" => filteredList.OrderByDescending(p => p.Name),
                "Lowest Price" => filteredList.OrderBy(p => p.Price),
                "Highest Price" => filteredList.OrderByDescending(p => p.Price),
            };

            VisibleProducts.Clear();
            foreach (var product in filteredList)
            {
                VisibleProducts.Add(product);
            }
        }
    }
}
