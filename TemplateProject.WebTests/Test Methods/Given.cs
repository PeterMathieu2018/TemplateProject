using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace TemplateProject.WebTests.Test_Methods
{
    [Binding]
    public partial class TestMethods : BaseWebTest
    {
        public TestMethods(TestConfiguration config) : base(config)
        {
        }

        [Given("I am on page (.*)")]
        public void GivenIAmOnPage(string url)
        {
            //Driver.Manage().Window.Size();
            Driver.Navigate().GoToUrl(BaseUrl + url);
            TakeScreenShot();
        }
    }
}
