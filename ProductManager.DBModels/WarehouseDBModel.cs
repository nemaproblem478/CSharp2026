using ProductManager.CommonComponents;

namespace ProductManager.DBModels
{
    public class WarehouseDBModel
    {
        private Guid _id;
        private string _name;
        private Location _location;

        public Guid Id { get => _id; }
        public string Name { get => _name; set => _name = value; }
        public Location Location { get => _location; set => _location = value; }

        private WarehouseDBModel()
        {

        }
        public WarehouseDBModel(string name, Location location)
        {
            _id = Guid.NewGuid();
            _name = name;
            _location = location;
        }

    }
}
