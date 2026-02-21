using ProductManager.CommonComponents;
using ProductManager.DBModels;
using ProductManager.UIModels;

namespace ProductManager.Services
{
    public class WarehouseService
    {
        private StorageService _storage;

        private WarehouseService() {}
        public WarehouseService(StorageService storageSevice)
        {
            _storage = storageSevice;
        }

        public WarehouseUIModel GetWarehouseUI(Guid? id)
        {
            if (id == null) return null;
            else
            {
                var dbModel = _storage.GetWarehouse(id);
                var uiModel = new WarehouseUIModel(dbModel);

                var products = _storage.GetProducts(id);
                foreach (var product in products)
                {
                    uiModel.Products.Add(new ProductUIModel(product));
                }
                uiModel.CalculateTotalCost();
                return uiModel;
            }
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
