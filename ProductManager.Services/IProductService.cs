using ProductManager.DTOModels;
using ProductManager.DTOModels.Product;

namespace ProductManager.Services
{
    public interface IProductService
    {
        public IEnumerable<ProductListDTO> GetProducts(Guid warehouseId);
        public ProductDetailsDTO GetProduct(Guid id);
        public void SaveProduct(ProductDetailsDTO productDetailsDTO);
    }
}
