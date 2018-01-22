using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace TemplateProject.WebTests.Test_Methods
{
    public partial class TestMethods : BaseWebTest
    {
        [Then(@"numeric with id (.*) has value (.*)")]
        public void ThenNumericWithIdShouldHaveValue(string inputId, string value)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(PageTimeOut));
            wait.Until(ExpectedConditions.ElementExists(By.Id(inputId)));

            var js = ((IJavaScriptExecutor)Driver);

            var numericText =
                (string)
                    js.ExecuteScript(string.Format("return $('#{0}').data('kendoNumericTextBox').value();", inputId))
                        .ToString();

            TakeScreenShot();

            Assert.AreEqual(numericText, value);
        }

        [Then(@"dropdown with id (.*) has (.*)")]
        public void ThenDropDownHasOptions(string dropdownId, string options)
        {

            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(PageTimeOut));
            wait.Until(ExpectedConditions.ElementExists(By.Id(dropdownId)));

            var dropdowns = Driver.FindElements(By.CssSelector("span.k-widget"));

            var dropdown =
                dropdowns.FirstOrDefault(i => i.FindElement(By.TagName("input")).GetAttribute("id") == dropdownId);

            dropdown?.Click();

            TakeScreenShot();

            //Cannot find way to wait for databound.
            //var js = ((IJavaScriptExecutor)Driver);
            //var uls =js.ExecuteScript(string.Format("return $('#{0}').data('kendoDropDownList').items();", dropdownId));

            ////var dropdownOptions = uls[0].Text.Replace("\r\n",",").Split(',');

            //var parseOptions = options.Split(',').ToList();

            //var count = 0;
            ////foreach (var d in dropdownOptions)
            ////{
            ////    count += parseOptions.Count(p => d == p);
            ////}

            // Assert.IsTrue(count==parseOptions.Count);

        }

        [Then(@"element (.*) has class (.*)")]
        public void ThenElementHasClass(string elementId, string className)
        {
            var element = Driver.FindElement(By.Id(elementId));
            TakeScreenShot();
            Assert.IsTrue(element.GetAttribute("class").Contains(className));
        }

        [Then(@"element with class (.*) is visible")]
        public void ThenElementWithClassIsVisible(string className)
        {
            WaitElementClassVisible(className);

            var element = Driver.FindElement(By.ClassName(className));

            TakeScreenShot();

            Assert.IsTrue(element.Displayed);
        }

        [Then(@"element with id (.*) is visible")]
        public void ThenElementWithIdIsVisible(string elementId)
        {
            WaitElementIdVisible(elementId);

            var element = Driver.FindElement(By.Id(elementId));

            TakeScreenShot();

            Assert.IsTrue(element.Displayed);
        }

        [Then(@"element with id (.*) has value (.*)")]
        public void ThenElementWithIdHasValue(string elementId, string text)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(PageTimeOut));
            wait.Until(ExpectedConditions.ElementExists(By.Id(elementId)));

            TakeScreenShot();
            var element = Driver.FindElement(By.Id(elementId));
            Assert.AreEqual(element.GetAttribute("value").Trim(), text.Trim());
        }

        [Then(@"element with id (.*) has text (.*)")]
        public void ThenElementWithIdHasText(string elementId, string text)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(PageTimeOut));
            wait.Until(ExpectedConditions.ElementExists(By.Id(elementId)));

            TakeScreenShot();

            var element = Driver.FindElement(By.Id(elementId));
            Assert.AreEqual(element.Text.Trim(), text.Trim());
        }

        [Then(@"element with class (.*) contains text (.*)")]
        public void ThenElementWithClassContainsText(string className, string text)
        {
            WaitElementClassExist(className);

            TakeScreenShot();

            var element = Driver.FindElement(By.ClassName(className));
            Assert.IsTrue(element.Text.Contains(text.Trim()));
        }

        [Then(@"element with id (.*) has attribute (.*) with value (.*)")]
        public void ThenElementWithIdDisabled(string elementId, string attribute, string value)
        {
            WaitElementIdExist(elementId);

            var element = Driver.FindElement(By.Id(elementId));

            TakeScreenShot();

            var attrib = element?.GetAttribute(attribute);

            Assert.IsTrue(element?.GetAttribute(attribute) == value);

        }

        [Then(@"the page (.*) should be displayed")]
        public void ThenThePageShouldBeDisplayed(string url)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(PageTimeOut));
            wait.Until(ExpectedConditions.UrlContains(url));
            TakeScreenShot();
            Assert.IsTrue(Driver.Url.Contains(BaseUrl + url));
        }

        [Then(@"the dropdown (.*) has selected value (.*)")]
        public void ThenDropDownHasSelectedValue(string dropdownId, string value)
        {
            Thread.Sleep(500);

            var js = ((IJavaScriptExecutor)Driver);

            var selectedOption =
                (string)
                    js.ExecuteScript(string.Format(" return $('#{0}').data('kendoDropDownList').text();", dropdownId));

            TakeScreenShot();

            Assert.AreEqual(selectedOption, value);
        }

        [Then(@"the (.*) column in (.*) row in (.*) grid should be (.*)")]
        public void ThenTheColumnInRowGridShouldBe(int columnNumber, int rowNumber, string gridId, string value)
        {
            Wait.Until(ExpectedConditions.ElementExists(By.Id(gridId)));

            Thread.Sleep(3000);

            var grid = Driver.FindElement(By.Id(gridId));

            Wait.Until(i => grid.Displayed);

            var rows = grid.FindElements(By.TagName("tr"));

            var row = rows[rowNumber];

            var columns = row.FindElements(By.TagName("td"));

            var column = columns[columnNumber];

            TakeScreenShot();

            //Allow for comaprison of current date.
            if (value.ToLower() == "currentdate")
            {
                value = DateTime.Now.ToString("MM/dd/yyyy");
            }

            Assert.AreEqual(column.Text.Trim(), value);
        }

        [Then(@"the (.*) column in row containing (.*) text in (.*) grid should be (.*)")]
        public void ThenColumnInRowContainingTextInGridShouldBe(int column, string text, string gridId, string value)
        {
            WaitElementIdExist(gridId);

            Thread.Sleep(3000);

            var grid = Driver.FindElement(By.Id(gridId));

            var tbody = grid.FindElement(By.TagName("tbody"));

            var testRow = tbody.FindElement(By.CssSelector("tr"));

            Wait.Until(i => testRow.Displayed);

            var rows = grid.FindElements(By.TagName("tr"));

            IWebElement gridRow = rows.FirstOrDefault(row => row.FindElements(By.TagName("td")).Select(i => i.Text).Contains(text));

            var gridColumn = gridRow.FindElements(By.TagName("td"))[column];

            TakeScreenShot();

            Assert.AreEqual(gridColumn.Text.Trim(), value);
        }

        [Then(@"the (.*) grid contains column (.*)")]
        public void ThenTheGridContainsHeader(string gridId, string header)
        {
            WaitElementIdExist(gridId);

            Thread.Sleep(3000);

            var grid = Driver.FindElement(By.Id(gridId));

            var tbody = grid.FindElement(By.TagName("tbody"));

            var testRow = tbody.FindElement(By.CssSelector("tr"));

            Wait.Until(i => testRow.Displayed);

            var rows = grid.FindElements(By.TagName("tr"));

            var row = rows[0];

            TakeScreenShot();

            Assert.IsTrue(row.Text.Contains(header));
        }

        [Then(@"element with class (.*) has attribute (.*) equal to (.*) in (.*) grid with row with text (.*)")]
        public void ThenElementWithClassHasAttributeInGridWithRowWithText(string className, string attribute, string value, string gridId, string text)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(PageTimeOut));
            wait.Until(ExpectedConditions.ElementExists(By.Id(gridId)));
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector("div.k-window")));
            wait.Until(ExpectedConditions.ElementExists(By.ClassName(className)));

            var grid = Driver.FindElement(By.Id(gridId));

            var rows = grid.FindElements(By.TagName("tr"));

            IWebElement gridRow = rows.FirstOrDefault(row => row.FindElements(By.TagName("td")).Select(i => i.Text).Contains(text));

            var element = gridRow?.FindElement(By.ClassName(className));

            var attrib = element?.GetAttribute(attribute);

            Assert.IsTrue(element?.GetAttribute(attribute) == value);
        }

        [Then(@"the alert text should be (.*)")]
        public void ThenTheAlertTextShouldBe(string text)
        {
            //Cannot take screenshot of alert.
            var alert = Driver.SwitchTo().Alert();

            Assert.AreEqual(alert.Text, text);
        }

        [Then(@"the kendo alert text should be (.*)")]
        public void ThenTheKendoAlertShouldBe(string text)
        {
            var js = ((IJavaScriptExecutor)Driver);

            var selectedOption = (string)js.ExecuteScript(" return $('#confirm').text();");

            Assert.AreEqual(selectedOption, text);
        }

        [Then(@"the number of rows in grid (.*) should be (.*)")]
        public void ThenTheNumberOfRowsInGridSHouldBe(string gridId, int numRows)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(PageTimeOut));

            wait.Until(ExpectedConditions.ElementExists(By.Id(gridId)));

            var grid = Driver.FindElement(By.Id(gridId));

            var rows = grid.FindElements(By.TagName("tr"));

            TakeScreenShot();

            //Remove header row in count.
            Assert.AreEqual(rows.Count - 1, numRows);
        }

        [Then(@"the date field (.*) has current date")]
        public void ThenTheDateFieldHasDate(string inputId)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(PageTimeOut));

            wait.Until(ExpectedConditions.ElementExists(By.Id(inputId)));

            var dateInput = Driver.FindElement(By.Id(inputId));

            Assert.IsTrue(DateTime.Now.ToString("M/d/yyyy") == dateInput.GetAttribute("value"));
        }

        [Then(@"file with name (.*) was exported")]
        public void ThenFileWithNameWasExported(string fileName)
        {
            var exist = false;
            var pathUser = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            var pathDownload = Path.Combine(pathUser, "Downloads");
            var filePaths = Directory.GetFiles(pathDownload);
            foreach (var p in filePaths)
            {
                if (!p.Contains(fileName)) continue;
                var thisFile = new FileInfo(p);
                //Check the file that are downloaded in the last 3 minutes
                if (thisFile.LastWriteTime.ToShortTimeString() == DateTime.Now.ToShortTimeString() ||
                    thisFile.LastWriteTime.AddMinutes(1).ToShortTimeString() == DateTime.Now.ToShortTimeString() ||
                    thisFile.LastWriteTime.AddMinutes(2).ToShortTimeString() == DateTime.Now.ToShortTimeString() ||
                    thisFile.LastWriteTime.AddMinutes(3).ToShortTimeString() == DateTime.Now.ToShortTimeString())
                {
                    exist = true;
                    File.Delete(p);
                    break;
                }
            }
            var js = ((IJavaScriptExecutor)Driver);

            js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");

            Thread.Sleep(100);

            TakeScreenShotEntireScreen();

            Assert.IsTrue(exist);
        }
    }
}
