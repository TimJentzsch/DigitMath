using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitMath
{
    /// <summary>
    /// A digit oriented presentation of an Integer value.
    /// </summary>
    public class DigitInt : IComparable<DigitInt>
    {
        #region Attributes
        #region Indexers
        public Digit this[int index]
        {
            get
            {
                if (index < 0 || index >= Digits.Length)
                    throw new IndexOutOfRangeException($"The index ({index}) must be between 0 and {Digits.Length - 1}");

                return new Digit(Digits[index], Base);
            }
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

        public DigitInt(byte[] digits, byte numBase)
        {
            Base = numBase;
            Digits = digits;
            IsNegative = false;
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

            var numbers = new Stack<byte>();

            for (; value > 0; value /= Base)
                numbers.Push((byte)(value % Base));

            Digits = numbers.Count > 0 ? numbers.ToArray() : new byte[] { 0 };
        }
        #endregion

        #region Methods
        #region Operations
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

            if (Length.CompareTo(other.Length) != 0)
                return Length.CompareTo(other.Length);

            for (var i = 0; i < Length; i++)
            {
                if (this[i].CompareTo(other[i]) != 0)
                    return this[i].CompareTo(other[i]);
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
        /// Adds two <see cref="DigitInt"/>s.
        /// </summary>
        /// <param name="left">The left <see cref="DigitInt"/> to add.</param>
        /// <param name="right">The right <see cref="DigitInt"/> to add.</param>
        /// <returns>The sum of the two <see cref="DigitInt"/>s.</returns>
        public static DigitInt operator +(DigitInt left, DigitInt right)
        {
            if (left.Base != right.Base)
                throw new FormatException($"The base of the two numbers ({left.Base} and {right.Base}) do not match.");

            var addBase = left.Base;
            var resultDigits = new LinkedList<byte>();
            var overhead = 0;
            var leftLength = left.Length;
            var rightLength = right.Length;

            for (var i = 1; i <= Math.Max(leftLength, rightLength); i++)
            {
                var leftVal = (leftLength - i) < 0 ? 0 : (ushort)left[leftLength - i];
                var rightVal = (rightLength - i) < 0 ? 0 : (ushort)right[rightLength - i];
                var fullValue = (ushort)((ushort)leftVal + (ushort)rightVal +  (ushort)overhead);
                var nextValue = fullValue;

                overhead = 0;

                if (fullValue >= addBase)
                {
                    nextValue = (byte)(fullValue % addBase);
                    overhead = (fullValue - nextValue) / addBase;
                }

                resultDigits.AddFirst((byte)nextValue);
            }

            return new DigitInt(resultDigits.ToArray(), addBase);
        }

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
                return (Base == d.Base) && (IsNegative == d.IsNegative) && (Digits.SequenceEqual(d.Digits));
            }

            return false;
        }

        /// <summary>
        /// Gets the hashcode of the <see cref="DigitInt"/>.
        /// </summary>
        /// <returns>The hashcode of the <see cref="DigitInt"/>.</returns>
        public override int GetHashCode()
        {
            return Digits.GetHashCode();
        }
        #endregion

        /// <summary>
        /// Converts the <see cref="DigitInt"/> to its <see cref="string"/> representation.
        /// </summary>
        /// <returns>The <see cref="string"/> representation of the <see cref="DigitInt"/>.</returns>
        public override string ToString()
        {
            var str = new StringBuilder();

            if (IsNegative)
                str.Append("-");

            foreach (var digit in Digits)
            {
                str.Append(Digit.ToChar(digit));
            }

            return str.ToString();
        }
        #endregion
    }
}
