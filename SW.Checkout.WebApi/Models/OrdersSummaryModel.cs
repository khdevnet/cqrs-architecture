using System.Collections.Generic;

namespace SW.Checkout.WebApi.Models
{
    public class OrdersSummaryModel
    {
        public int Count { get; set; }

        public IEnumerable<OrderReadModel> LastOrders { get; set; }
    }
}
