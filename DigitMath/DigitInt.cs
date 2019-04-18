using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitMath
{
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
        byte[] Digits { get; set; }
        byte Base { get; }
        public bool IsNegative { get; set; }
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

        public static bool operator >(DigitInt left, DigitInt right)
        {
            if (left == null)
                return false;

            return (left.CompareTo(right) > 0);
        }
        public static bool operator <(DigitInt left, DigitInt right)
        {
            if (left == null)
                return true;

            return (left.CompareTo(right) < 0);
        }

        public static DigitInt Add(DigitInt left, DigitInt right)
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

        public static bool operator ==(DigitInt left, DigitInt right)
        {
            if (left is null || right is null)
                return false;

            return left.Equals(right);
        }

        public static bool operator !=(DigitInt left, DigitInt right)
        {
            if (left is null || right is null)
                return true;

            return !left.Equals(right);
        }

        public override bool Equals(object obj)
        {
            if (obj is DigitInt d)
            {
                return (Base == d.Base) && (IsNegative == d.IsNegative) && (Digits.SequenceEqual(d.Digits));
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Digits.GetHashCode();
        }
        #endregion
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
