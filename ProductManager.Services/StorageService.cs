using ProductManager.DBModels;
using ProductManager.Services;

namespace ProductManager.ProductManager.Services
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
        public IEnumerable<ProductDBModel> GetProducts(Guid warehouseId)
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
    }

}
