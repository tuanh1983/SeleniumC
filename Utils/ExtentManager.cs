using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;
using System.IO;

namespace Automation.Utils
{
    public static class ExtentManager
    {
        private static ExtentReports? _extent;
        private static ExtentSparkReporter? _sparkReporter;

        public static ExtentReports GetInstance()
        {
            if (_extent == null)
            {
                // Define the path for the report
                string reportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ExtentReports", "SparkReport.html");

                // Ensure the directory exists
                Directory.CreateDirectory(Path.GetDirectoryName(reportPath)!);

                // Initialize the Spark reporter
                _sparkReporter = new ExtentSparkReporter(reportPath);

                // Optionally configure the Spark reporter
                _sparkReporter.Config.DocumentTitle = "Automation Test Report";
                _sparkReporter.Config.ReportName = "Extent Report - Spark";
                //_sparkReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;

                // Attach the Spark reporter to ExtentReports
                _extent = new ExtentReports();
                _extent.AttachReporter(_sparkReporter);

                // Add optional system/environment details
                _extent.AddSystemInfo("Environment", "QA");
                _extent.AddSystemInfo("Tester", "Your Name");
            }

            return _extent;
        }
    }
}
