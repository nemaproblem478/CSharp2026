using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductManager.DBModels;
using ProductManager.Storage;

namespace ProductManager.Repository
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly IStorageContext _storageContext;
        public WarehouseRepository(IStorageContext storageContext)
        {
            _storageContext = storageContext; ;
        }
        public IEnumerable<WarehouseDBModel> GetWarehouses()
        {
            return _storageContext.GetWarehouses();
        }
    }
}
