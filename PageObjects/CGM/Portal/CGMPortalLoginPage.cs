using Automation.Utils;
using OpenQA.Selenium;
using System;
using System.Linq;

namespace Automation.PageObjects.CGM.Portal
{
    public class CGMPortalLoginPage : CGMPortalBasePage
    {
        // Define locators for login page elements
        private readonly By usernameField = By.Id("MitIDSimulator_InputUsername");
        private readonly By passwordField = By.Id("MitIDSimulator_InputPassword");
        private readonly By loginButton = By.Id("MitIDSimulator_Submit");
        private readonly By successMessage = By.Id("successMessage");
        private readonly By testLoginTab = By.XPath("//div[(text()='Test login')]");
        //private readonly By selectUserLogin = By.CssSelector(".list-link");
        private readonly By selectUserLogin = By.XPath("(//a[contains(@class, 'list-link')])[2]");

        private readonly By poolPageTile = By.ClassName("page-header");

        public CGMPortalLoginPage(IWebDriver driver, int timeoutInSeconds) : base(driver, timeoutInSeconds) { }

        // Login action with test login
        public void LoginWithTestLogin(string username, string password)
        {
            try
            {
                // Simplified with utility methods
                Click(testLoginTab);
                SendKeys(usernameField, username);
                SendKeys(passwordField, password);
                Click(loginButton);                
                Click(selectUserLogin);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during login: {ex.Message}");
                throw;
            }
        }

        // Verify if the login was successful
        public bool IsLoginSuccessful()
        {
            try
            {
                return FindElement(poolPageTile).Text.Equals("Puljer", StringComparison.OrdinalIgnoreCase);
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        // Click on authentication tab by index
        public void ClickOnAuthenTab(int tabIndex)
        {
            try
            {
                var elements = FindElements(By.ClassName("authentication-tab")); // Adjust locator as needed
                if (elements == null || !elements.Any())
                {
                    throw new Exception("No authentication tabs found.");
                }

                if (tabIndex < 0 || tabIndex >= elements.Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(tabIndex), "Invalid tab index.");
                }

                elements.ElementAt(tabIndex).Click();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error clicking on authentication tab: {ex.Message}");
                throw;
            }
        }
    }
}
