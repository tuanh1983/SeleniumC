using Automation.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Internal;
using System.Threading;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace Automation.Drivers
{
    public static class WebDriverManager
    {
        private static IWebDriver? driver;
        private static BaseDriverManager? driverManager;

        public static IWebDriver GetDriver(string browser)
        {
            if (driver == null)
            {
                Console.WriteLine($"Browser: {browser}");

                // Validate the browser input
                if (string.IsNullOrEmpty(browser))
                {
                    throw new ArgumentNullException(nameof(browser), "Browser cannot be null or empty.");
                }

                // Instantiate the appropriate DriverManager based on the browser
                driverManager = browser.ToLower() switch
                {
                    "chrome" => new ChromeDriverManager(),
                    "firefox" => new FirefoxDriverManager(),
                    _ => throw new NotSupportedException($"Browser {browser} is not supported.")
                };

                // Use the DriverManager to create the driver
                driver = driverManager.GetDriver();

                // Configure the WebDriver with default timeout
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            }

            return driver;
        }

        public static void QuitDriver()
        {
            if (driver != null)
            {
                driver.Quit();
                driver = null;
            }
        }

    }



}
