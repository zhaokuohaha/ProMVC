using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportStore.Domain.Abstract;
using SportStore.Domain.Entities;
using SportStore.WebUI.Controllers;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using SportStore.WebUI.Models;
using SportStore.WebUI.HtmlHelpers;

namespace SportStore.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// 单元测试:分页
        /// </summary>
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
            ProductsListViewModel result = (ProductsListViewModel)controller.List(2).Model;

            //断言
            Product[] proArray = result.Products.ToArray();
            Assert.IsTrue(proArray.Length == 2);
            Assert.AreEqual(proArray[0].Name, "p4");
            Assert.AreEqual(proArray[1].Name, "p5");
        }

        /// <summary>
        /// 单元测试:创建页面链接
        /// </summary>
        [TestMethod]
        public void Can_Generate_Page_Links()
        {
            //准备:定义一个Html辅助器, 为了运用扩展方法,需要这样
            HtmlHelper myHelper = null;
            //准备:创建PagingInfo数据
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };
            //准备:用lambda表达式建立委托\
            Func<int, string> pageUrlDelegate = i => "Page" + i;

            //动作
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            //断言
            Assert.AreEqual(result.ToString(),
                @"<a href=""Page1"">1</a>"
                    +@"<a class=""selected"" href=""Page2"">2</a>"
                    +@"<a href=""Page3"">3</a>");
        }

        /// <summary>
        /// 单元测试: 页面模型视图数据
        /// </summary>
        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            //准备
            Mock<IProuctRepository> mock = new Mock<IProuctRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "p1" },
                new Product {ProductID = 2, Name = "p2" },
                new Product {ProductID = 3, Name = "p3" },
                new Product {ProductID = 4, Name = "p4" },
                new Product {ProductID = 5, Name = "p5" }
            }.AsQueryable());
            ProductController controller = new ProductController(mock.Object);
            controller.pageSize = 3;

            //动作
            ProductsListViewModel result = (ProductsListViewModel)controller.List(2).Model;

            //断言
            PagingInfo pageinfo = result.paginginfo;
            Assert.AreEqual(pageinfo.CurrentPage, 2);
            Assert.AreEqual(pageinfo.ItemsPerPage, 3);
            Assert.AreEqual(pageinfo.TotalItems, 5);
            Assert.AreEqual(pageinfo.TotalPages, 2);
        }
    }
}
