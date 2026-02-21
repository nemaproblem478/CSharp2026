using ProductManager.CommonComponents;
using ProductManager.DBModels;
using ProductManager.UIModels;

namespace ProductManager.Services
{
    public class ProductService
    {
        private StorageService _storage;

        private ProductService() {}
        public ProductService(StorageService storageSevice)
        {
            _storage = storageSevice;
        }

        public ProductUIModel GetProductUI(Guid id)
        {
            var dbModel = _storage.GetProduct(id);
            var uiModel = new ProductUIModel(dbModel);

            return uiModel;
        }
        //Get all products ui models by warehouse id
        public IEnumerable<ProductUIModel> GetProductsUI(Guid? warehouseId)
        {
            _storage.LoadData();
            var resultList = new List<ProductUIModel>();
            foreach (var product in _storage.GetProducts(warehouseId))
            {
                resultList.Add(new ProductUIModel(product));
            }
            return resultList;
        }

        public void SaveProduct(ProductUIModel uiModel)
        {
            ProductDBModel dbModel;

            dbModel = _storage.GetProduct(uiModel.ProductId);
            if (dbModel != null)
            {
                dbModel.Name = uiModel.Name;
                dbModel.Quantity = uiModel.Quantity;
                dbModel.Price = uiModel.Price;
                dbModel.Category = uiModel.Category;
                dbModel.Description = uiModel.Description;
            }
            else
            {
                dbModel = new ProductDBModel(uiModel.WarehouseId, uiModel.Name, uiModel.Quantity, uiModel.Price, uiModel.Category, uiModel.Description);
                _storage.AddProduct(dbModel);
            }
        }
    }
}
