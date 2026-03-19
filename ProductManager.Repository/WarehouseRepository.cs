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
        public IEnumerable<WarehouseDBModel> GetWarehouses()
        {
            return _storageContext.GetWarehouses();
        }
        public WarehouseDBModel GetWarehouse(Guid warehouseId)
        {
            return _storageContext.GetWarehouse(warehouseId);
        }
        public int GetProductsByWarehouseCount(Guid warehouseId)
        {
            return _storageContext.GetProductsByWarehouseCount(warehouseId);
        }
        public double GetWarehouseTotalCost(Guid warehouseId)
        {
            return _storageContext.GetWarehouseTotalCost(warehouseId);
        }
    }
}
