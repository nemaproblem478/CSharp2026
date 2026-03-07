using ProductManager.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductManager.DBModels;

namespace ProductManager.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IStorageContext _storageContext;
        public ProductRepository(IStorageContext storageContext)
        {
            _storageContext = storageContext;
        }
        public IEnumerable<ProductDBModel> GetProductsByWarehouse(Guid warehouseId)
        {
            return _storageContext.GetProductsByWarehouse(warehouseId);
        }
        public int GetProductsByWarehouseCount(Guid warehouseId)
        {
            return _storageContext.GetProductsByWarehouseCount(warehouseId);
        }
    }
}
