using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitMath
{
    public static class Util
    {
        /// <summary>
        /// Adds the <paramref name="value"/> at the start of the <paramref name="array"/>.
        /// </summary>
        /// <param name="array">The <see cref="Array{Byte}"/> to add the <paramref name="value"/> to.</param>
        /// <param name="value">The <see cref="byte"/> to add to the <paramref name="array"/>.</param>
        /// <returns>The new array with the value added.</returns>
        public static byte[] AddFirst(this byte[] array, byte value)
        {
            var newArray = new byte[array.Length + 1];
            array.CopyTo(array, 1);
            newArray[0] = value;
            return newArray;
        }

        /// <summary>
        /// Adds the <paramref name="value"/> at the end of the <paramref name="array"/>.
        /// </summary>
        /// <param name="array">The <see cref="Array{Byte}"/> to add the <paramref name="value"/> to.</param>
        /// <param name="value">The <see cref="byte"/> to add to the <paramref name="array"/>.</param>
        /// <returns>The new array with the value added.</returns>
        public static byte[] AddLast(this byte[] array, byte value)
        {
            var newArray = new byte[array.Length + 1];
            array.CopyTo(array, 0);
            newArray[newArray.Length - 1] = value;
            return newArray;
        }
    }
}
