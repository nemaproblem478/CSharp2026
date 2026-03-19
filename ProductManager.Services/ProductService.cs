using ProductManager.CommonComponents;
using ProductManager.DBModels;
using ProductManager.DTOModels.Product;
using ProductManager.Repository;

namespace ProductManager.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        //Get ProductDetailsDTO model by product id
        public ProductDetailsDTO GetProduct(Guid id)
        {
            var product = _repository.GetProduct(id);

            return product is null ? null : new ProductDetailsDTO(product.ProductId, product.WarehouseId, product.Name, product.Description, product.Category, product.Quantity, product.Price);
        }
        //Get all ProductListDTO models by warehouse id
        public IEnumerable<ProductListDTO> GetProducts(Guid warehouseId)
        {
            foreach (var product in _repository.GetProductsByWarehouse(warehouseId))
            {
                yield return new ProductListDTO(product.ProductId, product.Name, product.Category, product.Price);
            }
        }
        //Save Product to storage
        public void SaveProduct(ProductDetailsDTO productDTO)
        {
            var dbModel = new ProductDBModel(productDTO.ProductId, productDTO.WarehouseId, productDTO.Name, productDTO.Quantity, productDTO.Price, productDTO.Category, productDTO.Description);

            _repository.SaveProduct(dbModel);
        }
    }
}
