﻿using System;
using System.Collections.Generic;
using System.Linq;
using Marten;
using SW.Store.Checkout.Domain.Orders.Views;
using SW.Store.Checkout.Read.Extensibility;
using SW.Store.Checkout.Read.ReadView;
using SW.Store.Core.Aggregates;

namespace SW.Store.Checkout.Infrastructure.EventStore.Repositories
{
    internal sealed class OrderReadRepository : IOrderReadRepository
    {
        private readonly IDocumentStore store;

        public OrderReadRepository(IDocumentStore store)
        {
            this.store = store;
        }

        public void Store(EventSourcedAggregate aggregate)
        {
            using (IDocumentSession session = store.OpenSession())
            {
                // Take non-persisted events, push them to the event stream, indexed by the aggregate ID
                Core.Events.IEvent[] events = aggregate.PendingEvents.ToArray();
                session.Events.Append(aggregate.Id, aggregate.PendingEvents.ToArray());
                session.SaveChanges();
            }
            // Once succesfully persisted, clear events from list of uncommitted events
            aggregate.PendingEvents.Clear();
        }

        public T Load<T>(Guid id, int version = 0) where T : class, IAggregate, new()
        {
            using (IDocumentSession session = store.LightweightSession())
            {
                return session.Events.AggregateStream<T>(id, version);
            }

            throw new InvalidOperationException($"No aggregate by id {id}.");
        }

        public OrderReadView GetById(Guid id)
        {
            using (IDocumentSession session = store.OpenSession())
            {
                return session
                .Query<OrderView>()
                .ToList()
                .Select(a => new OrderReadView
                {
                    Id = a.Id,
                    Lines = a.Lines.Select(l => new OrderLineReadView
                    {
                        ProductId = l.ProductId,
                        Quantity = l.Quantity
                    }).ToList(),
                }).FirstOrDefault(p => p.Id == id);
            }
        }

        public IEnumerable<OrderReadView> Get()
        {
            throw new NotImplementedException();
        }
    }
}
