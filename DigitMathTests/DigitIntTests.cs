using System;
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
            Assert.AreEqual(new DigitInt(3), left + right,
                $"{left} + {right}");

            left = new DigitInt(55);
            right = new DigitInt(202);
            Assert.AreEqual(new DigitInt(257), left + right,
                $"{left} + {right}");

            left = new DigitInt(1332);
            right = new DigitInt(99);
            Assert.AreEqual(new DigitInt(1431), left + right,
                $"{left} + {right}");

            left = new DigitInt(-1332);
            right = new DigitInt(99);
            Assert.AreEqual(new DigitInt(-1233), left + right,
                $"{left} + {right}");

            left = new DigitInt(1332);
            right = new DigitInt(-99);
            Assert.AreEqual(new DigitInt(1233), left + right,
                $"{left} + {right}");

            left = new DigitInt(-1332);
            right = new DigitInt(-99);
            Assert.AreEqual(new DigitInt(-1431), left + right,
                $"{left} + {right}");
        }

        [TestMethod]
        public void TestSum()
        {
            var values = new DigitInt[]
            {
                new DigitInt(13),
                new DigitInt(2),
                new DigitInt(1114),
                new DigitInt(0),
                new DigitInt(6),
                new DigitInt(99)
            };
            Assert.AreEqual(new DigitInt(1234), DigitInt.Sum(values));
        }

        [TestMethod]
        public void TestSub()
        {
            var left = new DigitInt(7);
            var right = new DigitInt(5);
            Assert.AreEqual(new DigitInt(2), left - right, $"{left} - {right}");

            left = new DigitInt(55);
            right = new DigitInt(202);
            Assert.AreEqual(new DigitInt(-147), left - right, $"{left} - {right}");

            left = new DigitInt(1332);
            right = new DigitInt(99);
            Assert.AreEqual(new DigitInt(1233), left - right, $"{left} - {right}");

            left = new DigitInt(-1332);
            right = new DigitInt(99);
            Assert.AreEqual(new DigitInt(-1431), left - right, $"{left} - {right}");

            left = new DigitInt(1332);
            right = new DigitInt(-99);
            Assert.AreEqual(new DigitInt(1431), left - right, $"{left} - {right}");

            left = new DigitInt(-1332);
            right = new DigitInt(-99);
            Assert.AreEqual(new DigitInt(-1233), left - right, $"{left} - {right}");
        }

        [TestMethod]
        public void TestCompareTo()
        {
            var left = new DigitInt(7);
            var right = new DigitInt(5);
            Assert.IsFalse(left < right, $"{left} < {right}");

            left = new DigitInt(7);
            right = new DigitInt(5);
            Assert.IsTrue(left > right, $"{left} > {right}");

            left = new DigitInt(-10);
            right = new DigitInt(5);
            Assert.IsTrue(left < right, $"{left} < {right}");

            left = new DigitInt(-10);
            right = new DigitInt(5);
            Assert.IsFalse(left > right, $"{left} > {right}");

            left = new DigitInt(-7);
            right = new DigitInt(-5);
            Assert.IsTrue(left < right, $"{left} < {right}");

            left = new DigitInt(-7);
            right = new DigitInt(-5);
            Assert.IsFalse(left > right, $"{left} > {right}");
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
