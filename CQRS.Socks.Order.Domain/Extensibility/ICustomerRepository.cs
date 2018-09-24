namespace CQRS.Socks.Order.Domain.Extensibility
{
    public interface ICustomerRepository : ICrudRepository<Customer, int>
    {
        Customer Get(string name, string address);
    }
}
