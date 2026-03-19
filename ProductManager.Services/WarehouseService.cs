using ProductManager.CommonComponents;
using ProductManager.DBModels;
using ProductManager.DTOModels.Warehouse;
using ProductManager.Repository;

namespace ProductManager.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IWarehouseRepository _repository;

        public WarehouseService(IWarehouseRepository repository)
        {
            _repository = repository;
        }
        //Get warehouse ui model by warehouse id
        public WarehouseDetailsDTO GetWarehouse(Guid id)
        {
            var warehouse = _repository.GetWarehouse(id);
            return warehouse is null ? null : new WarehouseDetailsDTO(warehouse.Id, warehouse.Name, warehouse.Location, _repository.GetWarehouseTotalCost(warehouse.Id), _repository.GetProductsByWarehouseCount(warehouse.Id));
        }
        //Get all warehouse ui models
        public IEnumerable<WarehouseListDTO> GetAllWarehouses()
        {
            foreach (var warehouse in _repository.GetWarehouses())
            {
                yield return new WarehouseListDTO(warehouse.Id, warehouse.Name, _repository.GetWarehouseTotalCost(warehouse.Id));
            }
        }
        //Load Products to WarehouseUIModel
        //public void LoadProducts(WarehouseUIModel uiModel)
        //{
        //    uiModel.Products.Clear();
        //    _repository.LoadData();
        //    var products = _repository.GetProducts(uiModel.Id);
        //    foreach (var product in products)
        //    {
        //        uiModel.Products.Add(new ProductUIModel(product));
        //    }
        //    uiModel.CalculateTotalCost();
        //}
    }
}
