using ProductManager.CommonComponents;
using ProductManager.DBModels;
using ProductManager.DTOModels.Warehouse;
using ProductManager.Repository;
using System.ComponentModel.DataAnnotations;

namespace ProductManager.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IWarehouseRepository _repository;

        public WarehouseService(IWarehouseRepository repository)
        {
            _repository = repository;
        }
        //Get WarehouseDetailsDTO model by warehouse id
        public async Task<WarehouseDetailsDTO?> GetWarehouseAsync(Guid id)
        {
            var warehouse = await _repository.GetWarehouseAsync(id);
            return warehouse is null ? null : new WarehouseDetailsDTO(warehouse.Id, warehouse.Name, warehouse.Location, await _repository.GetWarehouseTotalCostAsync(warehouse.Id), await _repository.GetProductsByWarehouseCountAsync(warehouse.Id));
        }
        //Get all WarehouseListDTO models
        public async IAsyncEnumerable<WarehouseListDTO> GetAllWarehousesAsync()
        {
            await foreach (var warehouse in _repository.GetWarehousesAsync())
            {
                yield return new WarehouseListDTO(warehouse.Id, warehouse.Name, await _repository.GetWarehouseTotalCostAsync(warehouse.Id));
            }
        }
        public async Task UpdateWarehouseAsync(WarehouseDetailsDTO warehouse)
        {
            var errors = warehouse.Validate();
            if (errors.Count > 0)
                throw new ValidationException(String.Join(Environment.NewLine, errors.Select(s => s.ErrorMessage)));
            var dbModel = new WarehouseDBModel(warehouse.Id, warehouse.Name, warehouse.Location);
            await _repository.SaveWarehouseAsync(dbModel);
        }
        public async Task CreateWarehouseAsync(WarehouseCreateDTO warehouse)
        {
            var errors = warehouse.Validate();
            if (errors.Count > 0)
                throw new ValidationException(String.Join(Environment.NewLine, errors.Select(s => s.ErrorMessage)));
            var dbModel = new WarehouseDBModel(warehouse.Name, warehouse.Location);
            await _repository.SaveWarehouseAsync(dbModel);
        }
        public Task DeleteWarehouseAsync(Guid id)
        {
            return _repository.DeleteWarehouseAsync(id);
        }
    }
}
