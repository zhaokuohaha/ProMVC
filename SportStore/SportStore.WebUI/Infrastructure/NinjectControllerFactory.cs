using Ninject;
using System;
using System.Web.Mvc;
using System.Web.Routing;
using SportStore.Domain.Contrete;
using SportStore.Domain.Abstract;
using SportStore.Domain.Entities;
using System.Configuration;

namespace SportStore.WebUI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernal;
        //初始化, 新建注入内核, 添加绑定
        public NinjectControllerFactory()
        {
            ninjectKernal = new StandardKernel();
            AddBindings();
        }


        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)ninjectKernal.Get(controllerType);
        }

        /// <summary>
        /// 添加绑定
        /// </summary>
        private void AddBindings()
        {
            ninjectKernal.Bind<IProuctRepository>().To<EFProductRepository>();
            EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
            };
            ninjectKernal.Bind<IOrderProcesser>().To<EmailOrderProcessor>().WithConstructorArgument("settings", emailSettings);
            //Mock<IProuctRepository> mock = new Mock<Domain.Abstract.IProuctRepository>();
            //mock.Setup(m => m.Products).Returns(new List<Product>
            //{
            //    new Product {Name = "Football",Price=25 },
            //    new Product {Name = "Surf board",Price=179 },
            //    new Product {Name = "Running shoes",Price=95 },
            //}.AsQueryable());
            //ninjectKernal.Bind<IProuctRepository>().ToConstant(mock.Object);
        }
    }
}