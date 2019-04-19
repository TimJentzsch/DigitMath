﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DigitMath;

namespace DigitMathTests
{
    [TestClass]
    public class DigitIntTests : DigitInt
    {
        #region Attributes
        [TestMethod]
        public void TestLength()
        {
            var num = new DigitInt(new byte[] { 1 });
            Assert.AreEqual(1, num.Length);

            num = new DigitInt(new byte[] { 1, 2 });
            Assert.AreEqual(2, num.Length);
        }
        #endregion

        #region Constructors
        [TestMethod]
        public void TestCustructorEquality()
        {
            var num1 = new DigitInt(new byte[] { 0 });
            var num2 = new DigitInt(0);
            Assert.AreEqual(num1, num2, "0 not created equally");

            num1 = new DigitInt(new byte[] { 1, 2, 3 });
            num2 = new DigitInt(123);
            Assert.AreEqual(num1, num2, $"123 not created equally: {num1} | {num2}");
        }
        #endregion

        #region Methods
        [TestMethod]
        public void TestAdd()
        {
            var left = new DigitInt(1);
            var right = new DigitInt(2);
            Assert.AreEqual(new DigitInt(3), left + right);

            left = new DigitInt(55);
            right = new DigitInt(202);
            Assert.AreEqual(new DigitInt(257), left + right);

            left = new DigitInt(1332);
            right = new DigitInt(99);
            Assert.AreEqual(new DigitInt(1431), left + right);
        }

        [TestMethod]
        public void TestToString()
        {
            var num = new DigitInt(new byte[] { 1, 2, 3 });
            Assert.AreEqual("123", num.ToString());

            num = new DigitInt(new byte[] { 10, 3, 15 });
            Assert.AreEqual("A3F", num.ToString());
        }
        #endregion
    }
}
