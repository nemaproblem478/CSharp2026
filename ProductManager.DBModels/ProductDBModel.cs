using ProductManager.CommonComponents;

namespace ProductManager.DBModels
{
    public class ProductDBModel
    {

        public Guid ProductId { get; init; }
        public Guid WarehouseId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public double Price { get; set; }
        public Category Category { get; set; }
        public string Description { get; set; } = string.Empty;

        public ProductDBModel() { }
        public ProductDBModel(Guid warehouseId, string name, int quantity, double price, Category category, string description) : this(Guid.NewGuid(), warehouseId, name, quantity, price, category, description)
        { }
        public ProductDBModel(Guid productId, Guid warehouseId, string name, int quantity, double price, Category category, string description)
        {
            ProductId = productId;
            WarehouseId = warehouseId;
            Name = name;
            Quantity = quantity;
            Price = price;
            Category = category;
            Description = description;
        }
    }
}
