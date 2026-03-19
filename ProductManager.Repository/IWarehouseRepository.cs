using ProductManager.DBModels;
using ProductManager.DTOModels.Warehouse;

namespace ProductManager.Repository
{
    public interface IWarehouseRepository
    {
        IEnumerable<WarehouseDBModel> GetWarehouses();
        WarehouseDBModel GetWarehouse(Guid warehouseId);
        int GetProductsByWarehouseCount(Guid warehouseId);
        double GetWarehouseTotalCost(Guid warehouseId);
    }
}
