using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EssentialTools.Models;
using System.Linq;

namespace EssentialTools.Tests
{
    [TestClass]
    public class UnitTest2
    {
        Product[] products = {
                    new Product{Name = "Kayak", Category = "Watersports", Price = 1M},
                    new Product{Name = "Lifejacket", Category = "Watersports", Price = 2M},
                    new Product{Name = "SoccerBall", Category = "Soccer", Price = 3M},
                    new Product{Name = "CornerFalg", Category = "Soccer", Price = 4M},
            };
        [TestMethod]
        public void Sum_Products_Correctly()
        {
            //准备
            var discounter = new MinumumDiscountHelper();
            var target = new LinqValueCalculater(discounter);
            var goalTotal = products.Sum(e => e.Price);

            //动作
            var result = target.ValueProducts(products);

            //断言
            Assert.AreEqual(goalTotal, result);          
        }
    }
}
