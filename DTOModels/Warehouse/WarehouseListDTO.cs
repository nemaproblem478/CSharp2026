using ProductManager.CommonComponents;

namespace ProductManager.DTOModels.Warehouse
{
    public class WarehouseListDTO
    {
        public Guid Id { get; }
        public string Name { get; }
        public double TotalCost { get; set; }

        public WarehouseListDTO(Guid id, string name, double totalCost)
        {
            Id = id;
            Name = name;
            TotalCost = totalCost;
        }
    }
}
