using ProductManager.CommonComponents;
using ProductManager.Services;
using ProductManager.Viewmodels;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ProductManager.Pages;
public partial class ProductDetailsPage : ContentPage
{
	public ProductDetailsPage(ProductDetailsViewModel vms)
	{
		InitializeComponent();
        BindingContext = vms;
    }
}