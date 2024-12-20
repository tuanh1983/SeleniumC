using NUnit.Framework;
using Automation.Utils;
using Automation.Drivers;
using OpenQA.Selenium;

namespace Automation.Tests
{
    public abstract class BaseTestCase
    {
        protected IWebDriver driver;
        protected ModuleConfig moduleConfig;
        protected string Product { get; set; }
        protected string Module { get; set; }
        protected int TimeoutInSeconds { get; set; }

        [SetUp]
        public virtual void SetUp()
        {
            try
            {
                // Log step
                Console.WriteLine("BaseTestCase SetUp started");

                // Load Product and Module dynamically if not already set
                Product = Product ?? TestContext.Parameters.Get("Product", "DefaultProduct");
                Module = Module ?? TestContext.Parameters.Get("Module", "DefaultModule");

                Console.WriteLine($"Product: {Product}, Module: {Module}");

                // Load configuration for the specified Product and Module
                moduleConfig = ConfigReader.GetProductModuleConfig<ModuleConfig>(Product, Module);
                if (moduleConfig == null)
                {
                    throw new Exception($"Configuration not found for Product: {Product}, Module: {Module}");
                }

                Console.WriteLine($"Configuration loaded: BaseUrl={moduleConfig.BaseUrl}, Browser={moduleConfig.Browser}");

                // Check and initialize WebDriver
                if (string.IsNullOrEmpty(moduleConfig.Browser))
                {
                    throw new NotSupportedException("Browser is not specified in the configuration.");
                }

                driver = Automation.Drivers.WebDriverManager.GetDriver(moduleConfig.Browser);
                if (driver == null)
                {
                    throw new Exception($"WebDriver could not be initialized for browser: {moduleConfig.Browser}");
                }

                Console.WriteLine($"WebDriver initialized for browser: {moduleConfig.Browser}");

                // Navigate to Base URL
                if (string.IsNullOrEmpty(moduleConfig.BaseUrl))
                {
                    throw new Exception("BaseUrl is not specified in the configuration.");
                }
                driver.Navigate().GoToUrl(moduleConfig.BaseUrl);

                Console.WriteLine($"Navigated to Base URL: {moduleConfig.BaseUrl}");

                // Set the timeout value
                TimeoutInSeconds = moduleConfig.Timeout;
                Console.WriteLine($"Timeout set to: {TimeoutInSeconds} seconds");
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error during setup: {ex.Message}");
                throw;
            }
        }

        [TearDown]
        public virtual void TearDown()
        {
            try
            {
                // Flush the Extent report
                //extent.Flush();
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error flushing extent report: {ex.Message}");
            }
            finally
            {
                // Quit the driver after all tests are done
                driver?.Quit();
            }
        }
    }
}