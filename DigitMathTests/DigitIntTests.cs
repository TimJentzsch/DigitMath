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

            left = new DigitInt(0);
            right = new DigitInt(4);
            Assert.AreEqual(new DigitInt(4), left + right,
                $"{left} + {right}");

            left = new DigitInt(7);
            right = new DigitInt(7);
            Assert.AreEqual(new DigitInt(14), left + right,
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
        public void TestProduct()
        {
            var values = new DigitInt[]
            {
                new DigitInt(13),
                new DigitInt(2),
                new DigitInt(1114),
                new DigitInt(1),
                new DigitInt(6),
                new DigitInt(99)
            };
            Assert.AreEqual(new DigitInt(17204616), DigitInt.Product(values));
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
        public void TestMult()
        {
            var left = new DigitInt(7);
            var right = new DigitInt(5);
            Assert.AreEqual(new DigitInt(35), left * right, $"{left} * {right}");

            left = new DigitInt(6);
            right = new DigitInt(92);
            Assert.AreEqual(new DigitInt(552), left * right, $"{left} * {right}");

            left = new DigitInt(92);
            right = new DigitInt(6);
            Assert.AreEqual(new DigitInt(552), left * right, $"{left} * {right}");

            left = new DigitInt(-9);
            right = new DigitInt(202);
            Assert.AreEqual(new DigitInt(-1818), left * right, $"{left} * {right}");

            left = new DigitInt(-202);
            right = new DigitInt(9);
            Assert.AreEqual(new DigitInt(-1818), left * right, $"{left} * {right}");

            left = new DigitInt(-55);
            right = new DigitInt(-202);
            Assert.AreEqual(new DigitInt(11110), left * right, $"{left} * {right}");
        }

        [TestMethod]
        public void TestShiftLeft()
        {
            var value = new DigitInt(3);
            var result = value << 3;
            Assert.AreEqual(new DigitInt(3000), result, $"{value} << 3");
            Assert.AreEqual(4, result.Length, $"Length of {value} << 3");

            value = new DigitInt(0);
            result = value << 3;
            Assert.AreEqual(new DigitInt(0), result, $"{value} << 3");
            Assert.AreEqual(1, result.Length, $"Length of {value} << 3");

            value = new DigitInt(2672);
            result = value << 2;
            Assert.AreEqual(new DigitInt(267200), result, $"{value} << 2");
            Assert.AreEqual(6, result.Length, $"Length of {value} << 2");

            value = new DigitInt(369);
            result = value << 0;
            Assert.AreEqual(new DigitInt(369), result, $"{value} << 0");
            Assert.AreEqual(3, result.Length, $"Length of {value} << 0");

            value = new DigitInt(178);
            result = value << -1;
            Assert.AreEqual(new DigitInt(17), result, $"{value} << -1");
            Assert.AreEqual(2, result.Length, $"Length of {value} << -1");

            value = new DigitInt(369);
            result = value << -5;
            Assert.AreEqual(new DigitInt(0), result, $"{value} << -5");
            Assert.AreEqual(1, result.Length, $"Length of {value} << -5");
        }

        [TestMethod]
        public void TestShiftRight()
        {
            var value = new DigitInt(3000);
            var result = value >> 5;
            Assert.AreEqual(new DigitInt(0), result, $"{value} >> 5");
            Assert.AreEqual(1, result.Length, $"Length of {value} >> 5");

            value = new DigitInt(0);
            result = value >> 3;
            Assert.AreEqual(new DigitInt(0), result, $"{value} >> 3");
            Assert.AreEqual(1, result.Length, $"Length of {value} >> 3");

            value = new DigitInt(2672);
            result = value >> 2;
            Assert.AreEqual(new DigitInt(26), result, $"{value} >> 2");
            Assert.AreEqual(2, result.Length, $"Length of {value} >> 2");

            value = new DigitInt(369);
            result = value >> 0;
            Assert.AreEqual(new DigitInt(369), result, $"{value} >> 0");
            Assert.AreEqual(3, result.Length, $"Length of {value} >> 0");

            value = new DigitInt(178);
            result = value >> -1;
            Assert.AreEqual(new DigitInt(1780), result, $"{value} >> -1");
            Assert.AreEqual(4, result.Length, $"Length of {value} >> -1");

            value = new DigitInt(369);
            result = value >> -5;
            Assert.AreEqual(new DigitInt(36900000), result, $"{value} >> -5");
            Assert.AreEqual(8, result.Length, $"Length of {value} >> -5");
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
