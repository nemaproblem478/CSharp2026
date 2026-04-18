using ProductManager.DBModels;
using ProductManager.DTOModels.Warehouse;
using ProductManager.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.Repository
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly IStorageContext _storageContext;
        public WarehouseRepository(IStorageContext storageContext)
        {
            _storageContext = storageContext; ;
        }
        public IAsyncEnumerable<WarehouseDBModel> GetWarehousesAsync()
        {
            return _storageContext.GetWarehousesAsync();
        }
        public Task<WarehouseDBModel> GetWarehouseAsync(Guid warehouseId)
        {
            return _storageContext.GetWarehouseAsync(warehouseId);
        }
        public Task SaveWarehouseAsync(WarehouseDBModel warehouse)
        {
            return _storageContext.SaveWarehouseAsync(warehouse);
        }
        public Task DeleteWarehouseAsync(Guid warehouseId)
        {
            return _storageContext.DeleteWarehouseAsync(warehouseId);
        }
        public Task<int> GetProductsByWarehouseCountAsync(Guid warehouseId)
        {
            return _storageContext.GetProductsByWarehouseCountAsync(warehouseId);
        }
        public Task<double> GetWarehouseTotalCostAsync(Guid warehouseId)
        {
            return _storageContext.GetWarehouseTotalCostAsync(warehouseId);
        }
    }
}
