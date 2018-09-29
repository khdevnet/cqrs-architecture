namespace SW.Store.Checkout.Domain.Extensibility
{
    public interface IWarehouseRepository : ICrudRepository<Warehouse, int>
    {
        Warehouse Get(int productId, int quantity);
    }
}
