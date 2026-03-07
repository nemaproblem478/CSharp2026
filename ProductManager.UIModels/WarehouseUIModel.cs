using ProductManager.CommonComponents;
using ProductManager.DBModels;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProductManager.UIModels
{
    public class WarehouseUIModel
    {

        private WarehouseDBModel _dbModel;
        private Guid _id;
        private string _name;
        private Location _location;
        private ObservableCollection<ProductUIModel> _products;
        private double _totalCost;

        public Guid Id { get => _id; }
        public string Name { get => _name; set => _name = value; }
        public Location Location { get => _location; set => _location = value; }
        public ObservableCollection<ProductUIModel> Products { get => _products; set => _products = value; }
        public double TotalCost
        {
            get
            {
                CalculateTotalCost(); //calculate TotalCost before returning it
                return _totalCost;
            }
        }

        private WarehouseUIModel()
        {
            _products = new ObservableCollection<ProductUIModel>();
        }
        public WarehouseUIModel(WarehouseDBModel dbModel) : this()
        {
            _dbModel = dbModel;
            _id = dbModel.Id;
            _name = dbModel.Name;
            _location = dbModel.Location;
        }
        //WarehouseUI copy constructor
        public WarehouseUIModel(WarehouseUIModel uiModel) : this()
        {
            _dbModel = uiModel._dbModel;
            _id = uiModel.Id;
            _name = uiModel.Name;
            _location = uiModel.Location;
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
            //CalculateTotalCost();
            return $"Warehouse: {Name}, Location: {Location}, Products: {Products.Count}, Total Cost: ${TotalCost}";
        }

    }
}
