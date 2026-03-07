using ProductManager.Pages;

namespace ProductManager
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute($"{nameof(WarehousesPage)}/{nameof(WarehouseDetailsPage)}", typeof(WarehouseDetailsPage));
            Routing.RegisterRoute($"{nameof(WarehousesPage)}/{nameof(WarehouseDetailsPage)}/{nameof(ProductDetailsPage)}", typeof(ProductDetailsPage));
            Routing.RegisterRoute($"{nameof(WarehousesPage)}/{nameof(WarehouseDetailsPage)}/{nameof(ProductDetailsPage)}/{nameof(ProductCreatePage)}", typeof(ProductCreatePage));
        }
    }
}
