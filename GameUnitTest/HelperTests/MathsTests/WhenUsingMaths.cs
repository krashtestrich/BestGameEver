using System;
using GameLogic.Helpers.Maths;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameUnitTest.HelperTests.MathsTests
{
    [TestClass]
    public class WhenUsingMaths
    {
        [TestMethod]
        public void ShouldCalculateHypotenusLength()
        {
            const int a = 2;
            const int b = 3;
            var c = MathematicalFunctions.PythagorusGetHypotenusLengthFromRightAngledLengths(a, b);
            c = Math.Round(c, 2);
            Assert.IsTrue(c == 3.61);
        }
    }
}
