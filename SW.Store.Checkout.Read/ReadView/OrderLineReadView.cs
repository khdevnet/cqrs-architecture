﻿using System;

namespace SW.Store.Checkout.Read.ReadView
{
    public class OrderLineReadView
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string Status { get; set; }

        public int Quantity { get; set; }

        public Guid OrderId { get; set; }

        public OrderReadView Order { get; set; }
    }
}
