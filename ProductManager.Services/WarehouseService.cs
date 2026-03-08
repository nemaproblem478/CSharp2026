using ProductManager.CommonComponents;
using ProductManager.DBModels;
using ProductManager.UIModels;

namespace ProductManager.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IStorageService _storage;

        public WarehouseService(IStorageService storageSevice)
        {
            _storage = storageSevice;
        }
        //Get warehouse ui model by warehouse id
        public WarehouseUIModel GetWarehouseUI(Guid id)
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
        //Get all warehouse ui models
        public IEnumerable<WarehouseUIModel> GetAllWarehousesUI()
        {
            _storage.LoadData();
            var resultList = new List<WarehouseUIModel>();
            foreach (var warehouse in _storage.GetWarehouses())
            {
                var uiModel = new WarehouseUIModel(warehouse);
                var products = _storage.GetProducts(warehouse.Id);
                foreach (var product in products)
                {
                    uiModel.Products.Add(new ProductUIModel(product));
                }
                resultList.Add(uiModel);
            }
            return resultList;
        }
        //Load Products to WarehouseUIModel
        public void LoadProducts(WarehouseUIModel uiModel)
        {
            uiModel.Products.Clear();
            _storage.LoadData();
            var products = _storage.GetProducts(uiModel.Id);
            foreach (var product in products)
            {
                uiModel.Products.Add(new ProductUIModel(product));
            }
            uiModel.CalculateTotalCost();
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
