using ProductManager.CommonComponents;

namespace ProductManager.DTOModels.Warehouse
{
    public class WarehouseDetailsDTO : IWarehouseValidateDTO
    {
        public Guid Id { get; }
        public string Name { get; }
        public Location Location { get; }
        public double TotalCost { get; }
        public int ProductCount { get; }

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
