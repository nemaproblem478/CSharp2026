using ProductManager.CommonComponents;

namespace ProductManager.DTOModels.Warehouse
{
    public class WarehouseDetailsDTO
    {
        public Guid Id { get; }
        public string Name { get; }
        public Location Location { get; }
        public double TotalCost { get; set; }
        public int ProductCount { get; set; }

        public WarehouseDetailsDTO(Guid id, string name, Location location, double totalCost, int productCount)
        {
            Id = id;
            Name = name;
            Location = location;
            TotalCost = totalCost;
            ProductCount = productCount;
        }
    }
    
}
