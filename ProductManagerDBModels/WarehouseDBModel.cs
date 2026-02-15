
namespace ProductManagerDBModels
{
    public enum Location
    {
        Kyiv,
        Vasylkiv,
        Khmelnytsky,
        Neresnytsya,
        Lviv,
        Dnipro,
    }
    public class WarehouseDBModel
    {
        private Guid _id;
        private string _name;
        private Location _location;
        private List<ProductDBModel> _products;
        private double _totalCost;

        public Guid Id { get => _id; }
        public string Name { get => _name; set => _name = value; }
        public Location Location { get => _location; set => _location = value; }
        public List<ProductDBModel> Products { get => _products; set => _products = value; }
        public double TotalCost { get => _totalCost; }

        private WarehouseDBModel()
        {

        }
        public WarehouseDBModel(string name, Location location, List<ProductDBModel> products)
        {
            _id = Guid.NewGuid();
            _name = name;
            _location = location;
            _products = products;
        }

    }
}
