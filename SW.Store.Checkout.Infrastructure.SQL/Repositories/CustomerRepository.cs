using System.Linq;
using CQRS.Socks.Order.Domain;
using CQRS.Socks.Order.Domain.Extensibility;
using CQRS.Socks.Order.Infrastructure.SQL.Database;

namespace CQRS.Socks.Order.Infrastructure.SQL.Repositories
{
    internal class CustomerRepository : CrudRepository<Customer, int>, ICustomerRepository
    {

        public CustomerRepository(SocksShopDbContext db) : base(db)
        {

        }

        public Customer Get(string name, string address)
        {
            return db.Customers.FirstOrDefault(c => c.Name == name && c.ShippingAddress == address);
        }
    }
}
