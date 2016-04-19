using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportStore.Domain.Entities;
using System.Linq;
using Moq;
using SportStore.Domain.Abstract;
using SportStore.WebUI.Controllers;
using System.Web.Mvc;
using SportStore.WebUI.Models;

namespace SportStore.UnitTests
{
    [TestClass]
    public class CartTests
    {
        /// <summary>
        /// 单元测试, 测试购物车---添加新的商品
        /// </summary>
        [TestMethod]
        public void Can_Add_New_Lines()
        {
            //准备---创建一些测试产品
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };
            //准备---创建一个新的购物车
            Cart target = new Cart();

            //动作
            target.AddItem(p1, 1);
            target.AddItem(p2, 4);
            CartLine[] result = target.Lines.ToArray();

            //断言
            Assert.AreEqual(result.Length, 2);
            Assert.AreEqual(result[0].Product, p1);
            Assert.AreEqual(result[1].Product, p2);
            Assert.AreEqual(result[1].Quantity, 4);
        }

        /// <summary>
        /// 单元测试, 测试购物车---添加缘原有的商品
        /// </summary>
        [TestMethod]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            //准备---创建一些测试产品
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };
            //准备---创建一个新的购物车
            Cart target = new Cart();

            //动作
            target.AddItem(p1, 1);
            target.AddItem(p2, 4);
            target.AddItem(p1, 10);
            CartLine[] result = target.Lines.OrderBy(c => c.Product.ProductID).ToArray();

            //断言
            Assert.AreEqual(result.Length, 2);
            Assert.AreEqual(result[0].Quantity, 11);
            Assert.AreEqual(result[1].Quantity, 4);
        }
        /// <summary>
        /// 单元测试, 测试购物车---删除商品
        /// </summary>
        [TestMethod]
        public void Can_Remove_Lines()
        {
            //准备---创建一些测试产品
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };
            Product p3 = new Product { ProductID = 3, Name = "P3" };
            //准备---创建一个新的购物车
            Cart target = new Cart();

            //准备-- 添加一些数据
            target.AddItem(p1, 1);
            target.AddItem(p2, 4);
            target.AddItem(p3, 10);

            //动作--删除一些数据
            target.RemoveLine(p2);

            CartLine[] result = target.Lines.OrderBy(c => c.Product.ProductID).ToArray();

            //断言
            Assert.AreEqual(result.Length, 2);
            Assert.AreEqual(result[0].Product.ProductID,1);
            Assert.AreEqual(result[1].Product.ProductID, 3);
        }


        /// <summary>
        /// 单元测试, 测试购物车---购物车结算
        /// </summary>
        [TestMethod]
        public void Calculate_Cart_Total()
        {
            //准备---创建一些测试产品
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 100m };
            Product p2 = new Product { ProductID = 2, Name = "P2" , Price = 50m};
            //准备---创建一个新的购物车
            Cart target = new Cart();

            //动作
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 3);
            decimal result = target.ComputeTotalValue();

            //断言
            Assert.AreEqual(result, 450m);
        }

        /// <summary>
        /// 控制器单元测试 : 添加到购物车
        /// </summary>
        [TestMethod]
        public void Can_Add_To_Cart()
        {
            //准备---创建模仿存储库
            Mock<IProuctRepository> mock = new Mock<IProuctRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID= 1, Name = "P1", Category = "Apples" }
            }.AsQueryable());
            //准备 --- 创建cart
            Cart cart = new Cart();
            //准备 --- 创建控制器
            CartController target = new CartController(mock.Object);

            //动作---对cart添加一个产品
            target.AddToCart(cart, 1, null);

            //断言
            Assert.AreEqual(cart.Lines.Count(), 1);
            Assert.AreEqual(cart.Lines.ToArray()[0].Product.ProductID, 1);
        }


        /// <summary>
        /// 控制器单元测试 : 添加到购物车并转到新页面
        /// </summary>
        [TestMethod]
        public void Adding_Cart_To_Cart_Goes_To_Cart_Screem()
        {
            //准备---创建模仿存储库
            Mock<IProuctRepository> mock = new Mock<IProuctRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID= 1, Name = "P1", Category = "Apples" }
            }.AsQueryable());
            //准备 --- 创建cart
            Cart cart = new Cart();
            //准备 --- 创建控制器
            CartController target = new CartController(mock.Object);

            //动作---对cart添加一个产品
            RedirectToRouteResult result = target.AddToCart(cart, 2, "MyUrl");

            //断言
            Assert.AreEqual(result.RouteValues["action"], "Index");
            Assert.AreEqual(result.RouteValues["returnUrl"], "MyUrl");
        }

        /// <summary>
        /// 控制器单元测试 : 查看购物车
        /// </summary>
        [TestMethod]
        public void Can_View_Cart_Contents()
        {
            //准备 --- 创建cart
            Cart cart = new Cart();
            //准备 --- 创建控制器
            CartController target = new CartController(null);

            //动作---调用Index方法
            CartIndexViewModel result = (CartIndexViewModel)target.Index(cart, "myUrl").ViewData.Model;


            //断言
            Assert.AreSame(result.Cart, cart);
            Assert.AreEqual(result.ReturnUrl, "myUrl");
        }
    }
}
