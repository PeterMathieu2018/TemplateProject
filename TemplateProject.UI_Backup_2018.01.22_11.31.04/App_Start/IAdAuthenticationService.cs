using System.DirectoryServices.AccountManagement;
using TemplateProject.UI.Models;

namespace TemplateProject.UI.App_Start
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