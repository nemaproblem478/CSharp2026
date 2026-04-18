using ProductManager.CommonComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.DTOModels.Warehouse
{
    public interface IWarehouseValidateDTO
    {
        public string Name { get; }
        public Location Location { get; }
    }
}
