using System.Web.Security;
using SportStore.WebUI.Infrastructure.Abstrat;

namespace SportStore.WebUI.Infrastructure.Concrete
{
    public class FoemAuthProvider : IAuthProvider
    {
        public bool Authenticate(string username, string password)
        {
            bool result = FormsAuthentication.Authenticate(username, password);
            if (result)
            {
                FormsAuthentication.SetAuthCookie(username, false);
            }
            return result;
        }
    }
}