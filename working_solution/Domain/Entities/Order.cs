using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Order
    {
        public long id { get; set; }
        public string customer_name { get; set; }
        public string customer_email { get; set; }
        public IList<Item> items { get; set; }

        public Order(string customer_name, string customer_email, IList<Item> items)
        {
            this.customer_name = customer_name;
            this.customer_email = customer_email;
            this.items = items;
        }
    }
}