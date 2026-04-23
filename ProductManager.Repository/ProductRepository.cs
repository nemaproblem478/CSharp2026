using ProductManager.DBModels;
using ProductManager.DTOModels;
using ProductManager.DTOModels.Product;
using ProductManager.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IStorageContext _storageContext;
        public ProductRepository(IStorageContext storageContext)
        {
            _storageContext = storageContext;
        }
        public Task<IEnumerable<ProductDBModel>> GetProductsByWarehouseAsync(Guid warehouseId)
        {
            return _storageContext.GetProductsByWarehouseAsync(warehouseId);
        }
        public Task<ProductDBModel?> GetProductAsync(Guid productId)
        {
            return _storageContext.GetProductAsync(productId);
        }
        public Task SaveProductAsync(ProductDBModel product)
        {
            return _storageContext.SaveProductAsync(product);
        }
        public Task DeleteProductAsync(Guid productId)
        {
            return _storageContext.DeleteProductAsync(productId);
        }
    }
}
