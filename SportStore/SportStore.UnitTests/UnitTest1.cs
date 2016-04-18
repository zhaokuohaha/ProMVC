using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportStore.Domain.Abstract;
using SportStore.Domain.Entities;
using SportStore.WebUI.Controllers;
using System.Linq;
using System.Collections.Generic;

namespace SportStore.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Pageinate()
        {
            //准备
            Mock<IProuctRepository> mock = new Mock<IProuctRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "p1" },
                new Product {ProductID = 2, Name = "p2" },
                new Product {ProductID = 3, Name = "p3" },
                new Product {ProductID = 4, Name = "p4" },
                new Product {ProductID = 5, Name = "p5" },
            }.AsQueryable());
            ProductController controller = new ProductController(mock.Object);
            controller.pageSize = 3;

            //动作
            IEnumerable<Product> result = (IEnumerable<Product>)controller.List(2).Model;

            //断言
            Product[] proArray = result.ToArray();
            Assert.IsTrue(proArray.Length == 2);
            Assert.AreEqual(proArray[0].Name, "p4");
            Assert.AreEqual(proArray[1].Name, "p5");
        }
    }
}
