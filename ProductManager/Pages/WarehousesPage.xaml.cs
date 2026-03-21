using ProductManager.Services;
using ProductManager.Viewmodels;
using System.Collections.ObjectModel;

namespace ProductManager.Pages;

public partial class WarehousesPage : ContentPage
{
	public WarehousesPage(WarehousesViewModel vm)
	{
        InitializeComponent();
        BindingContext = vm;
	}
}