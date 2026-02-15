using ProductManager.CommonComponents.Enums;

namespace ProductManager.DBModels
{
    public class ProductDBModel
    {
        private Guid _productId;
        private Guid _warehouseId;
        private string _name;
        private int _quantity;
        private double _price;
        private Category _category;
        private string _description;

        public Guid ProductId { get => _productId; }
        public Guid WarehouseId { get => _warehouseId; set => _warehouseId = value; }
        public string Name { get => _name; set => _name = value; }
        public int Quantity { get => _quantity; set => _quantity = value; }
        public double Price { get => _price; set => _price = value; }
        public Category Category { get => _category; set => _category = value; }
        public string Description { get => _description; set => _description = value; }

        private ProductDBModel() 
        {

        }
        public ProductDBModel(Guid warehouseId, string name, int quantity, double price, Category category, string description)
        {
            _productId = Guid.NewGuid();
            _warehouseId = warehouseId;
            _name = name;
            _quantity = quantity;
            _price = price;
            _category = category;
            _description = description;
        }
    }
}
