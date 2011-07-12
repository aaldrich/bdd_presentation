using System;
using System.Collections.Generic;
using Domain.Entities;
using Domain.Infrastructure;
using Messages;

namespace Domain.Tasks
{
    public class OrderPlacingTask
    {
        readonly IRepository<Order> order_repository;
        readonly IRepository<Item> item_repository;
        readonly IShippingTask shipping_task;
        readonly IEmailingTask emailing_task;

        public OrderPlacingTask(
            IRepository<Order> order_repository,
            IRepository<Item> item_repository,
            IShippingTask shipping_task,
            IEmailingTask emailing_task
            )
        {
            this.order_repository = order_repository;
            this.item_repository = item_repository;
            this.shipping_task = shipping_task;
            this.emailing_task = emailing_task;
        }

        public Order place_order(OrderPlacingMessage message)
        {
            var order = new Order(message.customer_name, message.customer_email, get_items(message.item_ids));

            try
            {
                order_repository.save(order);
            }
            catch (Exception)
            {
                throw new DomainException(String.Format("Order for customer {0} could not be completed.",message.customer_name));
            }

            shipping_task.ship(order);
            emailing_task.send(order);
            return order;
        }

        public IList<Item> get_items(IList<long> item_ids)
        {
            IList<Item> return_items = new List<Item>();

            foreach (var item_id in item_ids)
            {
                var item = item_repository.get_by(item_id);
                return_items.Add(item);
            }

            return return_items;
        }
    }
}
