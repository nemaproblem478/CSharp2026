using Microsoft.Maui.Storage;
using System;
using System.Text.Json;
using ProductManager.DBModels;

namespace ProductManager.Storage
{
    public class FileStorageContext : IStorageContext
    {
        private static readonly string DatabasePath = Path.Combine(FileSystem.AppDataDirectory, "File storage");

        private string WarehouseFilePath(Guid warehouseId)
        {
            return Path.Combine(DatabasePath, warehouseId.ToString() + ".json");
        }
        private string WarehouseDirectoryPath(Guid warehouseId)
        {
            return Path.Combine(DatabasePath, warehouseId.ToString());
        }
        private string ProductFilePath(Guid warehouseId, Guid productId)
        {
            return ProductFilePath(WarehouseDirectoryPath(warehouseId), productId);
        }
        private string ProductFilePath(string warehouseFolderPath, Guid productId)
        {
            return Path.Combine(warehouseFolderPath, productId.ToString() + ".json");
        }

        private async Task Init()
        {
            if (!Directory.Exists(DatabasePath))
                await CreateMockStorage();
        }

        private async Task CreateMockStorage()
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

        public async Task<WarehouseDBModel> GetWarehouseAsync(Guid warehouseId)
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
                yield return JsonSerializer.Deserialize<WarehouseDBModel>(json);
            }
        }

        public async Task<ProductDBModel> GetProductAsync(Guid productId)
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
                products.Add(JsonSerializer.Deserialize<ProductDBModel>(json));
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
    }
}
