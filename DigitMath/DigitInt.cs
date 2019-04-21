using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;

namespace DigitMath
{
    /// <summary>
    /// A digit oriented presentation of an Integer value.
    /// </summary>
    public class DigitInt : IComparable<DigitInt>, IConvertible
    {
        #region Attributes
        #region Indexers
        public Digit this[int index]
        {
            get => GetLSD(index);
        }
        #endregion
        /// <summary>
        /// The values of the digits.
        /// </summary>
        byte[] Digits { get; set; }
        /// <summary>
        /// The base of the <see cref="DigitInt"/>.
        /// </summary>
        byte Base { get; }
        /// <summary>
        /// Determines whether the value of the <see cref="DigitInt"/> is negative.
        /// </summary>
        public bool IsNegative { get; set; }
        /// <summary>
        /// The number of digits of the <see cref="DigitInt"/>.
        /// </summary>
        public int Length => Digits.Length;
        #endregion

        #region Constructors
        public DigitInt()
        {
            Base = 10;
            Digits = new byte[] { 0 };
            IsNegative = false;
        }

        public DigitInt(byte[] digits)
        {
            Base = 10;
            Digits = digits;
            IsNegative = false;
        }

        public DigitInt(byte[] digits, byte numBase, bool isNegative)
        {
            Base = numBase;
            Digits = digits;
            IsNegative = isNegative;
        }

        public DigitInt(int value)
        {
            Base = 10;
            
            if (value < 0)
            {
                IsNegative = true;
                value = -value;
            } else
            {
                IsNegative = false;
            }

            var numbers = new Queue<byte>();

            for (; value > 0; value /= Base)
                numbers.Enqueue((byte)(value % Base));

            Digits = numbers.Count > 0 ? numbers.ToArray() : new byte[] { 0 };
        }

        public DigitInt(int value, int radix)
        {
            Base = (byte)radix;

            if (value < 0)
            {
                IsNegative = true;
                value = -value;
            }
            else
            {
                IsNegative = false;
            }

            var numbers = new Queue<byte>();

            for (; value > 0; value /= Base)
                numbers.Enqueue((byte)(value % Base));

            Digits = numbers.Count > 0 ? numbers.ToArray() : new byte[] { 0 };
        }
        #endregion

        #region Methods
        #region Comparisions
        /// <summary>
        /// Determines the order the <see cref="DigitInt"/>s should be sorted in.
        /// </summary>
        /// <param name="other">The other value to compare this value to.</param>
        /// <returns>
        /// <c>-1</c>, if this value is smaller than <paramref name="other"/>,
        /// <c>0</c>, if this value is equal to <paramref name="other"/> and
        /// <c>1</c>, if this value is bigger than <paramref name="other"/>.
        /// </returns>
        public int CompareTo(DigitInt other)
        {
            if (other == null)
                return 1;

            if (IsNegative && !other.IsNegative)
                return -1;

            if (!IsNegative && other.IsNegative)
                return 1;

            if (IsNegative && other.IsNegative)
                return (-other).CompareTo(-Copy());

            if (Length.CompareTo(other.Length) != 0)
                return Length.CompareTo(other.Length);

            for (var i = 0; i < Length; i++)
            {
                if (GetMSD(i).CompareTo(other.GetMSD(i)) != 0)
                    return GetMSD(i).CompareTo(other.GetMSD(i));
            }

            return 0;
        }

        /// <summary>
        /// Determines whether <paramref name="left"/> is bigger than <paramref name="right"/>.
        /// </summary>
        /// <param name="left">The left <see cref="DigitInt"/> to compare.</param>
        /// <param name="right">The right <see cref="DigitInt"/> to compare.</param>
        /// <returns>
        /// <c>true</c>, if <paramref name="left"/> is bigger than <paramref name="right"/>, else <c>false</c>.
        /// </returns>
        public static bool operator >(DigitInt left, DigitInt right)
        {
            if (left == null)
                return false;

            return (left.CompareTo(right) > 0);
        }

        /// <summary>
        /// Determines whether <paramref name="left"/> is smaller than <paramref name="right"/>.
        /// </summary>
        /// <param name="left">The left <see cref="DigitInt"/> to compare.</param>
        /// <param name="right">The right <see cref="DigitInt"/> to compare.</param>
        /// <returns>
        /// <c>true</c>, if <paramref name="left"/> is smaller than <paramref name="right"/>, else <c>false</c>.
        /// </returns>
        public static bool operator <(DigitInt left, DigitInt right)
        {
            if (left == null)
                return true;

            return (left.CompareTo(right) < 0);
        }

        /// <summary>
        /// Determines whether <paramref name="left"/> is equal to <paramref name="right"/>.
        /// </summary>
        /// <param name="left">The left <see cref="DigitInt"/> to compare.</param>
        /// <param name="right">The right <see cref="DigitInt"/> to compare.</param>
        /// <returns>
        /// <c>true</c>, if <paramref name="left"/> is equal to <paramref name="right"/>, else <c>false</c>.
        /// </returns>
        public static bool operator ==(DigitInt left, DigitInt right)
        {
            if (left is null || right is null)
                return false;

            return left.Equals(right);
        }
        /// <summary>
        /// Determines whether <paramref name="left"/> is not equal to <paramref name="right"/>.
        /// </summary>
        /// <param name="left">The left <see cref="DigitInt"/> to compare.</param>
        /// <param name="right">The right <see cref="DigitInt"/> to compare.</param>
        /// <returns>
        /// <c>true</c>, if <paramref name="left"/> is not equal to <paramref name="right"/>, else <c>false</c>.
        /// </returns>
        public static bool operator !=(DigitInt left, DigitInt right)
        {
            if (left is null || right is null)
                return true;

            return !left.Equals(right);
        }

        /// <summary>
        /// Determines whether this value is equal to <paramref name="obj"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare this value to.</param>
        /// <returns>
        /// <c>true</c>, if <paramref name="left"/> is equal to <paramref name="right"/>, else <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj is DigitInt d)
            {
                return (Base == d.Base) && (IsNegative == d.IsNegative) 
                    && (Length == d.Length) && (Digits.SequenceEqual(d.Digits));
            }

            return false;
        }
        #endregion

        #region Arithmetics
        #region Operators
        #region Addition
        /// <summary>
        /// Adds two <see cref="DigitInt"/>s.
        /// </summary>
        /// <param name="leftSummand">The left <see cref="DigitInt"/> to add.</param>
        /// <param name="rightSummand">The right <see cref="DigitInt"/> to add.</param>
        /// <returns>The sum of the two <see cref="DigitInt"/>s.</returns>
        public static DigitInt Add(DigitInt leftSummand, DigitInt rightSummand)
        {
            if (leftSummand.Base != rightSummand.Base)
                throw new FormatException($"The radixes of the two numbers ({leftSummand.Base} and {rightSummand.Base}) do not match.");

            if (rightSummand.IsNegative)
                return leftSummand - (-rightSummand);

            if (leftSummand.IsNegative)
                return rightSummand - (-leftSummand);

            var addRadix = leftSummand.Base;
            var resultDigits = new LinkedList<byte>();
            var overhead = 0;
            var maxLength = Math.Max(leftSummand.Length, rightSummand.Length);

            for (var i = 0; i < maxLength || overhead != 0; i++)
            {
                var leftVal = leftSummand.GetLSD(i);
                var rightVal = rightSummand.GetLSD(i);
                var fullValue = (ushort)((ushort)leftVal + (ushort)rightVal + (ushort)overhead);
                var nextDigit = fullValue;

                overhead = 0;

                if (fullValue >= addRadix)
                {
                    nextDigit = (byte)(fullValue % addRadix);
                    overhead = (fullValue - nextDigit) / addRadix;
                }

                resultDigits.AddLast((byte)nextDigit);
            }

            return new DigitInt(resultDigits.ToArray(), addRadix, false);
        }

        /// <summary>
        /// Adds two <see cref="DigitInt"/>s.
        /// </summary>
        /// <param name="leftSummand">The left <see cref="DigitInt"/> to add.</param>
        /// <param name="rightSummand">The right <see cref="DigitInt"/> to add.</param>
        /// <returns>The sum of the two <see cref="DigitInt"/>s.</returns>
        public static DigitInt operator +(DigitInt leftSummand, DigitInt rightSummand)
        {
            return Add(leftSummand, rightSummand);
        }

        /// <summary>
        /// Adds <c>1</c> to <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The <see cref="DigitInt"/> to add <c>1</c> to</param>
        /// <returns>The successor of <paramref name="value"/>.</returns>
        public static DigitInt operator ++(DigitInt value)
        {
            return value + new DigitInt(1);
        }
        #endregion

        #region Subtraction
        /// <summary>
        /// Determines the difference of <paramref name="minuend"/> and <paramref name="subtrahend"/>.
        /// </summary>
        /// <param name="minuend">The left <see cref="DigitInt"/> to subtract.</param>
        /// <param name="subtrahend">The right <see cref="DigitInt"/> to subtract.</param>
        /// <returns>The difference of <paramref name="minuend"/> and <paramref name="subtrahend"/>.</returns>
        public static DigitInt Sub(DigitInt minuend, DigitInt subtrahend)
        {
            if (minuend.Base != subtrahend.Base)
                throw new FormatException($"The base of the two numbers ({minuend.Base} and {subtrahend.Base}) do not match.");

            if (subtrahend.IsNegative)
                return minuend + (-subtrahend);

            if (subtrahend > minuend)
                return -(subtrahend - minuend);

            var subBase = minuend.Base;
            var resultDigits = new LinkedList<byte>();
            var overhead = 0;
            var leftLength = minuend.Length;
            var rightLength = subtrahend.Length;

            for (var i = 0; i < Math.Max(leftLength, rightLength); i++)
            {
                var leftVal = minuend.GetLSD(i);
                var rightVal = subtrahend.GetLSD(i);

                var minuendDigit = (ushort)leftVal;
                var subtrahendDigit = (ushort)rightVal + (ushort)overhead;

                overhead = 0;

                if (subtrahendDigit > minuendDigit)
                {
                    overhead = 1;
                    minuendDigit += subBase;
                }

                var nextValue = (ushort)(minuendDigit - subtrahendDigit);

                resultDigits.AddLast((byte)nextValue);
            }

            return new DigitInt(resultDigits.ToArray(), subBase, false);
        }
        /// <summary>
        /// Determines the difference of <paramref name="left"/> and <paramref name="right"/>.
        /// </summary>
        /// <param name="left">The left <see cref="DigitInt"/> to subtract.</param>
        /// <param name="right">The right <see cref="DigitInt"/> to subtract.</param>
        /// <returns>The difference of <paramref name="left"/> and <paramref name="right"/>.</returns>
        public static DigitInt operator -(DigitInt left, DigitInt right)
        {
            return Sub(left, right);
        }

        /// <summary>
        /// Negates the <see cref="DigitInt"/>.
        /// </summary>
        /// <param name="value">The <see cref="DigitInt"/> to negate.</param>
        /// <returns>The negated <see cref="DigitInt"/>.</returns>
        public static DigitInt operator -(DigitInt value)
        {
            var negation = value.Copy();
            negation.IsNegative = !negation.IsNegative;
            return negation;
        }

        /// <summary>
        /// Subtracts <c>1</c> from <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The <see cref="DigitInt"/> to substract <c>1</c> of.</param>
        /// <returns>The predecessor of <paramref name="value"/>.</returns>
        public static DigitInt operator --(DigitInt value)
        {
            return value - new DigitInt(1);
        }
        #endregion

        #region Multiplication
        /// <summary>
        /// Determines the product of <paramref name="left"/> and <paramref name="right"/>.
        /// </summary>
        /// <param name="left">The left <see cref="DigitInt"/> to multiply.</param>
        /// <param name="right">The right <see cref="DigitInt"/> to multiply.</param>
        /// <returns>The product of <paramref name="left"/> and <paramref name="right"/>.</returns>
        public static DigitInt Mult(DigitInt left, DigitInt right)
        {
            var product = new DigitInt(0);
            var absLeft = left.Absolute();
            var absRight = right.Absolute();

            var counter = Min(absLeft, absRight);
            var summand = Max(absLeft, absRight);

            for (var i = new DigitInt(0); i < counter; i++)
            {
                product += summand;
            }

            product.IsNegative = left.IsNegative ^ right.IsNegative;

            return product;
        }
        /// <summary>
        /// Determines the product of <paramref name="left"/> and <paramref name="right"/>.
        /// </summary>
        /// <param name="left">The left <see cref="DigitInt"/> to multiply.</param>
        /// <param name="right">The right <see cref="DigitInt"/> to multiply.</param>
        /// <returns>The product of <paramref name="left"/> and <paramref name="right"/>.</returns>
        public static DigitInt operator *(DigitInt left, DigitInt right)
        {
            return Mult(left, right);
        }

        /// <summary>
        /// Divides two <see cref="DigitInt"/>s.
        /// </summary>
        /// <param name="dividend">The dividend of the division.</param>
        /// <param name="divisor">The divisor of the division.</param>
        /// <param name="remainder">The remainder of the division.</param>
        /// <returns>The quotient of the division.</returns>
        public static DigitInt Div(DigitInt dividend, DigitInt divisor, out DigitInt remainder)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Divides two <see cref="DigitInt"/>s.
        /// </summary>
        /// <param name="dividend">The dividend of the division.</param>
        /// <param name="divisor">The divisor of the division.</param>
        /// <returns>The quotient of the division.</returns>
        public static DigitInt Div(DigitInt dividend, DigitInt divisor)
        {
            return Div(dividend, divisor, out _);
        }

        /// <summary>
        /// Divides two <see cref="DigitInt"/>s.
        /// </summary>
        /// <param name="dividend">The dividend of the division.</param>
        /// <param name="divisor">The divisor of the division.</param>
        /// <returns>The quotient of the division.</returns>
        public static DigitInt operator /(DigitInt dividend, DigitInt divisor)
        {
            return Div(dividend, divisor);
        }

        /// <summary>
        /// Determines the modulo (remainder) of a division.
        /// </summary>
        /// <param name="dividend">The dividend of the division.</param>
        /// <param name="divisor">The divisor of the division.</param>
        /// <returns>The remainder of the division.</returns>
        public static DigitInt Mod(DigitInt dividend, DigitInt divisor)
        {
            Div(dividend, divisor, out var remainder);
            return remainder;
        }

        /// <summary>
        /// Determines the modulo (remainder) of a division.
        /// </summary>
        /// <param name="dividend">The dividend of the division.</param>
        /// <param name="divisor">The divisor of the division.</param>
        /// <returns>The remainder of the division.</returns>
        public static DigitInt operator %(DigitInt dividend, DigitInt divisor)
        {
            return Mod(dividend, divisor);
        }

        /// <summary>y
        /// Performs a left shift on the <paramref name="value"/>. Equal to a multiplication by the <see cref="Base"/>;
        /// </summary>
        /// <param name="value">The <see cref="DigitInt"/> to perform the left shift on.</param>
        /// <param name="shifts">The number of digits to shift the value by.</param>
        /// <returns>The left shifted <see cref="DigitInt"/>.</returns>
        public static DigitInt ShiftLeft(DigitInt value, int shifts)
        {
            if (value is null)
                throw new ArgumentNullException("The value may not be null.");

            if (shifts < 0)
                return ShiftRight(value, -shifts);

            if (value.IsZero())
                return new DigitInt(0);

            var newValue = value.Copy();
            var newDigits = new byte[newValue.Digits.Length + shifts];
            value.Digits.CopyTo(newDigits, shifts);
            newValue.Digits = newDigits;

            return newValue;
        }

        /// <summary>
        /// Performs a left shift on the <paramref name="value"/>. Equal to a multiplication by the <see cref="Base"/>;
        /// </summary>
        /// <param name="value">The <see cref="DigitInt"/> to perform the left shift on.</param>
        /// <param name="shifts">The number of digits to shift the value by.</param>
        /// <returns>The left shifted <see cref="DigitInt"/>.</returns>
        public static DigitInt operator <<(DigitInt value, int shifts)
        {
            return ShiftLeft(value, shifts);
        }

        /// <summary>
        /// Performs a right shift on the <paramref name="value"/>. Equal to a division by the <see cref="Base"/>;
        /// </summary>
        /// <param name="value">The <see cref="DigitInt"/> to perform the right shift on.</param>
        /// <param name="shifts">The number of digits to shift the value by.</param>
        /// <returns>The right shifted <see cref="DigitInt"/>.</returns>
        public static DigitInt ShiftRight(DigitInt value, int shifts)
        {
            if (value is null)
                throw new ArgumentNullException("The value may not be null.");

            if (shifts < 0)
                return ShiftLeft(value, -shifts);

            if (shifts >= value.Length)
                return new DigitInt(0);

            var newValue = value.Copy();
            var newDigits = value.Digits.Reverse().Take(newValue.Digits.Length - shifts).Reverse().ToArray();
            newValue.Digits = newDigits;

            return newValue;
        }

        /// <summary>
        /// Performs a right shift on the <paramref name="value"/>. Equal to a division by the <see cref="Base"/>;
        /// </summary>
        /// <param name="value">The <see cref="DigitInt"/> to perform the right shift on.</param>
        /// <param name="shifts">The number of digits to shift the value by.</param>
        /// <returns>The right shifted <see cref="DigitInt"/>.</returns>
        public static DigitInt operator >>(DigitInt value, int shifts)
        {
            return ShiftRight(value, shifts);
        }
        #endregion
        #endregion

        #region Sum
        /// <summary>
        /// Calculates the sum over all <paramref name="values"/>.
        /// </summary>
        /// <param name="values">The <see cref="DigitInt"/>s to sum up.</param>
        /// <returns>The sum of all <paramref name="values"/>.</returns>
        public static DigitInt Sum(IEnumerable<DigitInt> values)
        {
            var curSum = new DigitInt(0);

            foreach (var value in values)
            {
                curSum += value;
            }

            return curSum;
        }

        /// <summary>
        /// Calculates the sum over all <paramref name="values"/>.
        /// </summary>
        /// <param name="values">The <see cref="DigitInt"/>s to sum up.</param>
        /// <returns>The sum of all <paramref name="values"/>.</returns>
        public static DigitInt Sum(params DigitInt[] values)
        {
            var curSum = new DigitInt(0);

            foreach (var value in values)
            {
                curSum += value;
            }

            return curSum;
        }
        #endregion

        #region Product
        /// <summary>
        /// Determines the product of the given <paramref name="values"/>.
        /// </summary>
        /// <param name="values">The <see cref="DigitInt"/>s to multiply together.</param>
        /// <returns>The product of the given <paramref name="values"/>.</returns>
        public static DigitInt Product(IEnumerable<DigitInt> values)
        {
            var curProduct = new DigitInt(1);

            foreach (var value in values)
            {
                curProduct *= value;

                if (curProduct == new DigitInt(0))
                    return curProduct;
            }

            return curProduct;
        }

        /// <summary>
        /// Determines the product of the given <paramref name="values"/>.
        /// </summary>
        /// <param name="values">The <see cref="DigitInt"/>s to multiply together.</param>
        /// <returns>The product of the given <paramref name="values"/>.</returns>
        public static DigitInt Product(params DigitInt[] values)
        {
            var curProduct = new DigitInt(1);

            foreach (var value in values)
            {
                curProduct *= value;

                if (curProduct == new DigitInt(0))
                    return curProduct;
            }

            return curProduct;
        }
        #endregion

        #region Max
        /// <summary>
        /// Determines the maximum <see cref="DigitInt"/> of all <paramref name="values"/>.
        /// </summary>
        /// <param name="values">The values to get the maximum of.</param>
        /// <returns>The maximum <see cref="DigitInt"/> of all <paramref name="values"/>.</returns>
        public static DigitInt Max(IEnumerable<DigitInt> values)
        {
            return values.Max();
        }

        /// <summary>
        /// Determines the maximum <see cref="DigitInt"/> of all <paramref name="values"/>.
        /// </summary>
        /// <param name="values">The values to get the maximum of.</param>
        /// <returns>The maximum <see cref="DigitInt"/> of all <paramref name="values"/>.</returns>
        public static DigitInt Max(params DigitInt[] values)
        {
            return values.Max();
        }
        #endregion

        #region Min
        /// <summary>
        /// Determines the minimum <see cref="DigitInt"/> of all <paramref name="values"/>.
        /// </summary>
        /// <param name="values">The values to get the minimum of.</param>
        /// <returns>The minimum <see cref="DigitInt"/> of all <paramref name="values"/>.</returns>
        public static DigitInt Min(IEnumerable<DigitInt> values)
        {
            return values.Min();
        }

        /// <summary>
        /// Determines the minimum <see cref="DigitInt"/> of all <paramref name="values"/>.
        /// </summary>
        /// <param name="values">The values to get the minimum of.</param>
        /// <returns>The minimum <see cref="DigitInt"/> of all <paramref name="values"/>.</returns>
        public static DigitInt Min(params DigitInt[] values)
        {
            return values.Min();
        }
        #endregion

        #region Absolute
        /// <summary>
        /// Determines the absolute value of the <see cref="DigitInt"/>.
        /// </summary>
        /// <returns>The absolute value of the <see cref="DigitInt"/>.</returns>
        public DigitInt Absolute()
        {
            var absolute = Copy();
            absolute.IsNegative = false;
            return absolute;
        }

        /// <summary>
        /// Determines whether the <see cref="DigitInt"/> represents the value <c>0</c>.
        /// </summary>
        /// <returns>
        /// <c>true</c>, if the <see cref="DigitInt"/> represents the value <c>0</c>, else <c>false</c>.
        /// </returns>
        public bool IsZero()
        {
            foreach (var digitValue in Digits)
            {
                if (digitValue != 0)
                    return false;
            }

            return true;
        }
        #endregion

        /// <summary>
        /// Gets the hashcode of the <see cref="DigitInt"/>.
        /// </summary>
        /// <returns>The hashcode of the <see cref="DigitInt"/>.</returns>
        public override int GetHashCode()
        {
            return Digits.GetHashCode();
        }
        #endregion

        public void SetBase(byte newBase)
        {
            if (newBase == Base)
                return;

            throw new NotImplementedException();
        }

        protected void ToBase10()
        {
            if (Base == 10)
                return;

            throw new NotImplementedException();
        }

        protected void FromBase10(byte newBase)
        {
            if (Base != 10)
                throw new FormatException($"The number is in base {Base}, but has to be in base 10.");

            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a copy of the <see cref="DigitInt"/>.
        /// </summary>
        /// <returns>A copy of the <see cref="DigitInt"/>.</returns>
        public DigitInt Copy()
        {
            return new DigitInt(Digits, Base, IsNegative);
        }

        #region Accessors
        #region Least Significant Digit
        /// <summary>
        /// Gets a <see cref="Digit"/> of the <see cref="DigitInt"/>, starting at the least significant <see cref="Digit"/>.
        /// </summary>
        /// <param name="index">The index of the <see cref="Digit"/>, starting at the least significant <see cref="Digit"/>.</param>
        /// <returns>
        /// The <see cref="Digit"/> at the position <paramref name="index"/>, starting at the least significant <see cref="Digit"/>.
        /// </returns>
        public Digit GetLSD(int index)
        {
            if (index < 0)
                throw new IndexOutOfRangeException($"The index ({index}) must not be less than 0.");

            if (index >= Length)
                return new Digit((byte)0, Base);

            return new Digit(Digits[index], Base);
        }

        /// <summary>
        /// Gets the least significant <see cref="Digit"/> of the <see cref="DigitInt"/>.
        /// </summary>
        /// <returns>
        /// The least significant <see cref="Digit"/> of the <see cref="DigitInt"/>.
        /// </returns>
        public Digit GetLSD()
        {
            return GetLSD(0);
        }
        #endregion

        #region Most Significant Digit
        /// <summary>
        /// Gets a <see cref="Digit"/> of the <see cref="DigitInt"/>, starting at the most significant <see cref="Digit"/>.
        /// </summary>
        /// <param name="index">The index of the <see cref="Digit"/>, starting at the most significant <see cref="Digit"/>.</param>
        /// <returns>
        /// The <see cref="Digit"/> at the position <paramref name="index"/>, 
        /// starting at the most significant <see cref="Digit"/>.
        /// </returns>
        public Digit GetMSD(int index)
        {
            if (index >= Length)
                throw new IndexOutOfRangeException($"The index ({index}) must not be greater  than or equal to the number of digits ({Length}).");

            if (index < 0)
                return new Digit((byte)0, Base);

            return new Digit(Digits[Length - index - 1], Base);
        }

        /// <summary>
        /// Gets the most significant <see cref="Digit"/> of the <see cref="DigitInt"/>.
        /// </summary>
        /// <returns>
        /// The most significant <see cref="Digit"/> of the <see cref="DigitInt"/>.
        /// </returns>
        public Digit GetMSD()
        {
            return GetMSD(0);
        }
        #endregion
        #endregion

        #region Conversions
        #region Implicit
        #region ToDigitInt
        /// <summary>
        /// Converts an <see cref="Int32"/> to a <see cref="DigitInt"/> implicitly.
        /// </summary>
        /// <param name="value">The <see cref="Int32"/> to convert to a <see cref="DigitInt"/>.</param>
        public static implicit operator DigitInt(int value)
        {
            return new DigitInt(value);
        }
        #endregion
        #endregion

        #region Explicit
        #region FromDigitInt
        public static explicit operator bool(DigitInt value)
        {
            return value.ToBoolean(null);
        }

        public static explicit operator byte(DigitInt value)
        {
            return value.ToByte(null);
        }

        public static explicit operator char(DigitInt value)
        {
            return value.ToChar(null);
        }

        public static explicit operator decimal(DigitInt value)
        {
            return value.ToDecimal(null);
        }

        public static explicit operator double(DigitInt value)
        {
            return value.ToDouble(null);
        }

        public static explicit operator short(DigitInt value)
        {
            return value.ToInt16(null);
        }

        public static explicit operator int(DigitInt value)
        {
            return value.ToInt32(null);
        }

        public static explicit operator long(DigitInt value)
        {
            return value.ToInt64(null);
        }

        public static explicit operator sbyte(DigitInt value)
        {
            return value.ToSByte(null);
        }

        public static explicit operator float(DigitInt value)
        {
            return value.ToSingle(null);
        }

        public static explicit operator BigInteger(DigitInt value)
        {
            return value.ToBigInteger(null);
        }

        public static explicit operator ushort(DigitInt value)
        {
            return value.ToUInt16(null);
        }

        public static explicit operator uint(DigitInt value)
        {
            return value.ToUInt32(null);
        }

        public static explicit operator ulong(DigitInt value)
        {
            return value.ToUInt64(null);
        }

        public static explicit operator string(DigitInt value)
        {
            return value.ToString();
        }
        #endregion
        #endregion

        #region IConvertable
        public TypeCode GetTypeCode()
        {
            return TypeCode.Object;
        }

        public bool ToBoolean(IFormatProvider provider)
        {
            if (Equals(new DigitInt(0)))
                return false;

            return true;
        }

        public byte ToByte(IFormatProvider provider)
        {
            return (byte)ToBigInteger(provider);
        }

        public char ToChar(IFormatProvider provider)
        {
            return (char)ToBigInteger(provider);
        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            return Convert.ToDateTime(ToInt64(provider));
        }

        public decimal ToDecimal(IFormatProvider provider)
        {
            return (decimal)ToBigInteger(provider);
        }

        public double ToDouble(IFormatProvider provider)
        {
            return (double)ToBigInteger(provider);
        }

        public short ToInt16(IFormatProvider provider)
        {
            return (short)ToBigInteger(provider);
        }

        public int ToInt32(IFormatProvider provider)
        {
            return (int)ToBigInteger(provider);
        }

        public long ToInt64(IFormatProvider provider)
        {
            return (long)ToBigInteger(provider);
        }

        public sbyte ToSByte(IFormatProvider provider)
        {
            return (sbyte)ToBigInteger(provider);
        }

        public float ToSingle(IFormatProvider provider)
        {
            return (float)ToBigInteger(provider);
        }

        public string ToString(IFormatProvider provider)
        {
            return ToString();
        }

        public object ToType(Type conversionType, IFormatProvider provider)
        {
            return Convert.ChangeType(ToInt64(provider), conversionType);
        }

        public ushort ToUInt16(IFormatProvider provider)
        {
            return (ushort)ToBigInteger(provider);
        }

        public uint ToUInt32(IFormatProvider provider)
        {
            return (uint)ToBigInteger(provider);
        }

        public ulong ToUInt64(IFormatProvider provider)
        {
            return (ulong)ToBigInteger(provider);
        }
        #endregion

        public BigInteger ToBigInteger(IFormatProvider provider)
        {
            var result = new BigInteger(0);

            for (var i = Length - 1; i >= 0; i--)
            {
                result *= Base;
                result += Digits[i];
            }

            if (IsNegative)
                result = -result;

            return result;
        }

        /// <summary>
        /// Converts the <see cref="DigitInt"/> to its <see cref="string"/> representation.
        /// </summary>
        /// <returns>The <see cref="string"/> representation of the <see cref="DigitInt"/>.</returns>
        public override string ToString()
        {
            var str = new StringBuilder();

            if (IsNegative)
                str.Append("-");

            for (var i = 0; i < Length; i++)
            {
                str.Append(Digit.ToChar(GetMSD(i)));
            }

            return str.ToString();
        }
        #endregion
        #endregion
    }
}
