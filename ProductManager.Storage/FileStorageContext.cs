using Microsoft.Maui.Storage;
using System;
using System.Text.Json;
using ProductManager.DBModels;

namespace ProductManager.Storage
{
    public class FileStorageContext : IStorageContext
    {
        private static readonly string DatabasePath = Path.Combine(FileSystem.AppDataDirectory, "File storage");

        private static string WarehouseFilePath(Guid warehouseId)
        {
            return Path.Combine(DatabasePath, warehouseId.ToString() + ".json");
        }
        private static string WarehouseDirectoryPath(Guid warehouseId)
        {
            return Path.Combine(DatabasePath, warehouseId.ToString());
        }
        private static string ProductFilePath(Guid warehouseId, Guid productId)
        {
            return ProductFilePath(WarehouseDirectoryPath(warehouseId), productId);
        }
        private static string ProductFilePath(string warehouseFolderPath, Guid productId)
        {
            return Path.Combine(warehouseFolderPath, productId.ToString() + ".json");
        }

        private static async Task Init()
        {
            if (!Directory.Exists(DatabasePath))
                await CreateMockStorage();
        }

        private static async Task CreateMockStorage()
        {
            Directory.CreateDirectory(DatabasePath);
            var inMemoryStorage = new InMemoryStorageContext();
            var tasks = new List<Task>();
            await foreach (var warehouse in inMemoryStorage.GetWarehousesAsync())
            {
                Directory.CreateDirectory(Path.Combine(DatabasePath, warehouse.Id.ToString()));
                tasks.Add(File.WriteAllTextAsync(WarehouseFilePath(warehouse.Id), JsonSerializer.Serialize(warehouse)));
                foreach (var product in await inMemoryStorage.GetProductsByWarehouseAsync(warehouse.Id))
                {
                    tasks.Add(File.WriteAllTextAsync(ProductFilePath(warehouse.Id, product.ProductId), JsonSerializer.Serialize(product)));
                }
            }
        }

        public async Task<WarehouseDBModel?> GetWarehouseAsync(Guid warehouseId)
        {
            await Init();
            var filePath = WarehouseFilePath(warehouseId);
            if (!File.Exists(filePath))
                return null;
            var json = await File.ReadAllTextAsync(filePath);
            return JsonSerializer.Deserialize<WarehouseDBModel>(json);
        }

        public async IAsyncEnumerable<WarehouseDBModel> GetWarehousesAsync()
        {
            await Init();
            foreach (var file in Directory.GetFiles(DatabasePath, "*.json"))
            {
                var json = await File.ReadAllTextAsync(file);
                var product = JsonSerializer.Deserialize<WarehouseDBModel>(json);
                if (product != null)
                {
                    yield return product;
                }
            }
        }

        public async Task<ProductDBModel?> GetProductAsync(Guid productId)
        {
            await Init();
            foreach (var directory in Directory.GetDirectories(DatabasePath))
            {
                var filePath = ProductFilePath(directory, productId);
                if (!File.Exists(filePath))
                    continue;
                var json = await File.ReadAllTextAsync(filePath);
                return JsonSerializer.Deserialize<ProductDBModel>(json);
            }
            return null;
        }

        public async Task<IEnumerable<ProductDBModel>> GetProductsByWarehouseAsync(Guid warehouseId)
        {
            await Init();
            var products = new List<ProductDBModel>();
            var warehouseDirectory = WarehouseDirectoryPath(warehouseId);
            if (!Directory.Exists(warehouseDirectory))
                return products;
            foreach (var file in Directory.GetFiles(warehouseDirectory, "*.json"))
            {
                var json = await File.ReadAllTextAsync(file);
                var product = JsonSerializer.Deserialize<ProductDBModel>(json);
                if (product != null)
                {
                    products.Add(product);
                }
            }
            return products;
        }

        public async Task<int> GetProductsByWarehouseCountAsync(Guid warehouseId)
        {
            await Init();
            var warehouseDirectory = WarehouseDirectoryPath(warehouseId);
            if (!Directory.Exists(warehouseDirectory))
                return 0;
            return Directory.GetFiles(warehouseDirectory).Length;
        }

        public async Task<double> GetWarehouseTotalCostAsync(Guid warehouseId)
        {
            await Init();
            var products = await GetProductsByWarehouseAsync(warehouseId);
            return products.Sum(p => p.Price * p.Quantity);
        }

        public async Task SaveProductAsync(ProductDBModel product)
        {
            await Init();
            var warehouseDirectory = WarehouseDirectoryPath(product.WarehouseId);
            if (!Directory.Exists(warehouseDirectory))
                Directory.CreateDirectory(warehouseDirectory);
            var filePath = ProductFilePath(warehouseDirectory, product.ProductId);
            await File.WriteAllTextAsync(filePath, JsonSerializer.Serialize(product));
        }

        public async Task DeleteProductAsync(Guid productId)
        {
            await Init();
            foreach (var directory in Directory.GetDirectories(DatabasePath))
            {
                var filePath = ProductFilePath(directory, productId);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return;
                }
            }
        }
        public async Task SaveWarehouseAsync(WarehouseDBModel warehouse)
        {
            await Init();
            var filePath = WarehouseFilePath(warehouse.Id);
            await File.WriteAllTextAsync(filePath, JsonSerializer.Serialize(warehouse));
        }
        public async Task DeleteWarehouseAsync(Guid warehouseId)
        {
            await Init();
            var filePath = WarehouseFilePath(warehouseId);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            var directoryPath = WarehouseDirectoryPath(warehouseId);
            if (Directory.Exists(directoryPath))
            {
                Directory.Delete(directoryPath, true);
            }
        }
    }
}