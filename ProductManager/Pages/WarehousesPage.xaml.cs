using ProductManager.Services;
using ProductManager.Viewmodels;
using System.Collections.ObjectModel;

namespace ProductManager.Pages;

public partial class WarehousesPage : ContentPage
{
    private readonly WarehousesViewModel _viewModel;
    public WarehousesPage(WarehousesViewModel vm)
	{
        InitializeComponent();
        BindingContext = _viewModel = vm;
	}

    //override protected async void OnAppearing()
    //{
    //    await _viewModel.RefreshData();
    //}
}