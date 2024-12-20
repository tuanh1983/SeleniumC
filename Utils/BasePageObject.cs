using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.IO;

namespace Automation.Utils
{
    public abstract class BasePageObject
    {
        protected readonly IWebDriver driver;
        protected readonly WebDriverWait wait;

        // Constructor to initialize driver and wait
        public BasePageObject(IWebDriver driver, int timeoutInSeconds)
        {
            this.driver = driver ?? throw new ArgumentNullException(nameof(driver));
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
        }

        // Wait for an element to be clickable
        protected IWebElement WaitForElementToBeClickable(By locator)
        {
            try
            {
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locator));
            }
            catch (WebDriverTimeoutException ex)
            {
                LogError($"Timeout waiting for element to be clickable: {locator}", ex);
                throw;
            }
        }

        // Find an element with retry logic
        protected IWebElement FindElement(By locator, int retries = 3)
        {
            for (int i = 0; i < retries; i++)
            {
                try
                {
                    return driver.FindElement(locator);
                }
                catch (NoSuchElementException)
                {
                    if (i == retries - 1) throw;
                    WaitBeforeRetry();
                }
            }
            return null; // Should not reach here
        }

        // Find multiple elements
        protected IReadOnlyCollection<IWebElement> FindElements(By locator)
        {
            try
            {
                return driver.FindElements(locator);
            }
            catch (NoSuchElementException ex)
            {
                LogError($"Elements not found: {locator}", ex);
                throw;
            }
        }

        // Wait for an element to be visible
        protected IWebElement WaitForElementToBeVisible(By locator)
        {
            try
            {
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
            }
            catch (WebDriverTimeoutException ex)
            {
                LogError($"Timeout waiting for element to be visible: {locator}", ex);
                throw;
            }
        }

        // Wait for a specific text to be present in an element
        protected bool WaitForTextToBePresentInElement(By locator, string text, int timeout = 10)
        {
            try
            {
                var customWait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                return customWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElementLocated(locator, text));
            }
            catch (WebDriverTimeoutException ex)
            {
                LogError($"Timeout waiting for text '{text}' to be present in element: {locator}", ex);
                return false;
            }
        }

        // Hover over an element
        protected void HoverOverElement(By locator)
        {
            try
            {
                var element = WaitForElementToBeVisible(locator);
                var actions = new OpenQA.Selenium.Interactions.Actions(driver);
                actions.MoveToElement(element).Perform();
            }
            catch (Exception ex)
            {
                LogError($"Error while hovering over element: {locator}", ex);
                throw;
            }
        }

        // Take a screenshot
        protected void CaptureScreenshot(string filename)
        {
            try
            {
                string screenshotsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Screenshots");
                Directory.CreateDirectory(screenshotsDir);

                var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                string filePath = Path.Combine(screenshotsDir, $"{filename}.png");
                screenshot.SaveAsFile(filePath, ScreenshotImageFormat.Png);
                Console.WriteLine($"Screenshot saved at: {filePath}");
            }
            catch (Exception ex)
            {
                LogError("Error capturing screenshot", ex);
            }
        }

        // Helper method to log errors
        private void LogError(string message, Exception ex)
        {
            Console.WriteLine($"{message}. Exception: {ex.Message}");
        }

        // Helper method to wait before retrying
        private void WaitBeforeRetry()
        {
            Thread.Sleep(500); // Wait 500ms before retrying
        }
        // Send keys to an element after waiting for it to be visible
        protected void SendKeys(By locator, string text)
        {
            try
            {
                WaitForElementToBeVisible(locator).SendKeys(text);
            }
            catch (Exception ex)
            {
                LogError($"Error sending keys to element: {locator}", ex);
                throw;
            }
        }

        // Click on an element after waiting for it to be clickable
        protected void Click(By locator)
        {
            try
            {
                WaitForElementToBeClickable(locator).Click();
            }
            catch (Exception ex)
            {
                LogError($"Error clicking on element: {locator}", ex);
                throw;
            }
        }

        // Get text from an element after waiting for it to be visible
        protected string GetText(By locator)
        {
            try
            {
                return WaitForElementToBeVisible(locator).Text;
            }
            catch (Exception ex)
            {
                LogError($"Error getting text from element: {locator}", ex);
                throw;
            }
        }

    }
}
