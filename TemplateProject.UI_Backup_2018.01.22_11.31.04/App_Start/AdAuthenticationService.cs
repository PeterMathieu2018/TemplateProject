

using System;
using System.DirectoryServices.AccountManagement;
using System.Security.Claims;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using TemplateProject.UI.Models;

namespace TemplateProject.UI.App_Start
{
    public class AdAuthenticationService : IAdAuthenticationService
    {
        private readonly IAuthenticationManager _authenticationManager;

        public AdAuthenticationService(IAuthenticationManager authenticationManager)
        {
            _authenticationManager = authenticationManager;
        }

        public AuthenticationResult SignIn(string username, string password, ContextType contextType,
            string activeDirectoryLocation)
        {
            try
            {
                var principalContext = new PrincipalContext(contextType, activeDirectoryLocation);
                var isAuthenticated = false;
                UserPrincipal userPrincipal = null;
                isAuthenticated = IsAuthenticated(username, password, principalContext, ref userPrincipal);

                AuthenticationResult signIn;
                if (CheckAuthenticationResult(isAuthenticated, userPrincipal, out signIn)) return signIn;

                var identity = CreateIdentity(userPrincipal);
                var groups = userPrincipal.GetAuthorizationGroups();
                foreach (var item in groups)
                {
                    try
                    {
                        Roles.CreateRole(item.Name);

                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception.Message);
                    }
                }
                _authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                _authenticationManager.SignIn(new AuthenticationProperties { IsPersistent = false }, identity);

                return new AuthenticationResult();
            }
            catch (Exception ex)
            {
                return new AuthenticationResult(ex.Message);
            }
        }

        public AuthenticationResult Authenticate(string username, string password, ContextType contextType,
            string activeDirectoryLocation)
        {
            var principalContext = new PrincipalContext(contextType, activeDirectoryLocation);
            var isAuthenticated = false;
            UserPrincipal userPrincipal = null;
            isAuthenticated = IsAuthenticated(username, password, principalContext, ref userPrincipal);

            AuthenticationResult signIn;
            return CheckAuthenticationResult(isAuthenticated, userPrincipal, out signIn)
                ? signIn
                : new AuthenticationResult();
        }

        public void SignOut(string applicationCookie)
        {
            _authenticationManager.SignOut(applicationCookie);
        }

        private ClaimsIdentity CreateIdentity(UserPrincipal userPrincipal)
        {
            var identity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie, ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            identity.AddClaim(
                new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider",
                    "Active Directory"));
            AddNonnullClaims(identity, ClaimTypes.Name, userPrincipal.DisplayName);
            AddNonnullClaims(identity, ClaimTypes.GivenName, userPrincipal.GivenName);
            AddNonnullClaims(identity, ClaimTypes.Surname, userPrincipal.Surname);
            AddNonnullClaims(identity, ClaimTypes.NameIdentifier, userPrincipal.SamAccountName);
            //
            //            identity.AddClaim(new Claim(ClaimTypes.Name, userPrincipal.DisplayName));
            //            identity.AddClaim(new Claim(ClaimTypes.GivenName, userPrincipal.GivenName));
            //            identity.AddClaim(new Claim(ClaimTypes.Surname, userPrincipal.Surname));
            //            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userPrincipal.SamAccountName));

            if (!string.IsNullOrEmpty(userPrincipal.EmailAddress))
            {
                identity.AddClaim(new Claim(ClaimTypes.Email, userPrincipal.EmailAddress));
            }

            // add your own claims if you need to add more information stored on the cookie

            return identity;
        }

        private void AddNonnullClaims(ClaimsIdentity identity, string claimType, string claim)
        {
            if (!string.IsNullOrEmpty(claim))
            {
                identity.AddClaim(new Claim(claimType, claim));
            }
        }

        private static bool CheckAuthenticationResult(bool isAuthenticated, UserPrincipal userPrincipal,
            out AuthenticationResult signIn)
        {
            if (!isAuthenticated || userPrincipal == null)
            {
                {
                    signIn = new AuthenticationResult("Username or Password is not correct");
                    return true;
                }
            }

            if (userPrincipal.IsAccountLockedOut())
            {
                // here can be a security related discussion weather it is worth 
                // revealing this information
                {
                    signIn = new AuthenticationResult("Your account is locked.");
                    return true;
                }
            }

            if (userPrincipal.Enabled.HasValue && userPrincipal.Enabled.Value == false)
            {
                // here can be a security related discussion weather it is worth 
                // revealing this information
                {
                    signIn = new AuthenticationResult("Your account is disabled");
                    return true;
                }
            }
            signIn = new AuthenticationResult();
            return false;
        }

        private static bool IsAuthenticated(string username, string password,
            PrincipalContext principalContext, ref UserPrincipal userPrincipal)
        {
            var isAuthenticated = false;
            try
            {
                isAuthenticated = principalContext.ValidateCredentials(username, password);
                if (isAuthenticated)
                {
                    userPrincipal = UserPrincipal.FindByIdentity(principalContext, username);
                }
            }
            catch (Exception)
            {
                isAuthenticated = false;
                userPrincipal = null;
            }
            return isAuthenticated;
        }
    }
}