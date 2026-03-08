using ProductManager.CommonComponents;

namespace ProductManager.DBModels
{
    public class WarehouseDBModel
    {
        private readonly Guid _id;
        private string _name;
        private Location _location;

        public Guid Id { get => _id; }
        public string Name { get => _name; set => _name = value; }
        public Location Location { get => _location; set => _location = value; }

        public WarehouseDBModel(string name, Location location) : this(Guid.NewGuid(), name, location)
        {
        }
        public WarehouseDBModel(Guid id, string name, Location location)
        {
            _id = id;
            _name = name;
            _location = location;
        }

    }
}
