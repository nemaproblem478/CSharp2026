using ProductManager.Viewmodels;

namespace ProductManager.Pages;

public partial class WarehouseEditorPage : ContentPage
{
	public WarehouseEditorPage(WarehouseEditorViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}