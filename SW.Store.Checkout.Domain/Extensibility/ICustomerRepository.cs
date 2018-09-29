namespace SW.Store.Checkout.Domain.Extensibility
{
    public interface ICustomerRepository : ICrudRepository<Customer, int>
    {
        Customer Get(string name, string address);
    }
}
