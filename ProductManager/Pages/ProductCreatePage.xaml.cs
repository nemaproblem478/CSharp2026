using ProductManager.CommonComponents;
using ProductManager.Services;
using ProductManager.UIModels;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ProductManager.Pages;

[QueryProperty(nameof(CurrentWarehouseId), "CurrentWarehouse")]
public partial class ProductCreatePage : ContentPage
{
    private Guid _currentWarehouseId;
    public Guid CurrentWarehouseId
    {
        get => _currentWarehouseId;
        set => _currentWarehouseId = value;
    }
    private readonly IProductService _service;
    public ProductCreatePage(IProductService service)
	{
		InitializeComponent();
        _service = service;
		pCategory.ItemsSource = EnumExtension.GetValuesWithNames<Category>(); //Initializing Category picker
		
	}
    //Handling saving product ui to storage whenever clicking "Save" button. Making sure no entries/picker are empty
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
        //Creating new ProductUIModel based on entries
        var newProduct = new ProductUIModel(_currentWarehouseId);
        newProduct.Name = eName.Text;
        newProduct.Quantity = int.Parse(eQuantity.Text);
        newProduct.Price = double.Parse(ePrice.Text);
        newProduct.Category = ((EnumWithName<Category>)pCategory.SelectedItem).Value;
        newProduct.Description = eDescription.Text;
        //Saving UI Model
        _service.SaveProduct(newProduct);
        
        DisplayAlert("Product Created!", $"{newProduct.Category.GetDisplayName()} {newProduct.Name} has been succesfully created!", "Great!");
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