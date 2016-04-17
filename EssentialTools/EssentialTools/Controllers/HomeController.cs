using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EssentialTools.Models;

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
        // GET: Home
        public ActionResult Index()
        {
            LinqValueCalculater calc = new LinqValueCalculater();
            ShoppingCart cart = new ShoppingCart(calc) { Products = products };
            decimal totalValue = cart.CalulaterProductTotal();
            return View(totalValue);
        }
    }
}