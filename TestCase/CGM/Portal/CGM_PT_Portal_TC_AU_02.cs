using NUnit.Framework;
using OpenQA.Selenium;
using Automation.PageObjects.CGM.Portal;
using Automation.Tests.CGM.Portal;

namespace Automation.TestCase.CGM.Portal
{
    [TestFixture, Parallelizable(ParallelScope.All)]
    public class CGM_PT_Portal_TC_AU_02 : CGM_PT_Portal_BaseTestCase
    {
        private CGMPortalLoginPage loginPage;

        [SetUp]
        public void Init()
        {
            // Initialize LoginPage object with the WebDriver instance and timeout value
            loginPage = new CGMPortalLoginPage(driver, TimeoutInSeconds);
        }

        [Test, Category("CGMPT_Portal")]
        public void Test1()
        {
            // Act: Perform login actions
            loginPage.ClickOnAuthenTab(2);
        }
    }
}