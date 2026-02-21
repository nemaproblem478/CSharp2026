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
        private List<ProductUIModel> _products;
        private double _totalCost;

        public Guid Id { get => _id; }
        public string Name { get => _name; set => _name = value; }
        public Location Location { get => _location; set => _location = value; }
        public List<ProductUIModel> Products { get => _products; set => _products = value; }
        public double TotalCost { get => _totalCost; }

        public WarehouseUIModel()
        {
            _products = new List<ProductUIModel>();
        }
        public WarehouseUIModel(WarehouseDBModel dbModel) : this()
        {
            _dbModel = dbModel;
            _id = dbModel.Id;
            _name = dbModel.Name;
            _location = dbModel.Location;
        }
        public void CalculateTotalCost()
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
            CalculateTotalCost();
            return $"Warehouse: {Name}, Location: {Location}, Products: {Products.Count}, Total Cost: ${TotalCost}";
        }
    }
}
