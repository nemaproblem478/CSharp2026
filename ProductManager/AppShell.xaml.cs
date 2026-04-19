using ProductManager.Pages;

namespace ProductManager
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute($"{nameof(WarehousesPage)}/{nameof(WarehouseDetailsPage)}", typeof(WarehouseDetailsPage));
            Routing.RegisterRoute($"{nameof(WarehousesPage)}/{nameof(WarehouseEditorPage)}", typeof(WarehouseEditorPage));
            Routing.RegisterRoute($"{nameof(WarehousesPage)}/{nameof(WarehouseDetailsPage)}/{nameof(ProductEditorPage)}", typeof(ProductEditorPage));
        }
    }
}
