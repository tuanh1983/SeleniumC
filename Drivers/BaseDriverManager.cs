using OpenQA.Selenium;
using System.Threading;

namespace Automation.Drivers
{
    public abstract class BaseDriverManager
    {
        // Thread-local storage for WebDriver to ensure thread safety
        protected static ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>();

        // Method to retrieve or create a WebDriver instance
        public IWebDriver GetDriver()
        {
            // Only create a new driver if it is not already initialized
            if (!driver.IsValueCreated)
            {
                driver.Value = CreateDriver();
            }
            return driver.Value;
        }

        // Method to quit and dispose of the WebDriver
        public void QuitDriver()
        {
            if (driver.IsValueCreated && driver.Value != null)
            {
                driver.Value.Quit();
                driver.Dispose();
            }
        }

        // Abstract method to be implemented by subclasses for creating specific drivers
        protected abstract IWebDriver CreateDriver();
    }
}
