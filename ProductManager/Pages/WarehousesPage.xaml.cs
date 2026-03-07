using ProductManager.Services;
using ProductManager.UIModels;
using System.Collections.ObjectModel;

namespace ProductManager.Pages;

public partial class WarehousesPage : ContentPage
{
	private readonly IWarehouseService _service;
	public ObservableCollection<WarehouseUIModel> Warehouses { get; set; }
	public WarehousesPage(IWarehouseService service)
	{
		InitializeComponent();
		_service = service;

		var data = _service.GetAllWarehousesUI().ToList();

		Warehouses = new ObservableCollection<WarehouseUIModel>(data);

		BindingContext = this;
	}

	private void WarehouseSelected(object sender, SelectionChangedEventArgs e)
	{
		if (e.CurrentSelection.Count > 0)
		{
            var warehouse = (WarehouseUIModel)e.CurrentSelection[0];

            Shell.Current.GoToAsync($"{nameof(WarehouseDetailsPage)}", new Dictionary<string, object> { { "SelectedWarehouse", warehouse } });
        }
	}
	protected override void OnAppearing()
	{
        base.OnAppearing();

		warehouseCollectionView.SelectedItem = null;

        var data = _service.GetAllWarehousesUI().ToList();

		Warehouses.Clear();

        foreach (var warehouse in data)
		{
			Warehouses.Add(warehouse);
		}
    }
}