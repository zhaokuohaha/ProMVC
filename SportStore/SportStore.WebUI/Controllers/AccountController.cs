using SportStore.WebUI.Infrastructure.Abstrat;
using SportStore.WebUI.Models;
using System.Web.Mvc;

namespace SportStore.WebUI.Controllers
{
    public class AccountController : Controller
    {
        IAuthProvider authProvider;

        public AccountController(IAuthProvider auth)
        {
            this.authProvider = auth;
        }

        public  ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModelcs model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if(authProvider.Authenticate(model.UserName, model.Password))
                {
                    return Redirect(returnUrl ?? Url.Action("Index", "Admin"));
                }
                else
                {
                    ModelState.AddModelError("", "用户名或密码不正确");
                    return View();
                }
            }
            else
            {
                return View("Check");
            }
        }
    }
}