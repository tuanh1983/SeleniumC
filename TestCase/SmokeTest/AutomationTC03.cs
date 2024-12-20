using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Events;
using OpenQA.Selenium.Support.UI;
using System.ComponentModel;
using System;
using WebDriverManager.DriverConfigs.Impl;
using Automation.Util;
using Automation.PageObjects.CGM.Portal;
namespace Automation.TestCase.SmokeTest
{
    public class AutomationTC03 : BaseTC
    {/*
        HomePage homePage;
        ParkingFeePage parkingFeePage;
        ComplaintPage complaintPage;
        [SetUp]
        public void SetupTest()
        {
            // Initialize the page objects here to ensure they are ready for use in the test steps
            homePage = new HomePage(driver);
            parkingFeePage = new ParkingFeePage(driver);
            complaintPage = new ComplaintPage(driver);
        }

        //[Parallelizable(ParallelScope.Children)]
        [Test, TestCaseSource("AddTestDataConfig")]
        public void TestStep01_login(string user, string password)
        {
            Console.WriteLine("This is test 1");

            driver.FindElement(By.LinkText("Username & password")).Click();

            //Login Page
            CGMPortalLoginPage loginUserPassPage = new CGMPortalLoginPage(driver);
            homePage = loginUserPassPage.Login(user, password);
            Assert.IsTrue(homePage.VerifyPageLoadSccessfully());
        }
        [Test]
        public void TestStep02_HomePage()
        {

            //Home Page
            //HomePage homePage = new HomePage(driver);
            homePage.ClickOnDynamicLink("Afgifter");
            //Click on All parking fees on left Menu
            homePage.ClickOnDynamicLink("Alle afgifter");
            Assert.IsTrue(homePage.VerifyParkingFeesListLoadSccessfully());
        }
        [Test]
        public void TestStep03_ParkingFreePage()
        {
            //Parking fee's detail page
            ParkingFeePage parkingFreePage = homePage.ClickOnFirstParkingFeeRow();
            Assert.IsTrue(parkingFreePage.VerifyListTabLoadSccessfully());
            parkingFreePage.CliclOnComplaintsTab();
            Assert.IsTrue(parkingFreePage.VerifyListComplaintsLoadSccessfully());
        }
        [Test]
        public void TestStep04_ComplaintPage()
        {

            //ComplaintPage's detail page
            ComplaintPage complaintPage = parkingFeePage.CliclOnFristComplaint();
            Assert.IsTrue(complaintPage.VerifyComplaintsPageLoadSccessfully());

            complaintPage.SetValueOntxtComplaintDateTime(Keys.Control + "a");
            complaintPage.SetValueOntxtComplaintDateTime(" ");
            string borderColor = complaintPage.GetCssValueOfTxtComplaintDateTime("border-color");
            Console.WriteLine(borderColor);
            Assert.IsTrue(borderColor.Equals("rgb(219, 36, 30)"));
        }
        [TearDown]
        public void TearDown()
        {
            Console.WriteLine("This is TearDown");
            //Assert.Pass();
        }
        public static IEnumerable<TestCaseData> AddTestDataConfig()
        {
            yield return new TestCaseData(getDataParse().extracData("userName"), getDataParse().extracData("password"));
        }
    }*/
    }
}