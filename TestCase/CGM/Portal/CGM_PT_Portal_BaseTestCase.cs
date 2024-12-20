using NUnit.Framework;

namespace Automation.Tests.CGM.Portal
{
    public class CGM_PT_Portal_BaseTestCase : BaseTestCase
    {
        [SetUp]
        public override void SetUp()
        {
            // Hardcode Product and Module for CGM Portal
            Product = "CGM";
            Module = "Portal";

            // Call base setup
            base.SetUp();
        }
    }
}
