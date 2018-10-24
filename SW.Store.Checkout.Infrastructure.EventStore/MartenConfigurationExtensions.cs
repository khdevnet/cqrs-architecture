using System;
using System.Collections.Generic;
using System.Text;
using Marten;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SW.Store.Checkout.Domain.Orders;
using SW.Store.Checkout.Domain.Orders.Events;

namespace SW.Store.Checkout.Infrastructure.EventStore
{
    public static class MartenConfigurationExtensions
    {
        public static void ConfigureMarten(this IServiceCollection services, string connectionString)
        {
            services.AddScoped(sp =>
            {
                var documentStore = DocumentStore.For(options =>
                {
                    var schemaName = "public";

                    options.Connection(connectionString);
                    options.AutoCreateSchemaObjects = AutoCreate.All;
                    options.Events.DatabaseSchemaName = schemaName;
                    options.DatabaseSchemaName = schemaName;

                    options.Events.InlineProjections.AggregateStreamsWith<OrderAggregate>();
                    //options.Events.InlineProjections.Add(new AllAccountsSummaryViewProjection());
                    //options.Events.InlineProjections.Add(new AccountSummaryViewProjection());
                    //options.Events.InlineProjections.Add(new ClientsViewProjection());

                    options.Events.AddEventType(typeof(OrderCreated));
                    options.Events.AddEventType(typeof(OrderLineAdded));
                });

                return documentStore.OpenSession();
            });
        }

    }
}
