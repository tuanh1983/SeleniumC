using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace Automation.Utils
{
    public abstract class BasePageObject
    {
        protected readonly IWebDriver driver;
        protected readonly WebDriverWait wait;

        public BasePageObject(IWebDriver driver, int timeoutInSeconds)
        {
            this.driver = driver;
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
        }

        // Wait for an element to be clickable
        protected IWebElement WaitForElementToBeClickable(By locator)
        {
            return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locator));
        }

        // Generic method to find an element
        protected IWebElement FindElement(By locator)
        {
            try
            {
                return driver.FindElement(locator);
            }
            catch (NoSuchElementException ex)
            {
                // Log the exception
                Console.WriteLine($"Element not found: {locator}. Exception: {ex.Message}");
                throw;
            }
        }

        // Generic method to find multiple elements
        protected IReadOnlyCollection<IWebElement> FindElements(By locator)
        {
            try
            {
                return driver.FindElements(locator);
            }
            catch (NoSuchElementException ex)
            {
                // Log the exception
                Console.WriteLine($"Elements not found: {locator}. Exception: {ex.Message}");
                throw;
            }
        }

        // Wait for an element to be visible
        protected IWebElement WaitForElementToBeVisible(By locator)
        {
            return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
        }
    }
}