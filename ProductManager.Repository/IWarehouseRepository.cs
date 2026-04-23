using ProductManager.DBModels;
using ProductManager.DTOModels.Warehouse;

namespace ProductManager.Repository
{
    public interface IWarehouseRepository
    {
        IAsyncEnumerable<WarehouseDBModel> GetWarehousesAsync();
        Task<WarehouseDBModel?> GetWarehouseAsync(Guid warehouseId);
        Task SaveWarehouseAsync(WarehouseDBModel warehouse);
        Task DeleteWarehouseAsync(Guid warehouseId);
        Task<int> GetProductsByWarehouseCountAsync(Guid warehouseId);
        Task<double> GetWarehouseTotalCostAsync(Guid warehouseId);
    }
}
