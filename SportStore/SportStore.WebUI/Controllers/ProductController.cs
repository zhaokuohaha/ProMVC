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
        public ViewResult List(int page = 1)
        {
            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = repository.Products
                    .OrderBy(p => p.ProductID)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize),
                paginginfo = new PagingInfo
                {
                    CurrentPage = page,
                    TotalItems = repository.Products.Count(),
                    ItemsPerPage = pageSize
                }
            };
            return View(model);
        }
    }
}