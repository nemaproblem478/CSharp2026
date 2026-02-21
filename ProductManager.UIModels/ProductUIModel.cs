using ProductManager.CommonComponents;
using ProductManager.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.UIModels
{
    public class ProductUIModel
    {
        private ProductDBModel _dbModel;
        private Guid _productId;
        private Guid _warehouseId;
        private string _name;
        private int _quantity;
        private double _price;
        private Category _category;
        private string _description;
        private double _totalCost;

        public Guid ProductId { get => _productId; }
        public Guid WarehouseId { get => _warehouseId; }
        public string Name { get => _name; set => _name = value; }
        public int Quantity { get => _quantity; set => _quantity = value; }
        public double Price { get => _price; set => _price = value; }
        public Category Category { get => _category; set => _category = value; }
        public string Description { get => _description; set => _description = value; }
        public double TotalCost { get => _totalCost; }

        public ProductUIModel(Guid warehouseId)
        {
            _warehouseId = warehouseId;
        }
        public ProductUIModel(ProductDBModel dbModel)
        {
            _dbModel = dbModel;
            _warehouseId = dbModel.WarehouseId;
            _productId = dbModel.ProductId;
            _name = dbModel.Name;
            _quantity = dbModel.Quantity;
            _price = dbModel.Price;
            _category = dbModel.Category;
            _description = dbModel.Description;
            CalculateTotalCost();
        }
        public void CalculateTotalCost()
        {
            _totalCost = Price * Quantity;
        }
        public override string ToString()
        {
            CalculateTotalCost();
            return $"{Name}\nCategory: {Category},\nQuantity: {Quantity}, Price: ${Price}, Total cost: ${TotalCost}\nDescription: {Description}";
        }
    }
}
