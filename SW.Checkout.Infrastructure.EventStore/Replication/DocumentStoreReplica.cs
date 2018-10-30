using Marten;

namespace SW.Checkout.Infrastructure.EventStore.Replication
{
    internal class DocumentStoreReplica : DocumentStore, IDocumentStoreReplica
    {
        public DocumentStoreReplica(StoreOptions options) : base(options)
        {

        }
    }
}
