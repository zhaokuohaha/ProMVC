using languageFeatures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace languageFeatures.Controllers
{
    public class HomeController : Controller
    {
        //Index动作
        public string Index()
        {
            return "Navigate wo a URL to show an example";
        }

        /// <summary>
        /// 设置属性值 -----   繁琐, 如果有多个属性的话
        /// </summary>
        /// <returns></returns>
        public ViewResult AutoProperty()
        {
            Product myProduct = new Product();
            myProduct.Name = "张三";
            string productName = myProduct.Name;
            return View("Result", (object)String.Format("Product Name : {0}", productName));
        }

        /// <summary>
        /// 创建一个产品对象  --- 使用对象初始化器
        /// </summary>
        /// <returns></returns>
        public ViewResult CreateProduct()
        {
            Product myProduct = new Product
            {
                ProductID = 100,
                Name = "皮划艇",
                Description = "一种个人用的小船",
                price = 275M,
                Category = "水上运动"
            };
            return View("Result", (object)String.Format("Category: {0}", myProduct.Category));
        }

        /// <summary>
        /// 初始化集合
        /// </summary>
        /// <returns></returns>
        public ViewResult CreateCollection()
        {
            string[] stringArray = { "苹果", "橙子", "李子"};
            List<int> intList = new List<int> { 10, 20, 30, 40 };
            Dictionary<string, int> myDict = new Dictionary<string, int>
            {
                {"apple",10 }, {"orange",20 }, {"plum",30 }
            };
            return View("Result", (object)stringArray[1]);
        }

        /// <summary>
        /// 使用扩展方法
        /// </summary>
        /// <returns></returns>
        public ViewResult UseExtension()
        {
            //创建并填充ShoppingCar
            ShoppingCart cart = new ShoppingCart
            {
                Products = new List<Product>{
                    new Product{Name = "Kayak", Price = 275M},//皮划艇
					new Product{Name = "Lifejacket", Price = 48.95M},//休闲夹克
					new Product{Name = "SoccerBall", Price = 19.50M},//足球
					new Product{Name = "CornerFlag", Price = 34.95M}//角旗
				}
            };
            decimal cartTotal = cart.TotalPrices();
            return View("Result", (object)String.Format("Total: {0:c}", cartTotal));
        }
    }
}