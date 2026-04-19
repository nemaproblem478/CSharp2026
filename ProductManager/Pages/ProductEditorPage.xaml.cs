using ProductManager.CommonComponents;
using ProductManager.Services;
using ProductManager.Viewmodels;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ProductManager.Pages;
public partial class ProductEditorPage : ContentPage
{
	public ProductEditorPage(ProductEditorViewModel vms)
	{
		InitializeComponent();
        BindingContext = vms;
    }
}