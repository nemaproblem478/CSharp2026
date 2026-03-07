using ProductManager.DBModels;

namespace ProductManager.Storage
{
    public interface IStorageContext
    {
        IEnumerable<WarehouseDBModel> GetWarehouses();
        IEnumerable<ProductDBModel> GetProductsByWarehouse(Guid departmentId);
        int GetProductsByWarehouseCount(Guid id);
    }
}
