using ProductManager.DBModels;
using ProductManager.CommonComponents;

namespace ProductManager.Services
{
    internal static class FakeStorage
    {
        private static readonly List<WarehouseDBModel> _warehouses;
        private static readonly List<ProductDBModel> _products;

        internal static IEnumerable<WarehouseDBModel> Warehouses
        {
            get
            {
                return _warehouses.ToList();
            }
        }
        internal static IEnumerable<ProductDBModel> Products
        {
            get
            {
                return _products.ToList();
            }
        }

        static FakeStorage()
        {
            var warehouseKyiv = new WarehouseDBModel("Kyiv Warehouse", Location.Kyiv);
            var warehouseKhmelnytsky = new WarehouseDBModel("Khmelnytsky Warehouse", Location.Khmelnytsky);
            var warehouseLviv = new WarehouseDBModel("Lviv Warehouse", Location.Lviv);
            _warehouses = new List<WarehouseDBModel> { warehouseKyiv, warehouseKhmelnytsky, warehouseLviv };
            _products = new List<ProductDBModel>
            {
                new ProductDBModel(warehouseKyiv.Id, "1993 Fender Telecaster TC72", 1, 1500, Category.ElectricGuitars, "1993 Fender Japan '72 Telecaster Custom (TC72) in Ocean Turquoise Metallic (OTM)—the rare early FujiGen iteration of the color, which is much darker than the common CIJ iteration."),
                new ProductDBModel(warehouseKhmelnytsky.Id, "Marshall JCM 900", 2, 1150, Category.Amplifiers, "Marshall JCM 900 model 4102 amp. 2 X G12T-75 Speakers. 100watt combo. Recently serviced. Great Sound"),

            };

        }
    }
}
