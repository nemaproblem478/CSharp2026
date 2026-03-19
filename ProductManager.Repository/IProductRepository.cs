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
        IEnumerable<ProductDBModel> GetProductsByWarehouse(Guid warehouseId);
        ProductDBModel GetProduct(Guid productId);
        void SaveProduct(ProductDBModel product);
    }
}
