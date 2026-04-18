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
        Task<WarehouseDetailsDTO> GetWarehouseAsync(Guid id);
        IAsyncEnumerable<WarehouseListDTO> GetAllWarehousesAsync();
        Task UpdateWarehouseAsync(WarehouseDetailsDTO warehouseDTO);
        Task CreateWarehouseAsync(WarehouseCreateDTO warehouseDTO);
        Task DeleteWarehouseAsync(Guid id);
    }
}
