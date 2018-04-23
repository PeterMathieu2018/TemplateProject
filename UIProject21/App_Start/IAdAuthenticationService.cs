using System.DirectoryServices.AccountManagement;
using UIProject21.Models;

namespace UIProject21.App_Start
{
    public interface IAdAuthenticationService
    {
        AuthenticationResult SignIn(string username, string password, ContextType contextType,
           string activeDirectoryLocation);
        AuthenticationResult Authenticate(string username, string password, ContextType contextType,
            string activeDirectoryLocation);
        void SignOut(string applicationCookie);
    }
}