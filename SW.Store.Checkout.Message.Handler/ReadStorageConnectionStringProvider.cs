using SW.Store.Core.Settings;

namespace SW.Store.Checkout.Message.Handler
{
    internal class ReadStorageConnectionStringProvider : IReadStorageConnectionStringProvider
    {
        public string Get()
        {
            return "Server=127.0.0.1;Port=5432;Database=swstore;User Id=postgres;Password=123456;";
        }
    }
}
