using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportStore.WebUI.Controllers;
using SportStore.WebUI.Infrastructure.Abstrat;
using SportStore.WebUI.Models;
using System.Web.Mvc;


namespace SportStore.UnitTests
{
    [TestClass]
    public  class AdminSecurityTest
    {
        [TestMethod]
        public void Can_Login_With_Valid_Credentials()
        {
            //准备 --- 创建模仿认证提供器
            Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
            mock.Setup(m => m.Authenticate("admin", "admin")).Returns(true);
            //准备 --- 创建视图模型
            LoginViewModelcs model = new LoginViewModelcs
            {
                UserName = "admin",
                Password = "admin"
            };
            //准备 --- 创建控制器
            AccountController target = new AccountController(mock.Object);

            //动作 --- 使用合法的凭证认证
            ActionResult result = target.Login(model, "/MyURL");

            //断言
            Assert.IsInstanceOfType(result, typeof(RedirectResult));
            Assert.AreEqual("/MyURL", ((RedirectResult)result).Url);
        }


        [TestMethod]
        public void Cannot_Login_With_InValid_Credentials()
        {
            //准备 --- 创建模仿认证提供器
            Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
            mock.Setup(m => m.Authenticate("baduser", "badpassowrd")).Returns(false);
            //准备 --- 创建视图模型
            LoginViewModelcs model = new LoginViewModelcs
            {
                UserName = "baduser",
                Password = "badpassword"
            };
            //准备 --- 创建控制器
            AccountController target = new AccountController(mock.Object);

            //动作 --- 使用合法的凭证认证
            ActionResult result = target.Login(model, "/MyURL");

            //断言
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsFalse(((ViewResult)result).ViewData.ModelState.IsValid);
        }
    }
}
