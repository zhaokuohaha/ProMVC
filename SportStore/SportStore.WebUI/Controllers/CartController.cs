using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportStore.Domain.Entities;
using SportStore.Domain.Abstract;
using SportStore.WebUI.Models;

namespace SportStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        /// <summary>
        /// 商品集合
        /// </summary>
        private IProuctRepository repository;
        
        public CartController(IProuctRepository repository, IOrderProcesser proc)
        {
            this.repository = repository;
            this.orderProcessor = proc;
        }

        ///// <summary>
        ///// 读取购物车, 首先从Session读取, 读不到则创建一个并写入Session
        ///// </summary>
        ///// <returns></returns>
        //private Cart GetCart()
        //{
        //    Cart cart = (Cart)Session["Cart"];
        //    if(cart == null)
        //    {
        //        cart = new Cart();
        //        Session["Cart"] = cart;
        //    }
        //    return cart;
        //}

        /// <summary>
        /// 添加到购物车
        /// </summary>
        /// <param name="productId">商品id</param>
        /// <param name="returnUrl">重定向Url</param>
        /// <returns></returns>
        public RedirectToRouteResult AddToCart(Cart cart, int productId, string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productId);
            if(product != null)
            {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        /// <summary>
        /// 从购物车中删除商品
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="productId">商品id</param>
        /// <param name="returnUrl">执行完成后返回的链接</param>
        /// <returns></returns>
        public RedirectToRouteResult RemoveFromCart(Cart cart,  int productId, string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productId);
            if(product != null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        /// <summary>
        /// Index 控制页面跳转
        /// </summary>
        /// <param name="cart">购物车</param>
        /// <param name="returnUrl">重定向路由</param>
        /// <returns></returns>
        public ViewResult Index(Cart cart,  string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }
        
        /// <summary>
        /// 查看购物车
        /// </summary>
        /// <param name="cart">购物车</param>
        /// <returns></returns>
        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        /// <summary>
        /// 提交订单第一步---检查用户信息
        /// </summary>
        /// <returns></returns>
        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }


        //提交订单第二步:  发邮件
        private IOrderProcesser orderProcessor;
        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if(cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry Your Cart Is Empty!");
            }

            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        }
    }
}