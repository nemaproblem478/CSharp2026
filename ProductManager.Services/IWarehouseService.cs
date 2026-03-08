using ProductManager.UIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.Services
{
    public interface IWarehouseService
    {
        public WarehouseUIModel GetWarehouseUI(Guid id);
        public IEnumerable<WarehouseUIModel> GetAllWarehousesUI();
        public void LoadProducts(WarehouseUIModel uiModel);
        public void SaveWarehouse(WarehouseUIModel uiModel);
    }
}
