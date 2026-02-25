using ProductManager.CommonComponents;
using ProductManager.Services;
using ProductManager.UIModels;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ProductManager.Pages;

public partial class ProductCreatePage : ContentPage
{
    private readonly IProductService _productService;
    public ProductCreatePage(IProductService productService)
	{
		InitializeComponent();
        _productService = productService;
		pCategory.ItemsSource = EnumExtension.GetValuesWithNames<Category>();
		
	}
	private void BackClicked(object sender, EventArgs e)
	{

	}
	private void SaveClicked(object sender, EventArgs e)
	{
		if (String.IsNullOrWhiteSpace(eName.Text))
        {
            DisplayAlert("Invalid data!", "Name of the product can't be empty", "Ok");
            return;
        }
        if (String.IsNullOrWhiteSpace(eDescription.Text))
        {
            DisplayAlert("Invalid data!", "Description of the product can't be empty", "Ok");
            return;
        }
        if (pCategory.SelectedItem == null)
        {
            DisplayAlert("Invalid data!", "Category of the product can't be empty", "Ok");
            return;
        }
        if (String.IsNullOrWhiteSpace(eQuantity.Text))
        {
            DisplayAlert("Invalid data!", "Quantity of the product can't be empty", "Ok");
            return;
        }
        if (String.IsNullOrWhiteSpace(ePrice.Text))
        {
            DisplayAlert("Invalid data!", "Price of the product can't be empty", "Ok");
            return;
        }
        var newProduct = new ProductUIModel(Guid.Empty);
        newProduct.Name = eName.Text;
        newProduct.Quantity = int.Parse(eQuantity.Text);
        newProduct.Price = double.Parse(ePrice.Text);
        newProduct.Category = ((EnumWithName<Category>)pCategory.SelectedItem).Value;
        newProduct.Description = eDescription.Text;
        _productService.SaveProduct(newProduct);
        DisplayAlert("Product Created!", $"{newProduct.Category.GetDisplayName()} {newProduct.Name} has been succesfully created!", "Great!");
    }

    private void OnQuantityEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrEmpty(e.NewTextValue))
            return;

        if (!e.NewTextValue.All(char.IsDigit))
        {
            ((Entry)sender).Text = e.OldTextValue;
        }
    }
    private void OnPriceEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrEmpty(e.NewTextValue))
            return;

        if (!Regex.IsMatch(e.NewTextValue, @"^[0-9]+[\.,]?[0-9]*$"))
        {
            ((Entry)sender).Text = e.OldTextValue;
        }
    }
}