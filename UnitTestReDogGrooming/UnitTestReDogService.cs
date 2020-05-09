using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestReDogGrooming
{
    [TestClass]
    public class UnitTestReDogService
    {
        [TestMethod]
        public void TestAdd()
        {
            Service S1 = new Service() { Code = "AAA45", Description = "Wash&dry", Price = 20 };
            Service S2 = new Service() { Code = "TSc56", Description = "Dry Cut", Price = 6 };
            Basket basket = new Basket();
            basket.AddService(S1);
            basket.AddService(S1);
            basket.AddService(S2);        // quantity == 2


            List<CartService> items = new List<CartService>(basket.servType);
            Assert.AreEqual(items.Count, 2);
            Assert.AreEqual(items[0].Quantity, 2);
            Assert.AreEqual(items[1].Quantity, 1);
        }

        [TestMethod]
        public void TestRemove()
        {
            Service S1 = new Service() { Code = "AAA45", Description = "Wash&dry", Price = 20 };
            Service S2 = new Service() { Code = "TSc56", Description = "Dry Cut", Price = 6 };
            Basket basket = new Basket();
            basket.AddService(S1);
            basket.AddService(S1);
            basket.AddService(S1);
            basket.AddService(S2);
            basket.RemoveService(S2);
            basket.RemoveService(S1);

            List<CartService> items = new List<CartService>(basket.servType);
            Assert.AreEqual(items.Count, 1);
            Assert.AreEqual(items[0].Quantity, 2);
            Assert.AreEqual(items[0].Code, S1.Code);
        }



        [TestMethod]
        public void TestotalPrice()
        {
            // test total price for all services
            Service S1 = new Service() { Code = "AAA45", Description = "Wash&dry", Price = 20 };
            Service S2 = new Service() { Code = "TSc56", Description = "Dry Cut", Price = 6 };
            Service S3 = new Service() { Code = "FTR67", Description = "Tick Removal", Price = 15 };
            Basket cart = new Basket();
            cart.AddService(S1);
            cart.AddService(S2);
            cart.AddService(S2);
            cart.AddService(S3);

            Assert.AreEqual(cart.CalculateTotalPrice(), S1.Price + S2.Price * 2 + S3.Price);
        }

        [TestMethod]
        public void TestApplyDiscount()
        {
            // test logic to add discount for 2 dogs 
            applyDiscount S1 = new applyDiscount() { Code = "AAA45", Description = "Wash&dry", Price = 20, NoOfDogs=2, discount= 25};
            applyDiscount S2 = new applyDiscount() { Code = "TSc56", Description = "Dry Cut", Price = 6, NoOfDogs=1, discount=0 };
            Basket cart = new Basket();
            cart.AddService(S1);
            cart.AddService(S2);
            cart.AddService(S2);
            double original = S1.Getprice();
            S1.applyintDiscount(1);
            Assert.AreEqual(24, S1.Getprice(), S2.Getprice());
        }
    }
}
    

