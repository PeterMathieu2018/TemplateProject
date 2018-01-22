using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using TechTalk.SpecFlow;
using System.Drawing;
using System.Windows.Forms;
using OpenQA.Selenium.Support.UI;

namespace TemplateProject.WebTests
{
    [TestClass]
    public abstract class BaseWebTest
    {
        public IWebDriver Driver;
        public string BaseUrl;
        public string ScreenshotLocation;
        public int PageTimeOut;
        public int TestStep;
        public DateTime TestDateTime;
        public WebDriverWait Wait;

        protected BaseWebTest(TestConfiguration config)
        {
            Driver = config.Driver;
            BaseUrl = config.BaseUrl;
            ScreenshotLocation = config.ScreenshotLocation;
            PageTimeOut = config.PageTimeOut;
            TestStep = config.TestStep;
            TestDateTime = config.TestDateTime;
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(PageTimeOut));
        }

        public void TakeScreenShot()
        {
            var screenshot = ((ITakesScreenshot)Driver).GetScreenshot();
            var actionName = ScenarioContext.Current.StepContext.StepInfo.Text;
            var regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            var r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            var path = getSavePath();

            System.IO.Directory.CreateDirectory(path);

            //max character is 260
            var savePath = path +
                            "Step " +
                            TestStep +
                            //" " 
                            //+TestDateTime.ToString("hh-mm-ss") + 
                            " " +
                            r.Replace(actionName, "");

            screenshot.SaveAsFile(savePath.Truncate(255) + ".png"
                , ScreenshotImageFormat.Png);

            TestStep++;
        }

        public void TakeScreenShotEntireScreen()
        {
            var memoryImage = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                               Screen.PrimaryScreen.Bounds.Height,
                               PixelFormat.Format32bppArgb);
            var s = new Size(memoryImage.Width, memoryImage.Height);

            var memoryGraphics = Graphics.FromImage(memoryImage);

            memoryGraphics.CopyFromScreen(0, 0, 0, 0, s);
            var actionName = ScenarioContext.Current.StepContext.StepInfo.Text;
            var regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            var r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            var path = getSavePath();

            System.IO.Directory.CreateDirectory(path);

            memoryImage.Save(path +
                "Step " +
                TestStep +
                //" " 
                //+TestDateTime.ToString("hh-mm-ss") + 
                " " +
                r.Replace(actionName, "") +
                ".png");
        }

        public string getSavePath()
        {
            var frame = new StackFrame(1);
            var actionName = ScenarioContext.Current.StepContext.StepInfo.Text;
            var regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            var r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            var version = fvi.FileVersion;

            var path =
                ScreenshotLocation
                + TestDateTime.ToString("yyyy-dd-M")
                + " Version "
                + r.Replace(version, "")
                + "\\"
                + r.Replace(ScenarioContext.Current.ScenarioInfo.Title, "")
                + "\\";

            return path;
        }

        [AfterScenario]
        public void DisposeWebDriver()
        {
            Driver.Dispose();
        }
    }
}
