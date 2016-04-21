using System.Web.Security;
using SportStore.WebUI.Infrastructure.Abstrat;

namespace SportStore.WebUI.Infrastructure.Concrete
{
    public class FoemAuthProvider : IAuthProvider
    {
        public bool Authenticate(string username, string password)
        {
#pragma warning disable CS0618 // 类型或成员已过时
            bool result = FormsAuthentication.Authenticate(username, password);
#pragma warning restore CS0618 // 类型或成员已过时
            if (result)
            {
                FormsAuthentication.SetAuthCookie(username, false);
            }
            return result;
        }
    }
}