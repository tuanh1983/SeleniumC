using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace Automation.Drivers
{
    public class ChromeDriverManager : BaseDriverManager
    {
        protected override IWebDriver CreateDriver()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("--disable-notifications");
            return new ChromeDriver(options);
        }

    }
    


}
