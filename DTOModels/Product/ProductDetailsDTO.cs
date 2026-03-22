using ProductManager.CommonComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.DTOModels.Product
{
    public class ProductDetailsDTO
    {
        public Guid ProductId { get; }
        public Guid WarehouseId { get; }
        public string Name { get; }
        public string Description { get; }
        public Category Category { get; }
        public int Quantity { get; }
        public double Price { get; }
        public ProductDetailsDTO(Guid productId, Guid warehouseId, string name, string description, Category category, int quantity, double price)
        {
            ProductId = productId;
            WarehouseId = warehouseId;
            Name = name;
            Description = description;
            Category = category;
            Quantity = quantity;
            Price = price;
        }
    }
}
