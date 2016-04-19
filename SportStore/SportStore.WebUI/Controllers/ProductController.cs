using System.Linq;
using System.Web.Mvc;
using SportStore.Domain.Abstract;
using SportStore.WebUI.Models;

namespace SportStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProuctRepository repository;
        public int pageSize = 2;
        public ProductController(IProuctRepository repository)
        {
            this.repository = repository;
        }
        /// <summary>
        /// 返回产品列表
        /// </summary>
        /// <param name="category">产品分类, 默认为所有分类</param>
        /// <param name="page">当前页面</param>
        /// <returns></returns>
        public ViewResult List(string category, int page = 1)
        {
            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = repository.Products
                    .Where(p => category == null || p.Category == category)
                    .OrderBy(p => p.ProductID)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize),
                paginginfo = new PagingInfo
                {
                    CurrentPage = page,
                    TotalItems = repository.Products.Count(),
                    ItemsPerPage = pageSize
                },
                CurrentCategory = category
            };
            return View(model);
        }
    }
}