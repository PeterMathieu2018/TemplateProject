using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace TemplateProject.WebTests.Test_Methods
{
    public partial class TestMethods : BaseWebTest
    {
        [Given("Username (.*) logged in with password (.*)")]
        public void GivenUserLoggedIn(string username, string password)
        {
            GivenIAmOnPage("Account/Login");
            WhenTextEntered(username, "UserName");
            WhenTextEntered(password, "Password");
            WhenElementIdClicked("submit");
            ThenThePageShouldBeDisplayed("");
        }
    }
}
