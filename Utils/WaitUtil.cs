using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace Automation.Utils
{
    public static class WaitUtil
    {
        // Wait until an element is visible on the page
        public static IWebElement WaitForElementVisible(IWebDriver driver, By locator, int timeoutInSeconds)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            return wait.Until(OpenQA.Selenium.Support.UI.ExpectedConditions.ElementIsVisible(locator));
        }

        // Wait until an element is clickable
        public static IWebElement WaitForElementClickable(IWebDriver driver, By locator, int timeoutInSeconds)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            return wait.Until(OpenQA.Selenium.Support.UI.ExpectedConditions.ElementToBeClickable(locator));
        }

        // Wait for a specific text to be present in an element
        public static bool WaitForTextToBePresent(IWebDriver driver, By locator, string text, int timeoutInSeconds)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            return wait.Until(OpenQA.Selenium.Support.UI.ExpectedConditions.TextToBePresentInElementLocated(locator, text));
        }
    }
}
