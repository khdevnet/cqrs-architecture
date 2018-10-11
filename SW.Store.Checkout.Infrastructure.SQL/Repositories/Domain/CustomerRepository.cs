using System.Linq;
using SW.Store.Checkout.Domain;
using SW.Store.Checkout.Domain.Extensibility;
using SW.Store.Checkout.Infrastructure.SQL.Database;

namespace SW.Store.Checkout.Infrastructure.SQL.Repositories.Domain
{
    internal class CustomerRepository : CrudRepository<Customer, int>, ICustomerRepository
    {

        public CustomerRepository(SwStoreDbContext db) : base(db)
        {

        }

        public Customer Get(string name, string address)
        {
            return db.Customers.FirstOrDefault(c => c.Name == name && c.ShippingAddress == address);
        }
    }
}
