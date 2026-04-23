using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductManager.DBModels;

namespace ProductManager.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDBModel>> GetProductsByWarehouseAsync(Guid warehouseId);
        Task<ProductDBModel?> GetProductAsync(Guid productId);
        Task SaveProductAsync(ProductDBModel product);
        Task DeleteProductAsync(Guid productId);
    }
}
