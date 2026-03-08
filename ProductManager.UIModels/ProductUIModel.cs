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
        private readonly ProductDBModel? _dbModel;
        private readonly Guid _productId;
        private readonly Guid _warehouseId;
        private string _name;
        private int _quantity;
        private double _price;
        private Category _category;
        private string _description;
        private double _totalCost;

        public Guid ProductId { get => _productId; }
        public Guid WarehouseId { get => _warehouseId; }
        public string Name { get => _name; set => _name = value; }
        public int Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                CalculateTotalCost();
            }
        }
        public double Price 
        { 
            get => _price;
            set
            {
                _price = value;
                CalculateTotalCost();
            }
        }
        public Category Category { get => _category; set => _category = value; }
        public string Description { get => _description; set => _description = value; }
        public double TotalCost { get => _totalCost; set { } }

        public ProductUIModel(Guid warehouseId)
        {
            _warehouseId = warehouseId;
            _name = "";
            _description = "";
        }
        public ProductUIModel(Guid warehouseId, string name, int quantity, double price, Category category, string description)
        {
            _warehouseId = warehouseId;
            _name = name;
            _quantity = quantity;
            _price = price;
            _category = category;
            _description = description;
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
        //ProductUIModel copy constructor
        public ProductUIModel(ProductUIModel uiModel) : this(uiModel.WarehouseId)
        {
            _dbModel = uiModel._dbModel;
            _productId = uiModel.ProductId;
            _name = uiModel.Name;
            _quantity = uiModel.Quantity;
            _price = uiModel.Price;
            _category = uiModel.Category;
            _description = uiModel.Description;
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
