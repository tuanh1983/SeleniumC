using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Events;
using OpenQA.Selenium.Support.UI;
using System.ComponentModel;
using System;
using WebDriverManager.DriverConfigs.Impl;
namespace Automation.TestCase.SmokeTest
{
    public class AutomationTC01
    {
        IWebDriver driver;
        WebDriverWait wait;
        [SetUp]
        public void Setup()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            Console.WriteLine("Setup method");

        }

        [Test]
        public void Test1()
        {
            Console.WriteLine("This is test 1");
            driver.Url = "https://ac-caseworker-demo.casewhere.com/#!/";

            driver.FindElement(By.LinkText("Username & password")).Click();
            driver.FindElement(By.Id("form-username")).SendKeys("ac-test+administrativt@gmail.com");
            driver.FindElement(By.Id("form-password")).SendKeys("ACtest!234");
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();
            TestContext.Progress.WriteLine(driver.Title);
            TestContext.Progress.WriteLine(driver.Url);
            //Thread.Sleep(5000);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector("[class='nav-link-text ng-binding']")));
            IWebElement parkingFeesLink = driver.FindElement(By.CssSelector("[class='nav-link-text ng-binding']"));
            parkingFeesLink.Click();
            Thread.Sleep(5000);
            IList<IWebElement> elements = driver.FindElements(By.CssSelector("[class='nav-link-text ng-binding']"));

            // Access the second element (index 1 since the list is zero-based)
            IWebElement secondElement = elements[1];

            secondElement.Click();

            //Wait for the list of application clickable
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector(".ui-grid-canvas .ui-grid-row .ui-grid-cell a.ng-binding")));
            IList<IWebElement> lstParkingLink = driver.FindElements(By.CssSelector(".ui-grid-canvas .ui-grid-row .ui-grid-cell a.ng-binding"));
            IWebElement parkingLink = lstParkingLink[1];
            parkingLink.Click();
            Thread.Sleep(10000);
            IList<IWebElement> lstTab = driver.FindElements(By.CssSelector(".nav-link.ng-binding"));
            IWebElement complainTab = lstTab[3];
            complainTab.Click();

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector(".ui-grid-coluiGrid-00HY .ui-grid-cell-contents.tooltip-viewport-container a.ng-binding")));
            IWebElement complainLinks = driver.FindElement(By.CssSelector(".ui-grid-coluiGrid-00HY .ui-grid-cell-contents.tooltip-viewport-container a.ng-binding"));
            //Console.WriteLine(complainLinks);
            Thread.Sleep(2000);
            complainLinks.Click();

            Thread.Sleep(5000);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("input.form-control.main-input[placeholder='dd.MM.yyyy']")));
            IWebElement complaintDatetime = driver.FindElement(By.CssSelector("input.form-control.main-input[placeholder='dd.MM.yyyy']"));
            Console.WriteLine(complaintDatetime);
            Thread.Sleep(5000);
            complaintDatetime.SendKeys(Keys.Control + "a");
            complaintDatetime.SendKeys(" ");
            Thread.Sleep(5000);
            string borderColor = complaintDatetime.GetCssValue("border-color");

            Assert.Pass();
        }
        [Test]
        public void Test2()
        {


            Console.WriteLine("This is test 2");
            Assert.Pass();
        }
        [TearDown]
        public void TearDown()
        {
            Console.WriteLine("This is TearDown");
            //Assert.Pass();
        }
    }
}