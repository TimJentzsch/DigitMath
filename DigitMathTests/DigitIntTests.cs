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

            num1 = new DigitInt(new byte[] { 3, 2, 1 });
            num2 = new DigitInt(123);
            Assert.AreEqual(num1, num2, $"123 not created equally: {num1} | {num2}");
        }
        #endregion

        #region Methods
        [TestMethod]
        public void TestAdd()
        {
            TestAddHelper(1, 2);
            TestAddHelper(55, 202);
            TestAddHelper(1332, 99);
            TestAddHelper(-1332, 99);
            TestAddHelper(1332, -99);
            TestAddHelper(-1332, -99);
            TestAddHelper(0, 4);
            TestAddHelper(7, 7);
        }

        void TestAddHelper(int leftSummand, int rightSummand)
        {
            var left = new DigitInt(leftSummand);
            var right = new DigitInt(rightSummand);
            var sum = new DigitInt(leftSummand + rightSummand);
            Assert.AreEqual(sum, left + right, $"{leftSummand} + {rightSummand}");
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
            TestSubHelper(7, 5);
            TestSubHelper(55, 202);
            TestSubHelper(1332, 99);
            TestSubHelper(-1332, 99);
            TestSubHelper(1332, -99);
            TestSubHelper(-1332, -99);
        }

        void TestSubHelper(int minuend, int subtrahend)
        {
            var left = new DigitInt(minuend);
            var right = new DigitInt(subtrahend);
            var dif = new DigitInt(minuend - subtrahend);
            Assert.AreEqual(dif, left - right, $"{left} - {right}");
        }

        [TestMethod]
        public void TestMult()
        {
            TestMultHelper(7, 5);
            TestMultHelper(6, 92);
            TestMultHelper(92, 6);
            TestMultHelper(-9, 202);
            TestMultHelper(-202, 9);
            TestMultHelper(-55, -202);
        }

        public void TestMultHelper(int leftFactor, int rightFactor)
        {
            var left = new DigitInt(leftFactor);
            var right = new DigitInt(rightFactor);
            var product = new DigitInt(leftFactor * rightFactor);
            Assert.AreEqual(product, left * right, $"{leftFactor * rightFactor}");
        }

        [TestMethod]
        public void TestDiv()
        {
            TestDivHelper(15, 5);
            TestDivHelper(333, 12);
            TestDivHelper(49, -7);
            TestDivHelper(-49, 7);
            TestDivHelper(-49, -7);
        }

        public void TestDivHelper(int dividend, int divisor)
        {
            var left = new DigitInt(dividend);
            var right = new DigitInt(divisor);
            var quotient = new DigitInt(dividend / divisor);
            var modus = new DigitInt(dividend % divisor);
            Assert.AreEqual(quotient, left / right, $"{left / right}");
            Assert.AreEqual(modus, left % right, $"{left % right}");
        }

        [TestMethod]
        public void TestShiftLeft()
        {
            TestShiftLeftHelper(3, 3, 3000);
            TestShiftLeftHelper(0, 3, 0);
            TestShiftLeftHelper(2672, 2, 267200);
            TestShiftLeftHelper(369, 0, 369);
            TestShiftLeftHelper(178, -1, 17);
            TestShiftLeftHelper(369, -5, 0);
        }

        public void TestShiftLeftHelper(int value, int shifts, int expected)
        {
            var digitInt = new DigitInt(value);
            var exp = new DigitInt(expected);
            var actual = digitInt << shifts;
            Assert.AreEqual(exp, actual, $"{value} << {shifts}");
            Assert.AreEqual(exp.Length, actual.Length, $"Length of ({value} << {shifts})");
        }

        [TestMethod]
        public void TestShiftRight()
        {
            TestShiftRightHelper(3000, 5, 0);
            TestShiftRightHelper(0, 3, 0);
            TestShiftRightHelper(2672, 2, 26);
            TestShiftRightHelper(369, 0, 369);
            TestShiftRightHelper(178, -1, 1780);
            TestShiftRightHelper(369, -5, 36900000);
        }

        public void TestShiftRightHelper(int value, int shifts, int expected)
        {
            var digitInt = new DigitInt(value);
            var exp = new DigitInt(expected);
            var actual = digitInt >> shifts;
            Assert.AreEqual(exp, actual, $"{value} >> {shifts}");
            Assert.AreEqual(exp.Length, actual.Length, $"Length of ({value} >> {shifts})");
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
        public void TestRadix()
        {
            TestRadixHelper(3, 2, "11");
            TestRadixHelper(15, 16, "F");
            TestRadixHelper(-2201, 16, "-899");
            TestRadixHelper(8590, 2, "10000110001110");
        }

        public void TestRadixHelper(int value, int radix, string str)
        {
            var digitValue = new DigitInt(value, radix);
            Assert.AreEqual(str, digitValue.ToString(), $"({value})_{radix}");
        }

        [TestMethod]
        public void TestDigitSum()
        {
            TestDigitSumHelper(123456789, 45);
            TestDigitSumHelper(6702, 15);
        }

        public void TestDigitSumHelper(int value, int digitSum)
        {
            var digitVal = new DigitInt(value);
            var expectedSum = new DigitInt(digitSum);
            var actualSum = digitVal.DigitSum();
            Assert.AreEqual(expectedSum, actualSum, $"DigitSum ({value})");
        }

        [TestMethod]
        public void TestDigitProduct()
        {
            TestDigitProductHelper(123456789, 362880);
            TestDigitProductHelper(6702, 0);
        }

        public void TestDigitProductHelper(int value, int digitProduct)
        {
            var digitVal = new DigitInt(value);
            var expectedProduct = new DigitInt(digitProduct);
            var actualProduct = digitVal.DigitProduct();
            Assert.AreEqual(expectedProduct, actualProduct, $"DigitProduct ({value})");
        }

        [TestMethod]
        public void TestToString()
        {
            var num = new DigitInt(123);
            Assert.AreEqual("123", num.ToString());

            num = new DigitInt(2623, 16);
            Assert.AreEqual("A3F", num.ToString());
        }
        #endregion
    }
}
