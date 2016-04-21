using SportStore.Domain.Entities;
using SportStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportStore.WebUI.Controllers
{
    public class AdminController : Controller
    {
        /// <summary>
        /// 产品上下文----事实上应该可以这么理解
        /// </summary>
        private IProuctRepository repository;
        public AdminController(IProuctRepository ip)
        {
            this.repository = ip;
        }
        
        /// <summary>
        /// 主页
        /// </summary>
        /// <returns></returns>
        public ViewResult Index()
        {
            return View(repository.Products);
        }

        /// <summary>
        /// 返回编辑界面,页面中会有保存按钮
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public ViewResult Edit(int productId)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productId);
            return View(product);
        }

        /// <summary>
        /// 执行保存---接收编辑页面的post请求
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(Product procduct)
        {
            if (ModelState.IsValid)
            {
                repository.SaveProduct(procduct);
                TempData["message"] = string.Format("{0} has been saved", procduct.Name);
                return RedirectToAction("Index");
            }
            else//数据有误
            {
                return View(procduct);
            }
        }


        public ViewResult Create()
        {
            return View("Edit", new Product());
        }


        public ActionResult Delete(int ProductId)
        {
            Product deleteedProduct = repository.DeleteProduct(ProductId);
            if(deleteedProduct != null)
            {
                TempData["message"] = string.Format("{0} was deleted", deleteedProduct.Name);
            }
            return RedirectToAction("Index");
        }
    }
}