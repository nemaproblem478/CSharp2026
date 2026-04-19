using Microsoft.Extensions.Logging;
using ProductManager.Pages;
using ProductManager.Services;
using ProductManager.Repository;
using ProductManager.Storage;
using ProductManager.Viewmodels;
using CommunityToolkit.Maui;

namespace ProductManager
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiCommunityToolkit();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<IStorageContext, FileStorageContext>();
            builder.Services.AddSingleton<IWarehouseRepository, WarehouseRepository>();
            builder.Services.AddSingleton<IProductRepository, ProductRepository>();
            builder.Services.AddSingleton<IWarehouseService, WarehouseService>();
            builder.Services.AddSingleton<IProductService, ProductService>();

            builder.Services.AddSingleton<WarehousesPage>();
            builder.Services.AddTransient<WarehouseEditorPage>();
            builder.Services.AddTransient<WarehouseDetailsPage>();
            builder.Services.AddTransient<ProductEditorPage>();

            builder.Services.AddSingleton<WarehousesViewModel>();
            builder.Services.AddTransient<WarehouseEditorViewModel>();
            builder.Services.AddTransient<WarehouseDetailsViewModel>();
            builder.Services.AddTransient<ProductEditorViewModel>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
