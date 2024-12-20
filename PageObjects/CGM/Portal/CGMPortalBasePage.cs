using Automation.Utils;
using OpenQA.Selenium;

namespace Automation.PageObjects.CGM.Portal
{
    public class CGMPortalBasePage : BasePageObject
    {
        public CGMPortalBasePage(IWebDriver driver, int timeoutInSeconds) : base(driver, timeoutInSeconds) { }

        // Define locators
        private By GetDynamicLinkLocator(string linkText) => By.XPath($"//span[@class='nav-link-text ng-binding' and text()='{linkText}']");
        private By GetDynamicTabLocator(string linkText) => By.XPath($"//button[contains(@class, 'nav-link ng-binding') and contains(text(), '{linkText}')]");

        // Click on a dynamic link
        public void ClickOnDynamicLink(string linkText)
        {
            var dynamicLink = WaitForElementToBeClickable(GetDynamicLinkLocator(linkText));
            dynamicLink.Click();
        }

        // Click on a dynamic tab
        public void ClickOnDynamicTab(string linkText)
        {
            var dynamicTab = WaitForElementToBeClickable(GetDynamicTabLocator(linkText));
            dynamicTab.Click();
        }
    }
}