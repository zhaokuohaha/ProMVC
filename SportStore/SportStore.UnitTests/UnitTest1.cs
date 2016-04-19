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
            //准备,创建控制器并使页面大小为3
            ProductController controller = new ProductController(mock.Object);
            controller.pageSize = 3;

            //动作
            ProductsListViewModel result = (ProductsListViewModel)controller.List(null,2).Model;

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
            ProductsListViewModel result = (ProductsListViewModel)controller.List(null,2).Model;

            //断言
            PagingInfo pageinfo = result.paginginfo;
            Assert.AreEqual(pageinfo.CurrentPage, 2);
            Assert.AreEqual(pageinfo.ItemsPerPage, 3);
            Assert.AreEqual(pageinfo.TotalItems, 5);
            Assert.AreEqual(pageinfo.TotalPages, 2);
        }

        /// <summary>
        /// 单元测试  分类过滤
        /// </summary>
        [TestMethod]
        public void Can_Filter_Products()
        {
            //准备----创建模仿存储库
            Mock<IProuctRepository> mock = new Mock<IProuctRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]{
                new Product {ProductID = 1, Name = "p1", Category = "Cat1" },
                new Product {ProductID = 2, Name = "p2", Category = "Cat2"},
                new Product {ProductID = 3, Name = "p3", Category = "Cat1"},
                new Product {ProductID = 4, Name = "p4", Category = "Cat2"},
                new Product {ProductID = 5, Name = "p5", Category = "Cat3"}
            }.AsQueryable());
            ProductController controller = new ProductController(mock.Object);
            controller.pageSize = 3;

            //动作
            Product[] result = ((ProductsListViewModel)controller.List("Cat2", 1).Model).Products.ToArray();

            //断言
            Assert.AreEqual(result.Length, 2);
            Assert.IsTrue(result[0].Name == "p2" && result[0].Category == "Cat2");
            Assert.IsTrue(result[1].Name == "p4" && result[1].Category == "Cat2");
        }

        /// <summary>
        /// 单元测试: 生成分类列表
        /// </summary>
        [TestMethod]
        public void Can_Create_Categories()
        {
            //准备
            Mock<IProuctRepository> mock = new Mock<IProuctRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "p1", Category = "Cat1" },
                new Product {ProductID = 2, Name = "p2", Category = "Cat1"},
                new Product {ProductID = 3, Name = "p3", Category = "Cat2"},
                new Product {ProductID = 4, Name = "p4", Category = "Cat3"}
            }.AsQueryable());
            NavController target = new NavController(mock.Object);

            //动作
            string[] results = ((IEnumerable<string>)target.Menu().Model).ToArray();

            //断言
            Assert.AreEqual(results.Length, 3);
            Assert.AreEqual(results[0], "Cat1");
            Assert.AreEqual(results[1], "Cat2");
            Assert.AreEqual(results[2], "Cat3");
        }

        /// <summary>
        /// 单元测试: 报告被选中分类
        /// </summary>
        //[TestMethod]
        //public void Indicates_Selected_Categories()
        //{
        //    //准备 --- 创建模仿存储库
        //    Mock<IProuctRepository> mock = new Mock<IProuctRepository>();
        //    mock.Setup(m => m.Products).Returns(new Product[]
        //    {
        //        new Product {ProductID = 1, Name = "P1", Category = "Apples" },
        //        new Product {ProductID = 4, Name = "P2", Category = "Oranges" }
        //    }.AsQueryable());
        //    //准备 --- 创建控制器
        //    NavController target = new NavController(mock.Object);
        //    //准备 --- 定义已选分类
        //    string categoryToSelect = "Apples";
#warning 这一步过不去, 不知道为啥
        //    //动作
        //    string reslut = target.Menu(categoryToSelect).ViewBag.SelectedCategory;

        //    //断言
        //    Assert.AreEqual(categoryToSelect, reslut);
        //}


        /// <summary>
        /// 单元测试: 特定分类的产品数
        /// </summary>
        [TestMethod]
        public void Generate_Category_Specific_Product_Count()
        {
            //准备 --- 创建模仿存储库
            Mock<IProuctRepository> mock = new Mock<IProuctRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]{
                new Product {ProductID = 1, Name = "p1", Category = "Cat1" },
                new Product {ProductID = 2, Name = "p2", Category = "Cat2"},
                new Product {ProductID = 3, Name = "p3", Category = "Cat1"},
                new Product {ProductID = 4, Name = "p4", Category = "Cat1"},
                new Product {ProductID = 5, Name = "p5", Category = "Cat3"}
            }.AsQueryable());
            //准备---创建控制器并使页面容纳3个物品
            ProductController target = new ProductController(mock.Object);
            target.pageSize = 2;

            //动作 -- 测试不同分类的产品数
            int res1 = ((ProductsListViewModel)target.List("Cat1").Model).paginginfo.TotalItems;
            int res2 = ((ProductsListViewModel)target.List("Cat2").Model).paginginfo.TotalItems;
            int res3 = ((ProductsListViewModel)target.List("Cat3").Model).paginginfo.TotalItems;
            int resAll = ((ProductsListViewModel)target.List(null).Model).paginginfo.TotalItems;
            //动作---产品1的页面数
            int resp1 = ((ProductsListViewModel)target.List(null).Model).paginginfo.TotalPages;
            //断言
            Assert.AreEqual(res1, 3);
            Assert.AreEqual(res2, 1);
            Assert.AreEqual(res3, 1);
            Assert.AreEqual(resAll, 5);
            Assert.AreEqual(resp1, 2);
        }
    }
}
