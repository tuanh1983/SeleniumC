using Automation.Utils;
using OpenQA.Selenium;

namespace Automation.PageObjects.CGM.Portal
{
    public class CGMPortalLoginPage : CGMPortalBasePage
    {
        // Define locators for login page elements
        private readonly By usernameField = By.Id("username");
        private readonly By passwordField = By.Id("password");
        private readonly By loginButton = By.Id("loginButton");
        private readonly By successMessage = By.Id("successMessage");

        public CGMPortalLoginPage(IWebDriver driver, int timeoutInSeconds) : base(driver, timeoutInSeconds) { }

        // Login action
        public void Login(string username, string password)
        {
            try
            {
                FindElement(usernameField).SendKeys(username);
                FindElement(passwordField).SendKeys(password);
                FindElement(loginButton).Click();
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error during login: {ex.Message}");
                throw;
            }
        }

        // Verify if the login was successful
        public bool IsLoginSuccessful()
        {
            try
            {
                return FindElement(successMessage).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        // Click on authentication tab
        public void ClickOnAuthenTab(int tabIndex)
        {
            // Implementation for clicking on the authentication tab
        }
    }
}