﻿using System.Collections.Generic;

namespace SW.Store.Checkout.Domain
{
    public class Warehouse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Item> Items { get; set; }
    }
}