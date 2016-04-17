using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EssentialTools.Models;
using System.Linq;
using Moq;

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
            /*--------使用moq----------*/
            //模仿对象: IDiscountHelper的实现
            Mock<IDiscountHelper> mock = new Mock<IDiscountHelper>();
            //选择方法
            mock.Setup(m => m.ApplyDiscount(It.IsAny<decimal>())).Returns<decimal>(total => total);
            var target = new LinqValueCalculater(mock.Object);
            var goalTotal = products.Sum(e => e.Price);


            /*------不使用moq---------*/
            //var discounter = new MinumumDiscountHelper();
            //var target = new LinqValueCalculater(discounter);
            //var goalTotal = products.Sum(e => e.Price);

            //动作
            var result = target.ValueProducts(products);

            //断言
            Assert.AreEqual(goalTotal, result);          
        }



        private Product[] createProduct(decimal value)
        {
            return new[] { new Product { Price = value } };
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public void pass_Through_Variable_Discounts()
        {
            //准备
            Mock<IDiscountHelper> mock = new Mock<IDiscountHelper>();
            mock.Setup(m => m.ApplyDiscount(It.IsAny<decimal>())).Returns<decimal>(total => total);
            mock.Setup(m => m.ApplyDiscount(It.Is<decimal>(v => v == 0))).Throws<System.ArgumentOutOfRangeException>();
            mock.Setup(m => m.ApplyDiscount(It.Is<decimal>(v => v>100))).Returns<decimal>(total => total*0.9m);
            mock.Setup(m => m.ApplyDiscount(It.IsInRange<decimal>(10,100,Range.Inclusive))).Returns<decimal>(total => total-5);
            var target = new LinqValueCalculater(mock.Object);

            //动作
            decimal Discount5 = target.ValueProducts(createProduct(5));
            decimal Discount10 = target.ValueProducts(createProduct(10));
            decimal Discount50 = target.ValueProducts(createProduct(50));
            decimal Discount100 = target.ValueProducts(createProduct(100));
            decimal Discount500 = target.ValueProducts(createProduct(500));

            //断言
            Assert.AreEqual(5, Discount5, "$5 Fail");
            Assert.AreEqual(5, Discount10, "$10 Fail");
            Assert.AreEqual(45, Discount50, "$50 Fail");
            Assert.AreEqual(95, Discount100, "$100 Fail");
            Assert.AreEqual(450, Discount500, "$500 Fail");
            target.ValueProducts(createProduct(0));


        }
    }
}
