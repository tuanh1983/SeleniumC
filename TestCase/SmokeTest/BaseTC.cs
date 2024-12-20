using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Events;
using OpenQA.Selenium.Support.UI;
using System.ComponentModel;
using System;
using WebDriverManager.DriverConfigs.Impl;
using Automation.PageObjects;
using Automation.Util;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework.Interfaces;
using System.Web;

namespace Automation.TestCase.SmokeTest
{
    public class BaseTC
    {
        ExtentReports extent;
        ExtentTest test;
        public IWebDriver driver;
        WebDriverWait wait;
        public string browerName;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string reportPath = projectDirectory + "//index_" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss") + ".html";
            browerName = TestContext.Parameters["browerName"];
            var htmlReport = new ExtentSparkReporter(reportPath);
            extent = new ExtentReports();
            extent.AttachReporter(htmlReport);
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Url = "https://ac-caseworker-demo.casewhere.com/#!/";
        }

        [SetUp]
        public void Setup()
        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            Console.WriteLine("Setup method");
        }

        public static JsonReader getDataParse()
        {
            return new JsonReader();
        }
        [TearDown]
        public void After()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            DateTime time = DateTime.Now;
            string fileName = "Screenshot_" + time.ToString("h_mm_ss") + ".png"; // Create unique file name for screenshots
            string screenshotBase64 = CaptureScreenshot(driver, fileName);
            if (status == TestStatus.Failed)
            {
                // Capture screenshot on failure
                test.Log(Status.Fail, "Test failed", MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshotBase64).Build());
            }
            else if (status == TestStatus.Passed)
            {
                // Optionally capture a screenshot on pass (if needed)
                test.Log(Status.Pass, "Test passed", MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshotBase64).Build());
            }

        }
        [OneTimeTearDown]
        public void FinalTearDown()
        {
            // Flush the Extent report
            extent.Flush();
            // Quit the driver after all tests are done
            driver.Quit();
        }
        // Capture Screenshot as Base64
        public string CaptureScreenshot(IWebDriver driver, string screenshotName)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            var screenshot = ts.GetScreenshot();
            string base64Screenshot = screenshot.AsBase64EncodedString;
            return base64Screenshot;  // Returning base64 encoded string
        }
    }
}
