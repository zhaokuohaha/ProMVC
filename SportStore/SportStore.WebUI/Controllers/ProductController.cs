using System.Linq;
using System.Web.Mvc;
using SportStore.Domain.Abstract;
namespace SportStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProuctRepository repository;
        public int pageSize = 4;
        public ProductController(IProuctRepository repository)
        {
            this.repository = repository;
        }
        public ViewResult List(int page = 1)
        {
            return View(repository.Products.OrderBy(p=>p.ProductID).Skip((page -1)*pageSize).Take(pageSize));
        }
    }
}