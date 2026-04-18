using ProductManager.DTOModels;
using ProductManager.DTOModels.Product;

namespace ProductManager.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductListDTO>> GetProductsByWarehouseAsync(Guid warehouseId);
        Task<ProductDetailsDTO> GetProductAsync(Guid id);
        Task UpdateProductAsync(ProductDetailsDTO productDetailsDTO);
        Task CreateProductAsync(ProductCreateDTO productCreateDTO);
        Task DeleteProductAsync(Guid id);
    }
}
