using ProductManager.Services;
using ProductManager.UIModels;
using ProductManager.Viewmodels;
using System.Collections.ObjectModel;

namespace ProductManager.Pages;
public partial class WarehouseDetailsPage : ContentPage
{
	public WarehouseDetailsPage(WarehouseDetailsViewModel vm)
	{
        InitializeComponent();
        BindingContext = vm;
    }
}