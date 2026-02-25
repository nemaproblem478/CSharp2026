using ProductManager.Pages;

namespace ProductManager
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ProductCreatePage), typeof(ProductCreatePage));
        }
    }
}
