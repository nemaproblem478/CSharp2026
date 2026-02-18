using ProductManager.CommonComponents;
using ProductManager.DBModels;

namespace ProductManager.UIModels
{
    public class WarehouseUIModel
    {
        private WarehouseDBModel _dbModel;
        private Guid _id;
        private string _name;
        private Location _location;
        private List<ProductDBModel> _products;
        private double _totalCost;

        public Guid Id { get => _id; }
        public string Name { get => _name; set => _name = value; }
        public Location Location { get => _location; set => _location = value; }
        public List<ProductDBModel> Products { get => _products; set => _products = value; }
        public double TotalCost { get => _totalCost; set => _totalCost = value; }

        public WarehouseUIModel()
        {
            _products = new List<ProductDBModel>();
        }
        public WarehouseUIModel(WarehouseDBModel dbModel) : this()
        {
            _dbModel = dbModel;
            _id = dbModel.Id;
            _name = dbModel.Name;
            _location = dbModel.Location;
            calculateTotalCost();
        }
        public void calculateTotalCost()
        {
            var result = 0.0;
            foreach (var product in _products) 
            {
                result += product.Quantity * product.Price;
            }
            _totalCost = result;
        }
        public override string ToString()
        {
            return $"Warehouse: {Name}, Location: {Location}, Products: {Products.Count}, Total Cost: {TotalCost}";
        }
    }
}
