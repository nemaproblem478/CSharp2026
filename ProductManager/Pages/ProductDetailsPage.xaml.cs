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
        pCategory.ItemsSource = EnumExtension.GetValuesWithNames<Category>(); //Initializing Category picker
    }
    //Handling saving product when pressing "Save Changes" button. Making sure there are no empty entries/picker
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
        if (pCategory.SelectedItem is EnumWithName<Category> wrappedCategory)
        {
            CurrentProduct.Category = wrappedCategory.Value;
        }
        _service.SaveProduct(CurrentProduct);
        DisplayAlert("Changes Saved!", $"{CurrentProduct.Name} has been succesfully saved!", "Great!");
    }
    //Handling Quntity entry whenever it's content changes. Making sure only numbers are entered
    private void OnQuantityEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrEmpty(e.NewTextValue))
            return;

        if (!e.NewTextValue.All(char.IsDigit))
        {
            ((Entry)sender).Text = e.OldTextValue;
        }
    }
    //Handling Price entry whenever it's content changes. Making sure only numbers and up to one "," are entered
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