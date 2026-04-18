using ProductManager.CommonComponents;
using ProductManager.DBModels;
using ProductManager.DTOModels.Product;
using ProductManager.Repository;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


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
        public async Task<ProductDetailsDTO> GetProductAsync(Guid id)
        {
            var product = await _repository.GetProductAsync(id);

            return product is null ? null : new ProductDetailsDTO(product.ProductId, product.WarehouseId, product.Name, product.Description, product.Category, product.Quantity, product.Price);
        }
        //Get all ProductListDTO models by warehouse id
        public async Task<IEnumerable<ProductListDTO>> GetProductsByWarehouseAsync(Guid warehouseId)
        {
            return (await _repository.GetProductsByWarehouseAsync(warehouseId)).Select(product => new ProductListDTO(product.ProductId, product.Name, product.Category, product.Price));
        }
        //Save edited Product to storage
        public async Task UpdateProductAsync(ProductDetailsDTO productDTO)
        {
            var errors = productDTO.Validate();
            if (errors.Count > 0)
                throw new ValidationException(String.Join(Environment.NewLine, errors.Select(s => s.ErrorMessage)));
            var dbModel = new ProductDBModel(productDTO.ProductId, productDTO.WarehouseId, productDTO.Name, productDTO.Quantity, productDTO.Price, productDTO.Category, productDTO.Description);

            await _repository.SaveProductAsync(dbModel);
        }
        //Save created Product to storage
        public async Task CreateProductAsync(ProductCreateDTO productDTO)
        {
            var errors = productDTO.Validate();
            if (errors.Count > 0)
                throw new ValidationException(String.Join(Environment.NewLine, errors.Select(s => s.ErrorMessage)));
            var dbModel = new ProductDBModel(productDTO.WarehouseId, productDTO.Name, productDTO.Quantity, productDTO.Price, productDTO.Category, productDTO.Description);
            await _repository.SaveProductAsync(dbModel);
        }
        public Task DeleteProductAsync(Guid id)
        {
            return _repository.DeleteProductAsync(id);
        }
    }
}
