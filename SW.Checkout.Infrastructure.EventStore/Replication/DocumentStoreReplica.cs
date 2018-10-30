using Marten;

namespace SW.Store.Checkout.Infrastructure.EventStore.Replication
{
    internal class DocumentStoreReplica : DocumentStore, IDocumentStoreReplica
    {
        public DocumentStoreReplica(StoreOptions options) : base(options)
        {

        }
    }
}
