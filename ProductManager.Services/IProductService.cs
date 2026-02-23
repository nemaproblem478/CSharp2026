using ProductManager.UIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.Services
{
    public interface IProductService
    {
        public ProductUIModel GetProductUI(Guid id);
        public IEnumerable<ProductUIModel> GetProductsUI(Guid? warehouseId);
        public void SaveProduct(ProductUIModel uiModel);
    }
}
