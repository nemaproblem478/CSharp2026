using ProductManager.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.Services
{
    public interface IStorageService
    {
        public void LoadData();
        public WarehouseDBModel GetWarehouse(Guid? id);
        public IEnumerable<WarehouseDBModel> GetWarehouses();
        public ProductDBModel GetProduct(Guid id);
        public IEnumerable<ProductDBModel> GetProducts(Guid? warehouseId);
        public bool AddWarehouse(WarehouseDBModel dbModel);
        public bool AddProduct(ProductDBModel dbModel);

    }
}
