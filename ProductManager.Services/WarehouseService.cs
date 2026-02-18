using ProductManager.CommonComponents;
using ProductManager.DBModels;
using ProductManager.UIModels;

namespace ProductManager.Services
{
    public class WarehouseService
    {
        private StorageService _storage;

        public WarehouseService()
        {
            _storage = new StorageService();
        }

        public WarehouseUIModel GetWarehouseUI(Guid id)
        {
            var dbModel = _storage.GetWarehouse(id);
            var uiModel = new WarehouseUIModel(dbModel);

            var productsDB = _storage.GetProducts(id);
            foreach (var product in productsDB)
            {
                uiModel.Products.Add(product);
            }
            return uiModel;
        }

        public void SaveWarehouse(WarehouseUIModel uiModel)
        {
            WarehouseDBModel dbModel;

            dbModel = _storage.GetWarehouse(uiModel.Id);
            if (dbModel != null)
            {
                dbModel.Name = uiModel.Name;
                dbModel.Location = uiModel.Location;
            }
            else
            {
                dbModel = new WarehouseDBModel(uiModel.Name, uiModel.Location);
                _storage.AddWarehouse(dbModel);
            }
        }
    }
}
