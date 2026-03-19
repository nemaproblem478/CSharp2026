using ProductManager.CommonComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.DTOModels.Product
{
   public class ProductListDTO
   {
        public Guid Id { get; }
        public string Name { get; }
        public Category Category { get; }
        public double Price { get; }
        public ProductListDTO(Guid id, string name, Category category, double price)
        {
            Id = id;
            Name = name;
            Category = category;
            Price = price;
        }
   }
}
