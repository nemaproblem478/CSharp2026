using ProductManager.DBModels;

namespace ProductManager.Storage
{
    public interface IStorageContext
    {
        IEnumerable<WarehouseDBModel> GetWarehouses();
        WarehouseDBModel GetWarehouse(Guid warehouseId);
        IEnumerable<ProductDBModel> GetProductsByWarehouse(Guid warehouseId);
        ProductDBModel GetProduct(Guid productId);
        void SaveProduct(ProductDBModel newProduct);
        int GetProductsByWarehouseCount(Guid warehouseId);
        double GetWarehouseTotalCost(Guid warehouseId);
    }
}
