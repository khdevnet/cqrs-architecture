namespace SW.Store.Checkout.Domain.Extensibility
{
    public interface IWarehouseItemRepository : ICrudRepository<WarehouseItem, int>
    {
        WarehouseItem Get(int productId, int warehouseId);

        WarehouseItem UpdateQuantity(int productId, int warehouseId, int quantity);
    }
}
