using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace TemplateProject.WebTests.Test_Methods
{
    public partial class TestMethods : BaseWebTest
    {
        public void WaitElementIdExist(string id)
        {
            Wait.Until(ExpectedConditions.ElementExists(By.Id(id)));
        }

        public void WaitElementClassExist(string className)
        {
            Wait.Until(ExpectedConditions.ElementExists(By.ClassName(className)));
        }

        public void WaitElementIdVisible(string id)
        {
            Wait.Until(ExpectedConditions.ElementIsVisible(By.Id(id)));
        }

        public void WaitElementClassVisible(string className)
        {
            Wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName(className)));
        }

        public void WaitElementCssVisible(string cssSelector)
        {
            Wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(cssSelector)));
        }

        public void WaitElementInvisiblePresent(string cssSelector)
        {
            Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector(cssSelector)));
        }


        /// <summary>
        /// Use to refresh menu.
        /// </summary>
        [When(@"page is reset")]
        public void WhenPageReset()
        {
            Driver.Navigate().GoToUrl(BaseUrl);
        }

        [When(@"element with id (.*) is clicked")]
        public void WhenElementIdClicked(string elementId)
        {
            WaitElementIdExist(elementId);

            Thread.Sleep(500);

            var element = Driver.FindElement(By.Id(elementId));
            var action = new Actions(Driver);
            action.MoveToElement(element).Build().Perform();
            TakeScreenShot();
            if (element.HasMethod("Click") && element.Displayed)
            {
                element.Click();
            }
            else
            {
                element.SendKeys(Keys.Return);
            }
        }

        [When(@"element with class (.*) is clicked")]
        public void WhenElementClassClicked(string className)
        {
            WaitElementClassExist(className);

            var element = Driver.FindElement(By.ClassName(className));
            var action = new Actions(Driver);
            action.MoveToElement(element).Build().Perform();
            TakeScreenShot();

            if (element.HasMethod("Click") && element.Displayed)
            {
                element.Click();
            }
            else
            {
                element.SendKeys(Keys.Return);
            }
        }

        [When(@"element with class (.*) in element with id (.*) clicked")]
        public void WhenElementWithClassInElementWithIdClicked(string className, string elementId)
        {
            WaitElementIdVisible(elementId);

            var elementWithId = Driver.FindElement(By.Id(elementId));
            var element = elementWithId.FindElement(By.ClassName(className));
            var action = new Actions(Driver);
            action.MoveToElement(element).Build().Perform();
            TakeScreenShot();

            element.SendKeys(Keys.Return);
        }

        [When(@"radiobutton with id (.*) is clicked")]
        public void WhenRadioButtonWithIdClicked(string elementId)
        {
            WaitElementIdExist(elementId);

            var js = ((IJavaScriptExecutor)Driver);

            js.ExecuteScript(string.Format("$('#{0}').prop('checked',true);", elementId));

            TakeScreenShot();
        }

        [When(@"text (.*) entered in (.*)")]
        public void WhenTextEntered(string text, string inputId)
        {
            WaitElementIdExist(inputId);

            var input = Driver.FindElement(By.Id(inputId));

            //use ctr+a to avoid client validator problems.
            input.SendKeys(Keys.Control + "a");
            input.SendKeys(text);

            TakeScreenShot();
        }

        [When(@"value (.*) entered in numeric with (.*) id")]
        public void WhenValueEnteredInNumericWithId(string value, string inputId)
        {
            WaitElementIdExist(inputId);

            var js = ((IJavaScriptExecutor)Driver);

            js.ExecuteScript(string.Format("$('#{0}').data('kendoNumericTextBox').value({1});", inputId, value));

            //Note: Changing value of dropdown list or numeric kendo elements does not necessarily call change event. 
            //Call change event to ensure all effects of changing this element occur.
            js.ExecuteScript(string.Format("$('#{0}').data('kendoNumericTextBox').trigger('{1}');", inputId, "change"));

            TakeScreenShot();
        }

        [When(@"click alert box ok (.*)")]
        public void WhenClickAlertBoxOk(bool ok)
        {
            if (ok)
            {
                Driver.SwitchTo().Alert().Accept();
            }
            else
            {
                Driver.SwitchTo().Alert().Dismiss();
            }
        }

        [When(@"click kendo alert box ok (.*)")]
        public void WhenClickKendoAlertBoxOk(bool ok)
        {
            TakeScreenShot();

            if (ok)
            {
                var js = ((IJavaScriptExecutor)Driver);

                js.ExecuteScript("$('#confirm').parent().find('.k-button.k-primary').trigger('click');");
            }
            else
            {
                var js = ((IJavaScriptExecutor)Driver);

                js.ExecuteScript("$('#confirm').parent().find(''[class='k-button']'').trigger('click');");
            }
        }

        [When(@"switch to new tab")]
        public void WhenSwitchToNewTab()
        {
            var tabs = Driver.WindowHandles;

            Driver.SwitchTo().Window(tabs.Last());
        }

        [When(@"switch to old tab")]
        public void WhenSwitchToOldTab()
        {
            var tabs = Driver.WindowHandles;

            Driver.SwitchTo().Window(tabs.First());
        }

        [When(@"dropdown with id (.*) value (.*)")]
        public void WhenDropDownPicked(string dropdownId, string value)
        {
            Thread.Sleep(500);

            var js = ((IJavaScriptExecutor)Driver);

            js.ExecuteScript(string.Format("$('#{0}').data('kendoDropDownList').value('{1}');", dropdownId, value));

            js.ExecuteScript(string.Format("$('#{0}').data('kendoDropDownList').trigger('{1}');", dropdownId, "select"));

            js.ExecuteScript(string.Format("$('#{0}').data('kendoDropDownList').trigger('{1}');", dropdownId, "change"));

            TakeScreenShot();
        }

        [When(@"element with class (.*) clicked in (.*) grid with row containing text (.*)")]
        public void WhenElementWithClassClickedInGridWithRowContainingText(string className, string gridId, string text)
        {
            WaitElementIdExist(gridId);

            Thread.Sleep(3000);

            var grid = Driver.FindElement(By.Id(gridId));

            var tbody = grid.FindElement(By.TagName("tbody"));

            var testRow = tbody.FindElement(By.CssSelector("tr"));

            Wait.Until(i => testRow.Displayed);

            WaitElementClassExist(className);

            var rows = grid.FindElements(By.TagName("tr"));

            IWebElement gridRow = rows.FirstOrDefault(row => row.FindElements(By.TagName("td")).Select(i => i.Text).Contains(text));

            var button = gridRow?.FindElement(By.ClassName(className));

            var action = new Actions(Driver);
            action.MoveToElement(button).Build().Perform();

            TakeScreenShot();
            if (button.HasMethod("Click"))
            {

                button?.Click();

            }
            else
            {
                button?.SendKeys(Keys.Return);
            }

        }

        [When(@"row containing text (.*) in grid (.*) is clicked")]
        public void WhenRowContainingTextInGridClicked(string text, string gridId)
        {
            WaitElementIdExist(gridId);

            Thread.Sleep(3000);

            var grid = Driver.FindElement(By.Id(gridId));

            var tbody = grid.FindElement(By.TagName("tbody"));

            var testRow = tbody.FindElement(By.CssSelector("tr"));

            Wait.Until(i => testRow.Displayed);

            var rows = grid.FindElements(By.TagName("tr"));

            List<IWebElement> gridRows = rows.Where(row => row.FindElements(By.TagName("td")).Select(i => i.Text).Contains(text)).Take(2).ToList();


            var action = new Actions(Driver);
            action.KeyDown(Keys.LeftControl).Click(gridRows[0]).Click(gridRows[1]).KeyUp(Keys.LeftControl).Build().Perform();

            TakeScreenShot();
        }
        /// <summary>
        /// Does not work when filtering multiple fields at the same time.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="columnName"></param>
        /// <param name="gridId"></param>
        [When(@"filter (.*) in column (.*) in grid (.*)")]
        public void WhenFilterColumnInGrid(string value, string columnName, string gridId)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(PageTimeOut));
            wait.Until(ExpectedConditions.ElementExists(By.Id(gridId)));
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector("div.k-window")));

            var js = ((IJavaScriptExecutor)Driver);

            //Allow for filter of current date.
            if (value.ToLower() == "currentdate")
            {
                value = DateTime.Now.ToString("MM/dd/yyyy");
            }

            var runJs = "";
            int filterValue;
            DateTime filterDate;
            if (int.TryParse(value, out filterValue))
            {
                runJs = "var grid=$('#" + gridId + "').data('kendoGrid'); var oldFilter= grid.dataSource.filter();" +
                        "grid.dataSource.filter({ field: '" + columnName + "',operator:'eq',value: '" + value + "'});";
            }
            else if (DateTime.TryParse(value, out filterDate))
            {
                runJs = "var grid=$('#" + gridId + "').data('kendoGrid'); " +
                  "grid.dataSource.filter({field:'" + columnName + "',operator:'gte',value:'" + filterDate.ToString("MM/dd/yyyy") + "'});" +
                  "grid.dataSource.sort({field:'" + columnName + "',dir:'desc'});";

            }
            else if (value == "null")
            {
                runJs = "var grid=$('#" + gridId + "').data('kendoGrid'); " +
                       "grid.dataSource.filter({field:'" + columnName + "',operator:'gte',value:null}); grid.dataSource.filter({field:'" + columnName + "',operator:'lte',value:null});";
            }
            else
            {
                runJs = "var grid=$('#" + gridId + "').data('kendoGrid'); " +
                        "grid.dataSource.filter({field:'" + columnName + "',operator:'startswith',value:'" + value + "'})";
            }

            js.ExecuteScript(runJs);

            TakeScreenShot();
        }

        [When(@"filter on grid (.*) is cleared")]
        public void WhenFilterInGridIsCleared(string gridId)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(PageTimeOut));
            wait.Until(ExpectedConditions.ElementExists(By.Id(gridId)));
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector("div.k-window")));

            var js = ((IJavaScriptExecutor)Driver);

            var runJs = "var grid=$('#" + gridId + "').data('kendoGrid'); " +
                        "grid.dataSource.filter({});";

            js.ExecuteScript(runJs);

            TakeScreenShot();
        }

        [When(@"scroll to bottom of screen")]
        public void WhenScrollToBottomOfScreen()
        {
            var js = ((IJavaScriptExecutor)Driver);

            js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");

            Thread.Sleep(100);
        }

    }
}
