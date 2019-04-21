using System;

namespace DigitMath
{
    public class Digit : IComparable<Digit>
    {
        #region Attributes
        byte Value { get; set; }
        public byte Radix { get; set; }
        #endregion

        #region Constructors
        public Digit(byte value)
        {
            Radix = 10;

            if (value > 9)
                throw new ArgumentOutOfRangeException($"The value must be lower than {Radix}.");

            Value = value;
        }

        public Digit(byte value, byte numBase)
        {
            Radix = numBase;

            if (value >= numBase)
                throw new ArgumentOutOfRangeException($"The value must be lower than {Radix}");

            Value = value;
        }

        public Digit(int value)
        {
            Radix = 10;

            if (value < 0 || value > 9)
                throw new ArgumentOutOfRangeException($"The value must be between 0 and {Radix}.");

            Value = (byte)value;
        }

        public Digit(int value, int numBase)
        {
            if (numBase < 0 || numBase > 255)
                throw new ArgumentOutOfRangeException("The numBase must be between 0 and 255.");

            Radix = (byte)numBase;

            if (value < 0 || value >= numBase)
                throw new ArgumentOutOfRangeException($"The value must be between 0 and {Radix}");

            Value = (byte)value;
        }
        #endregion

        #region Methods
        public char ToChar()
        {
            if (Value < 10)
                return (char)(Value + 48);

            if (Value < 36)
                return (char)(Value + 55);

            return '?';
        }

        public static char ToChar(Digit digit)
        {
            if (digit == null)
                return '?';

            return digit.ToChar();
        }

        public static char ToChar(byte digit)
        {
            if (digit < 10)
                return (char)(digit + 48);

            if (digit < 36)
                return (char)(digit + 55);

            return '?';
        }

        public static implicit operator byte(Digit digit)
        {
            if (digit == null)
                return 0;

            return digit.Value;
        }

        public static implicit operator ushort(Digit digit)
        {
            if (digit == null)
                return 0;

            return digit.Value;
        }

        public static implicit operator int(Digit digit)
        {
            if (digit == null)
                return 0;

            return digit.Value;
        }

        public override string ToString()
        {
            if (Value < 36)
                return ToChar().ToString();

            return $"({Value})";
        }

        public int CompareTo(Digit other)
        {
            if (other == null)
                return 1;

            return Value.CompareTo(other.Value);
        }
        #endregion
    }
}
