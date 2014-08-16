using System;
using GameLogic.Maths;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameUnitTest.MathsTests
{
    [TestClass]
    public class WhenUsingMaths
    {
        [TestMethod]
        public void ShouldCalculateHypotenusLength()
        {
            var a = 2;
            var b = 3;
            var c = MathematicalFunctions.PythagorusGetHypotenusLengthFromRightAngledLengths(2, 3);
            c = Math.Round(c, 2);
            Assert.IsTrue(c == 3.61);
        }
    }
}
