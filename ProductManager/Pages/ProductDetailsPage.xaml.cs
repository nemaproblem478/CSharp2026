using ProductManager.CommonComponents;
using ProductManager.Services;
using ProductManager.UIModels;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ProductManager.Pages;

[QueryProperty(nameof(CurrentProduct), "SelectedProduct")]
public partial class ProductDetailsPage : ContentPage
{
	private ProductUIModel _currentProduct;
    private readonly IProductService _service;

	public ProductUIModel CurrentProduct
	{
		get => _currentProduct;
		set
		{
			_currentProduct = value;
			BindingContext = CurrentProduct;
		}
	}
	public ProductDetailsPage(IProductService service)
	{
		InitializeComponent();
        _service = service;
        pCategory.ItemsSource = EnumExtension.GetValuesWithNames<Category>();
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
        if (!String.IsNullOrWhiteSpace(ePrice.Text))
        {
            CurrentProduct.Price = double.Parse(ePrice.Text);
            
        }
        //var changedProduct = new ProductUIModel(_currentProduct);
        //changedProduct.Name = eName.Text;
        //changedProduct.Quantity = int.Parse(eQuantity.Text);
        //changedProduct.Category = ((EnumWithName<Category>)pCategory.SelectedItem).Value;
        //changedProduct.Description = eDescription.Text;
        if (pCategory.SelectedItem is EnumWithName<Category> wrappedCategory)
        {
            CurrentProduct.Category = wrappedCategory.Value;
        }
        _service.SaveProduct(CurrentProduct);
        DisplayAlert("Changes Saved!", $"{CurrentProduct.Name} has been succesfully saved!", "Great!");
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

        if (!Regex.IsMatch(e.NewTextValue, @"^[0-9]+[\,]?[0-9]*$"))
        {
            ((Entry)sender).Text = e.OldTextValue;
        }
    }
}