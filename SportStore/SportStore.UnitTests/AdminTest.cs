using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportStore.Domain.Abstract;
using SportStore.Domain.Entities;
using SportStore.WebUI.Controllers;
using System.Web.Mvc;

namespace SportStore.UnitTests
{
    [TestClass]
    public class AdminTest
    {
        /// <summary>
        /// 管理员是否能返回所有的产品Product 对象
        /// </summary>
        [TestMethod]
        public void Index_Contains_All_Products()
        {
            //准备---创建模仿存储库
            Mock<IProuctRepository> mock = new Mock<IProuctRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1" },
                new Product {ProductID = 2, Name = "P2" },
                new Product {ProductID = 3, Name = "P3" }
            }.AsQueryable());
            //准备---创建控制器
            AdminController target = new AdminController(mock.Object);


            //动作
            Product[] result = ((IEnumerable<Product>)target.Index().ViewData.Model).ToArray();

            //断言
            Assert.AreEqual(result.Length, 3);
        }

        /// <summary>
        /// 测试Edit方法: id有效时, 能获取所查找的产品, id无效时--找不大产品
        /// </summary>
        [TestMethod]
        public void Can_Edit_Product()
        {
            //准备---创建模仿存储库
            Mock<IProuctRepository> mock = new Mock<IProuctRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {Name = "P1", ProductID = 1 }
            }.AsQueryable());
            //准备--创建控制器
            AdminController target = new AdminController(mock.Object);

            //动作-- 执行Edit方法
// 这两行换过来就测试不通过====
            Product p2 = (Product)target.Edit(2).ViewData.Model;
            Product p1 = target.Edit(1).Model as Product;
            //断言
            Assert.AreEqual(1, p1.ProductID);
            Assert.IsNull(p2);
        }

        /*测试Edit提交---确保对模型绑定器创建的Product对象所有的有效更新被传递给产品存储库进行
         保存, 还要检查非法更新---模型错误(不会被传递给存储库*/
        [TestMethod]
        public void Can_Save_Valid_Changs()
        {
            //准备---创建模仿存储库
            Mock<IProuctRepository> mock = new Mock<IProuctRepository>();
            //准备---创建控制器
            AdminController target = new AdminController(mock.Object);
            //准备---创建一个产品
            Product product = new Product { Name = "TestSuccess" };
            

            //动作---试着保存这个东西
            ActionResult result = target.Edit(product);

            //断言
            //---调用了存储库
            mock.Verify(m => m.SaveProduct(product));
            //---检查方法的结果类型
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Cannot_Save_InValid_Changs()
        {
            //准备---创建模仿存储库
            Mock<IProuctRepository> mock = new Mock<IProuctRepository>();
            //准备---创建控制器
            AdminController target = new AdminController(mock.Object);
            //准备---创建一个产品
            Product product = new Product { Name = "TestSuccess" };
            //准备---把一个错误添加到模型状态
            target.ModelState.AddModelError("error", "error");

            //动作---试着保存这个东西
            ActionResult result = target.Edit(product);

            //断言
            //---存储库未被调用
            mock.Verify(m => m.SaveProduct(It.IsAny<Product>()),Times.Never());
            //---检查方法的结果类型---ViewResult
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }


        //测试 删除产品---
        //Idy有效: 调用存储库的DeleteProduct方法, 把相应的对象删除
        [TestMethod]
        public void Can_Delete_Valid_Products()
        {
            //准备---创建一个产品
            Product prod = new Product { ProductID = 2, Name = "Test" };
            //准备---创建模仿存储库
            Mock<IProuctRepository> mock = new Mock<IProuctRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1" },
                new Product {ProductID = 2, Name = "P2" },
            }.AsQueryable());
            //准备---创建控制器
            AdminController target = new AdminController(mock.Object);

            //动作---删除商品
            target.Delete(prod.ProductID);

            //断言---确保存储库的删除方法是针对正确的产盘被调用的
            mock.Verify(m => m.DeleteProduct(prod.ProductID));

        }
    }
}
