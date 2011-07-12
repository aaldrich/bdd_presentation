//using System;
//using System.Collections.Generic;
//using Domain;
//using Domain.Entities;
//using Domain.Infrastructure;
//using Domain.Tasks;
//using Messages;
//using Moq;
//using NUnit.Framework;

//namespace Traditional_Tests
//{
//    [TestFixture]
//    public class OrderingTest
//    {
//        OrderPlacingTask order_placing_task;
//        Mock<IRepository<Order>> order_repository;
//        Mock<IRepository<Item>> item_repository;
//        Mock<IShippingTask> shipping_task;
//        Mock<IEmailingTask> emailing_task;

//        [SetUp]
//        public void Setup()
//        {
//            order_repository = new Mock<IRepository<Order>>();
//            item_repository = new Mock<IRepository<Item>>();
//            shipping_task = new Mock<IShippingTask>();
//            emailing_task = new Mock<IEmailingTask>();

//            order_placing_task = new OrderPlacingTask(
//                order_repository.Object
//                ,item_repository.Object
//                ,shipping_task.Object
//                ,emailing_task.Object);
//        }

//        [Test]
//        public void PlaceAnOrderTest()
//        {
//            var message = new OrderPlacingMessage()
//                              {
//                                  customer_email = "adam@adamaldrich.com",
//                                  customer_name = "Adam Aldrich",
//                                  item_ids = new List<long>(){1,2}
//                              };

//            var order = order_placing_task.place_order(message);                 

//            order_repository.Verify(x=>x.save(It.IsAny<Order>()));
//            Assert.IsNotNull(order);
//            shipping_task.Verify(x=>x.ship(order));
//            emailing_task.Verify(x=>x.send(order));
//        }

//        [Test]
//        public void PlaceAnOrderThatHitsAnErrorTest()
//        {
//            order_repository.Setup(x => x.save(It.IsAny<Order>())).Throws(new ArgumentException());

//            var message = new OrderPlacingMessage()
//            {
//                customer_email = "adam@adamaldrich.com",
//                customer_name = "Adam Aldrich",
//                item_ids = new List<long>() { 1, 2 }
//            };

//            Order order = null;

//            try
//            {
//                order = order_placing_task.place_order(message);
//            }
//            catch (Exception exception)
//            {
//                Assert.IsInstanceOf(typeof(DomainException),exception);
//            }

//            order_repository.Verify(x => x.save(It.IsAny<Order>()));
//            Assert.IsNull(order);
//            shipping_task.Verify(x => x.ship(order),Times.Never());
//            emailing_task.Verify(x => x.send(order),Times.Never());
//        }
//    }
//}
