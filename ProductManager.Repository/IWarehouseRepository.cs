using ProductManager.DBModels;

namespace ProductManager.Repository
{
    public interface IWarehouseRepository
    {
        IEnumerable<WarehouseDBModel> GetWarehouses();
    }
}
