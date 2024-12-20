using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using Automation.Utils;
using Automation.Drivers;
using OpenQA.Selenium;
using System;
using System.Threading;

namespace Automation.Tests
{
    public abstract class BaseTestCase
    {
        // Use ThreadLocal for thread-safe WebDriver instances
        private static ThreadLocal<IWebDriver> threadDriver = new ThreadLocal<IWebDriver>(() => null);
        protected IWebDriver driver => threadDriver.Value;

        protected ModuleConfig moduleConfig;
        protected string Product { get; set; }
        protected string Module { get; set; }
        protected int TimeoutInSeconds { get; set; }
        protected string user { get; set; }
        protected string password { get; set; }

        // ExtentReports setup
        private static ExtentReports _extent;
        private static ThreadLocal<ExtentTest> _test = new ThreadLocal<ExtentTest>(() => null);
        protected ExtentTest Test => _test.Value;

        [OneTimeSetUp]
        public void InitializeExtentReports()
        {
            try
            {
                Console.WriteLine("Initializing ExtentReports...");

                string reportPath = $"{AppDomain.CurrentDomain.BaseDirectory}Report\\SparkReport.html";

                Console.WriteLine($"Report Path: {reportPath}");

                Directory.CreateDirectory(Path.GetDirectoryName(reportPath)!);

                var sparkReporter = new ExtentSparkReporter(reportPath)
                {
                    Config =
            {
                DocumentTitle = "Automation Test Report",
                ReportName = "Extent Report - Spark",
                Theme = AventStack.ExtentReports.Reporter.Config.Theme.Dark
            }
                };

                _extent = new ExtentReports();
                _extent.AttachReporter(sparkReporter);
                _extent.AddSystemInfo("Environment", "QA");
                _extent.AddSystemInfo("Tester", "Your Name");
                Console.WriteLine("ExtentReports initialized successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing ExtentReports: {ex.Message}");
                throw;
            }
        }


        [SetUp]
        public virtual void SetUp()
        {
            try
            {
                Console.WriteLine("BaseTestCase SetUp started");

                // Create a new test in ExtentReports
                _test.Value = _extent.CreateTest(TestContext.CurrentContext.Test.Name);

                // Load Product and Module
                Product = Product ?? TestContext.Parameters.Get("Product", "DefaultProduct");
                Module = Module ?? TestContext.Parameters.Get("Module", "DefaultModule");

                Test.Info($"Product: {Product}, Module: {Module}");

                // Load configuration
                moduleConfig = ConfigReader.GetProductModuleConfig<ModuleConfig>(Product, Module);
                if (moduleConfig == null)
                {
                    throw new Exception($"Configuration not found for Product: {Product}, Module: {Module}");
                }

                Test.Info($"Configuration loaded: BaseUrl={moduleConfig.BaseUrl}, Browser={moduleConfig.Browser}");

                // Initialize WebDriver
                if (string.IsNullOrEmpty(moduleConfig.Browser))
                {
                    throw new NotSupportedException("Browser is not specified in the configuration.");
                }

                if (threadDriver.Value == null)
                {
                    threadDriver.Value = Automation.Drivers.WebDriverManager.GetDriver(moduleConfig.Browser);
                    Test.Info($"WebDriver initialized for browser: {moduleConfig.Browser}");
                }

                // Navigate to Base URL
                if (string.IsNullOrEmpty(moduleConfig.BaseUrl))
                {
                    throw new Exception("BaseUrl is not specified in the configuration.");
                }

                driver.Navigate().GoToUrl(moduleConfig.BaseUrl);
                Test.Info($"Navigated to Base URL: {moduleConfig.BaseUrl}");

                // Set timeout
                TimeoutInSeconds = moduleConfig.Timeout;
                Test.Info($"Timeout set to: {TimeoutInSeconds} seconds");

                // Set user credentials
                user = moduleConfig.Credentials.Username;
                password = moduleConfig.Credentials.Password;
                Test.Info($"User credentials set for: {user}");
            }
            catch (Exception ex)
            {
                Test.Fail($"Error during setup: {ex.Message}");
                throw;
            }
        }

        [TearDown]
        public virtual void TearDown()
        {
            try
            {
                Test.Info("BaseTestCase TearDown started");

                // Quit the WebDriver for this thread
                if (driver != null)
                {
                    driver.Quit();
                    threadDriver.Value = null; // Clear the value for this thread
                    Test.Pass("WebDriver quit successfully");
                }
                else
                {
                    Test.Warning("No WebDriver instance found for this thread.");
                }
            }
            catch (Exception ex)
            {
                Test.Fail($"Error during teardown: {ex.Message}");
            }
        }

        // Final cleanup after all tests
        [OneTimeTearDown]
        public void FinalCleanup()
        {
            try
            {
                Console.WriteLine("Final cleanup: Disposing ThreadLocal WebDriver instances.");
                threadDriver.Dispose();

                // Flush the ExtentReports
                _extent.Flush();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during final cleanup: {ex.Message}");
            }
        }
    }
}
