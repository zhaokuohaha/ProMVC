﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EssentialTools.Models;
using Ninject;

namespace EssentialTools.Controllers
{
    public class HomeController : Controller
    {
        Product[] products = {
                    new Product{Name = "Kayak", Category = "Watersports", Price = 1M},
                    new Product{Name = "Lifejacket", Category = "Watersports", Price = 2M},
                    new Product{Name = "SoccerBall", Category = "Soccer", Price = 3M},
                    new Product{Name = "CornerFalg", Category = "Soccer", Price = 4M},
                                 };
        /// <summary>
        /// IValueCalculator实例变量, 在整个控制器类中用来表示构造器所接收的IValueCalculator
        /// </summary>
        private IValueCalculator calc;
        public HomeController(IValueCalculator calc)
        {
            this.calc = calc;
        }
        // GET: Home
        public ActionResult Index()
        {
            //    //创建Ninject内核
            //    IKernel ninjectKernel = new StandardKernel();
            //    //绑定类
            //    ninjectKernel.Bind<IValueCalculator>().To<LinqValueCalculater>();
            //    //实际使用Ninject
            //    IValueCalculator calc = ninjectKernel.Get<IValueCalculator>();

            //IValueCalculator calc = new LinqValueCalculater();//向下转型
            ShoppingCart cart = new ShoppingCart(calc) { Products = products };
            decimal totalValue = cart.CalulaterProductTotal();
            return View(totalValue);
        }
    }
}