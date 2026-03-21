using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductManager.DTOModels;
using ProductManager.DTOModels.Warehouse;

namespace ProductManager.Services
{
    public interface IWarehouseService
    {
        public WarehouseDetailsDTO GetWarehouse(Guid id);
        public IEnumerable<WarehouseListDTO> GetAllWarehouses();
        //public void LoadProducts(WarehouseUIModel uiModel);
    }
}
