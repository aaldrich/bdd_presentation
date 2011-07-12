using System;
using System.Collections.Generic;
using Domain;
using Domain.Entities;
using Domain.Infrastructure;
using Domain.Tasks;
using Machine.Specifications;
using Machine.Specifications.DevelopWithPassion.Rhino;
using Messages;
using Rhino.Mocks;

namespace BDD_Tests
{
    public class OrderPlacingTaskSpecs : Observes<OrderPlacingTask>
    {
        Establish context = () =>
        {
            shipping_task = the_dependency<IShippingTask>();
            emailing_task = the_dependency<IEmailingTask>();
            order_repository = the_dependency<IRepository<Order>>();
        };

        protected static IShippingTask shipping_task;
        protected static IEmailingTask emailing_task;
        protected static IRepository<Order> order_repository;
    }
		
    public class when_a_customer_places_an_order_that_is_successful : OrderPlacingTaskSpecs
    {
        Establish context = () =>
        {
            message = new OrderPlacingMessage()
                          {
                              customer_email = "adam@adamaldrich.com",
                              customer_name = "Adam Aldrich",
                              item_ids = new List<long>(){1,2}
                          };
        };
			
        Because b = () =>
        {
            order = sut.place_order(message);					
        };

        It should_return_the_created_order = () =>
        {
            order.ShouldNotBeNull();                       
        };

        It should_save_the_order_to_the_repository = () =>
        {
            order_repository.AssertWasCalled(x => x.save(order));
        };

        It should_ship_the_items_to_the_customer = () =>
        {
            shipping_task.AssertWasCalled(x=>x.ship(order));
        };

        It should_send_the_customer_an_email = () =>
        {
            emailing_task.AssertWasCalled(x=>x.send(order));
        };

        static OrderPlacingMessage message;
        static Order order;
    }

    public class when_a_customer_places_an_order_that_is_not_successful : OrderPlacingTaskSpecs
    {
        Establish context = () =>
        {
            message = new OrderPlacingMessage()
                          {
                              customer_email = "adam@adamaldrich.com",
                              customer_name = "Adam Aldrich",
                              item_ids = new List<long>(){1,2}
                          };

            order_repository = the_dependency<IRepository<Order>>();
            order_repository.Stub(x => x.save(Arg<Order>.Is.Anything)).Throw(new Exception("oh no!"));
        };
			
        Because b = () =>
        {
            catch_exception(()=> order = sut.place_order(message));					
        };

        It should_throw_a_domain_exception = () =>
        {
            exception_thrown_by_the_sut.ShouldBeAn<DomainException>();                   
        };

        It should_return_a_null_order = () =>
        {
            order.ShouldBeNull();
        };
        
        It should_not_ship_the_items_to_the_customer = () =>
        {
            shipping_task.AssertWasNotCalled(x=>x.ship(order));
        };

        It should_not_send_the_customer_an_email = () =>
        {
            emailing_task.AssertWasNotCalled(x=>x.send(order));
        };

        static OrderPlacingMessage message;
        static Order order;
    }
}
