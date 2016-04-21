namespace SportStore.WebUI.Infrastructure.Abstrat
{
    public interface IAuthProvider
    {
        bool Authenticate(string username, string password);
    }
}