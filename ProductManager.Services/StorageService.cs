using ProductManager.DBModels;
using ProductManager.UIModels;

namespace ProductManager.Services
{
    public class StorageService
    {
        private List<WarehouseDBModel> _warehouses;
        private List<ProductDBModel> _products;

        public void LoadData()
        {
            if (_warehouses != null && _products != null)
                return;
            _warehouses = FakeStorage.Warehouses.ToList();
            _products = FakeStorage.Products.ToList();
        }

        public WarehouseDBModel GetWarehouse(Guid? id)
        {
            LoadData();
            foreach (var warehouse in _warehouses)
            {
                if (warehouse.Id == id)
                    return warehouse;
            }
            return null;
        }
        public IEnumerable<WarehouseDBModel> GetWarehouses()
        {
            LoadData();
            var resultList = new List<WarehouseDBModel>();
            foreach (var warehouse in _warehouses)
            {
                resultList.Add(warehouse);
            }
            return resultList;
        }
        public ProductDBModel GetProduct(Guid id)
        {
            LoadData();
            foreach (var product in _products)
            {
                if (product.ProductId == id)
                {
                    return product;
                }
            }
            return null;
        }
        //Get all products db models by warehouse id
        public IEnumerable<ProductDBModel> GetProducts(Guid? warehouseId)
        {
            LoadData();
            var resultList = new List<ProductDBModel>();
            foreach(var product in _products)
            {
                if (product.WarehouseId == warehouseId)
                {
                    resultList.Add(product);
                }
            }
            return resultList;
        }
        //Add warehouse to the storage
        public bool AddWarehouse(WarehouseDBModel dbModel)
        {
            LoadData();
            _warehouses.Add(dbModel);
            return true;
        }
        //Add product to the storage
        public bool AddProduct(ProductDBModel dbModel)
        {
            LoadData();
            _products.Add(dbModel);
            return true;
        }
    }

}
