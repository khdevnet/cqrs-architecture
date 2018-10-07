using SW.Store.Core;

namespace SW.Store.Checkout.OrderHandler.Application
{
    internal class ConnectionStringProvider : IConnectionStringProvider
    {
        public string Get()
        {
            return "Server=.\\SQLEXPRESS;Database=SwStoreDbContext;Trusted_Connection=True;";
        }
    }
}
