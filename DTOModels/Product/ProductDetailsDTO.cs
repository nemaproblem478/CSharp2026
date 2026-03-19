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
        public Guid ProductId { get; set; }
        public Guid WarehouseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
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
