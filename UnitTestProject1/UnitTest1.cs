using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VisitorInfo;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async void testAsync()
        {
            string data = await new VisitorList().getAllVisitIdTest("https://api-dev.interfacema.de/visits");
            Assert.IsNotNull(data);

        }
    }
}
