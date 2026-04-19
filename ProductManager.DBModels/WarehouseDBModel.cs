using ProductManager.CommonComponents;

namespace ProductManager.DBModels
{
    public class WarehouseDBModel
    {

        public Guid Id { get; init; }
        public string Name { get; set; }
        public Location Location { get; set; }

        public WarehouseDBModel() { }
        public WarehouseDBModel(string name, Location location) : this(Guid.NewGuid(), name, location)
        {
        }
        public WarehouseDBModel(Guid id, string name, Location location)
        {
            Id = id;
            Name = name;
            Location = location;
        }

    }
}
