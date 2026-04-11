using ProductManager.DBModels;

namespace ProductManager.Storage
{
    public interface IStorageContext
    {
        IAsyncEnumerable<WarehouseDBModel> GetWarehousesAsync();
        Task<WarehouseDBModel> GetWarehouseAsync(Guid warehouseId);
        Task<IEnumerable<ProductDBModel>> GetProductsByWarehouseAsync(Guid warehouseId);
        Task<ProductDBModel> GetProductAsync(Guid productId);
        Task SaveProductAsync(ProductDBModel newProduct);
        Task<int> GetProductsByWarehouseCountAsync(Guid warehouseId);
        Task<double> GetWarehouseTotalCostAsync(Guid warehouseId);
    }
}
