using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System.Threading;

namespace Automation.Drivers
{
    public class FirefoxDriverManager : BaseDriverManager
    {
        protected override IWebDriver CreateDriver()
        {
            var options = new FirefoxOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("--disable-notifications");
            return new FirefoxDriver(options);
        }

    }



}
