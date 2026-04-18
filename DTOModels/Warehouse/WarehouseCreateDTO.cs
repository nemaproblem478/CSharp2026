using ProductManager.CommonComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.DTOModels.Warehouse
{
    public class WarehouseCreateDTO : IWarehouseValidateDTO
    {
        public string Name { get; }
        public Location Location { get; }

        public WarehouseCreateDTO(string name, Location location)
        {
            Name = name;
            Location = location;
        }
    }
}
