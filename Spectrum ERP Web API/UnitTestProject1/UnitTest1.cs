using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spectrum_ERP_Web_API.Controllers;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Arrange
            TestController TC = new TestController();
            int v = 2;

            //Act
            var result = TC.getUserCounts(v);

            Assert.AreEqual(v * 5, result);
            


        }
    }
}
