using ProductManager.CommonComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.DTOModels.Product
{
    public interface IProductValidateDTO
    {
        public Guid WarehouseId { get; }
        public string Name { get; }
        public string Description { get; }
        public Category Category { get; }
        public int Quantity { get; }
        public double Price { get; }
    }
}
