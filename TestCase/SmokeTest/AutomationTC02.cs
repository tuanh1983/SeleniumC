using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Events;
using OpenQA.Selenium.Support.UI;
using WebDriverManager.DriverConfigs.Impl;
namespace Automation.TestCase.SmokeTest
{
    public class AutomationTC02
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
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            Console.WriteLine("Setup method");
        }

        [Test]
        public void Test1()
        {
            Console.WriteLine("This is test 1");
            driver.Url = "https://worker-local.casewhere.com/cw-bookstore#!/";

            Thread.Sleep(5000);
            //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector("[class='nav-link-text ng-binding']")));

            IList<IWebElement> elements = driver.FindElements(By.CssSelector("[class='nav-link-text ng-binding']"));

            // Access the second element (index 1 since the list is zero-based)
            IWebElement secondElement = elements[10];
            secondElement.Click();
            Thread.Sleep(5000);
            //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector("[class='nav-link-text ng-binding']")));
            IWebElement addBtn = driver.FindElement(By.CssSelector("[data-cw-element-id='263542b5-e1be-43c8-ba82-30910f0b0f32']"));
            addBtn.Click();
            Thread.Sleep(5000);
            IWebElement companyInt = driver.FindElement(By.CssSelector("[id='CompanyInt']"));
            // Perform an action on the second element (e.g., click)
            companyInt.SendKeys(Keys.Control + "a");
            companyInt.SendKeys(" ");

            string borderColor = companyInt.GetCssValue("border-color");

            // Output the border color
            Console.WriteLine($"Border color of CompanyInt: {borderColor}");
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