using ProductManager.Services;
using ProductManager.UIModels;
using System.Collections.ObjectModel;

namespace ProductManager.Pages;

[QueryProperty(nameof(CurrentWarehouse), "SelectedWarehouse")]
public partial class WarehouseDetailsPage : ContentPage
{
	private WarehouseUIModel _currentWarehouse;
    private readonly IProductService _service;
    public WarehouseUIModel CurrentWarehouse
	{
		get => _currentWarehouse;
		set
		{
			_currentWarehouse = value;
			BindingContext = CurrentWarehouse;
		}
	}
	public WarehouseDetailsPage(IProductService service)
	{
		_service = service;
        InitializeComponent();

		
    }
    //Handling selection of a product and navigating into the ProductDetailsPage
    public void ProductSelected(object sender, SelectionChangedEventArgs e)
	{
		var product = (ProductUIModel)e.CurrentSelection[0];
		Shell.Current.GoToAsync($"{nameof(ProductDetailsPage)}", new Dictionary<string, object> { { "SelectedProduct", product } });
	}
	//Handling navigating to ProductCreatePage when clicking on "Create" button
	private void CreateClicked(object sender, EventArgs e)
	{
        Shell.Current.GoToAsync($"{nameof(ProductCreatePage)}", new Dictionary<string, object> { { "CurrentWarehouse", _currentWarehouse.Id } });
    }
    //Handling updating information of all products on appearing
    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (_currentWarehouse != null)
        {
            var data = _service.GetProductsUI(_currentWarehouse.Id).ToList();

			CurrentWarehouse.Products = new ObservableCollection<ProductUIModel>(data);

            var ContextUi = new WarehouseUIModel(_currentWarehouse);
			ContextUi.Products = new ObservableCollection<ProductUIModel>(data);

			BindingContext = ContextUi;
        }
    }
}