﻿
using AutomaticPetFeeder.Converters;

namespace System
{
    /// <summary>
    /// This class augments <see cref="BitConverter"/> to support endianness in converting data values
    /// to byte arrays and vice versa, regardless the endianness of the system.
    /// <para>
    /// The <see cref="LittleEndian"/> returns an instance that handles bytes in arrays in little-endian order; 
    /// while the <see cref="BigEndian"/> returns an instance that handles bytes in arrays in big-endian order.
    /// </para> 
    /// </summary>
    /// <remarks>
    /// Note, <see cref="BitConverter"/> assumes that the byte arrays given to it are always in the system's 
    /// native byte order. 
    /// </remarks>
    public abstract partial class EndianBitConverter
    {
        static EndianBitConverter _littleEndian;
        private static BigEndianBitConverter _bigEndian;

        /// <summary>
        /// Gets an <see cref="EndianBitConverter"/> to support little-endian byte order.
        /// </summary>
        public static EndianBitConverter LittleEndian => _littleEndian ??= new LittleEndianBitConverter();

        /// <summary>
        /// Gets an <see cref="EndianBitConverter"/> to support big-endian byte order.
        /// </summary>
        public static EndianBitConverter BigEndian => _bigEndian ??= new BigEndianBitConverter();

        /// <summary>
        /// Returns a byte array of the specified boolean value.
        /// </summary>
        /// <param name="value">The boolean value to convert.</param>
        /// <returns>A byte array converted from the boolean value.</returns>
        public abstract byte[] GetBytes(bool value);

        /// <summary>
        /// Returns a byte array of the specified Unicode character.
        /// </summary>
        /// <param name="value">The character to convert.</param>
        /// <returns>A byte array converted from the character value.</returns>
        public abstract byte[] GetBytes(char value);

        /// <summary>
        /// Returns a byte array of the specified 16-bit integer value.
        /// </summary>
        /// <param name="value">The number to convert.</param>
        /// <returns>A byte array converted from the number value.</returns>
        public abstract byte[] GetBytes(short value);

        /// <summary>
        /// Returns a byte array of the specified unsigned 16-bit integer value.
        /// </summary>
        /// <param name="value">The number to convert.</param>
        /// <returns>A byte array converted from the number value.</returns>
        public abstract byte[] GetBytes(ushort value);

        /// <summary>
        /// Returns a byte array of the specified 32-bit integer value.
        /// </summary>
        /// <param name="value">The number to convert.</param>
        /// <returns>A byte array converted from the number value.</returns>
        public abstract byte[] GetBytes(int value);

        /// <summary>
        /// Returns a byte array of the specified unsigned 32-bit integer value.
        /// </summary>
        /// <param name="value">The number to convert.</param>
        /// <returns>A byte array converted from the number value.</returns>
        public abstract byte[] GetBytes(uint value);

        /// <summary>
        /// Returns a byte array of the specified 64-bit integer value.
        /// </summary>
        /// <param name="value">The number to convert.</param>
        /// <returns>A byte array converted from the number value.</returns>
        public abstract byte[] GetBytes(long value);

        /// <summary>
        /// Returns a byte array of the specified unsigned 64-bit integer value.
        /// </summary>
        /// <param name="value">The number to convert.</param>
        /// <returns>A byte array converted from the number value.</returns>
        public abstract byte[] GetBytes(ulong value);

        /// <summary>
        /// Returns a byte array of the specified float value.
        /// </summary>
        /// <param name="value">The number to convert.</param>
        /// <returns>A byte array converted from the number value.</returns>
        public abstract byte[] GetBytes(float value);

        /// <summary>
        /// Returns a byte array of the specified double value.
        /// </summary>
        /// <param name="value">The number to convert.</param>
        /// <returns>A byte array converted from the number value.</returns>
        public abstract byte[] GetBytes(double value);

        /// <summary>
        /// Returns a byte array of the specified string, using UTF8 enconding.
        /// </summary>
        /// <remarks>
        /// Note: UFT8 enconding has no endianness, i.e., the byte order is always the same 
        /// regardless the architecture's endianness.
        /// </remarks>
        /// <param name="value">The string to convert.</param>
        /// <returns>A byte array converted from the string.</returns>
        public abstract byte[] GetUTF8Bytes(string value);

        /// <summary>
        /// Converts to a boolean value from the byte array starting at specified position.
        /// </summary>
        /// <param name="bytes">A byte array.</param>
        /// <param name="startIndex">The starting position within the byte array to convert.</param>
        /// <returns>The boolean value converted from the byte array.</returns>
        public abstract bool ToBoolean(byte[] bytes, int startIndex = 0);

        /// <summary>
        /// Converts to a character value from the byte array starting at specified position.
        /// </summary>
        /// <param name="bytes">A byte array.</param>
        /// <param name="startIndex">The starting position within the byte array to convert.</param>
        /// <returns>The character value converted from the byte array.</returns>
        public abstract char ToChar(byte[] bytes, int startIndex = 0);

        /// <summary>
        /// Converts to a 16-bit integter value from the byte array starting at specified position.
        /// </summary>
        /// <param name="bytes">A byte array.</param>
        /// <param name="startIndex">The starting position within the byte array to convert.</param>
        /// <returns>The number value converted from the byte array.</returns>
        public abstract short ToInt16(byte[] bytes, int startIndex = 0);

        /// <summary>
        /// Converts to an unsigned 16-bit integter value from the byte array starting at specified position.
        /// </summary>
        /// <param name="bytes">A byte array.</param>
        /// <param name="startIndex">The starting position within the byte array to convert.</param>
        /// <returns>The number value converted from the byte array.</returns>
        public abstract ushort ToUInt16(byte[] bytes, int startIndex = 0);

        /// <summary>
        /// Converts to a 32-bit integter value from the byte array starting at specified position.
        /// </summary>
        /// <param name="bytes">A byte array.</param>
        /// <param name="startIndex">The starting position within the byte array to convert.</param>
        /// <returns>The number value converted from the byte array.</returns>
        public abstract int ToInt32(byte[] bytes, int startIndex = 0);

        /// <summary>
        /// Converts to an unsigned 32-bit integter value from the byte array starting at specified position.
        /// </summary>
        /// <param name="bytes">A byte array.</param>
        /// <param name="startIndex">The starting position within the byte array to convert.</param>
        /// <returns>The number value converted from the byte array.</returns>
        public abstract uint ToUInt32(byte[] bytes, int startIndex = 0);

        /// <summary>
        /// Converts to a 64-bit integter value from the byte array starting at specified position.
        /// </summary>
        /// <param name="bytes">A byte array.</param>
        /// <param name="startIndex">The starting position within the byte array to convert.</param>
        /// <returns>The number value converted from the byte array.</returns>
        public abstract long ToInt64(byte[] bytes, int startIndex = 0);

        /// <summary>
        /// Converts to an unsigned 64-bit integter value from the byte array starting at specified position.
        /// </summary>
        /// <param name="bytes">A byte array.</param>
        /// <param name="startIndex">The starting position within the byte array to convert.</param>
        /// <returns>The number value converted from the byte array.</returns>
        public abstract ulong ToUInt64(byte[] bytes, int startIndex = 0);

        /// <summary>
        /// Converts to a float value from the byte array starting at specified position.
        /// </summary>
        /// <param name="bytes">A byte array.</param>
        /// <param name="startIndex">The starting position within the byte array to convert.</param>
        /// <returns>The number value converted from the byte array.</returns>
        public abstract float ToSingle(byte[] bytes, int startIndex = 0);

        /// <summary>
        /// Converts to a double value from the byte array starting at specified position.
        /// </summary>
        /// <param name="bytes">A byte array.</param>
        /// <param name="startIndex">The starting position within the byte array to convert.</param>
        /// <returns>The number value converted from the byte array.</returns>
        public abstract double ToDouble(byte[] bytes, int startIndex = 0);


        /// <summary>
        /// Converts to a Unicode string from the byte array starting at specified position.
        /// </summary>
        /// <param name="bytes">A byte array using UTF-16 encoding.</param>
        /// <param name="startIndex">The starting position within the byte array to convert.</param>
        /// <param name="count">The number of bytes to convert.</param>
        /// <returns>The Unicode string converted from the byte array.</returns>
        public abstract string ToString(byte[] bytes, int startIndex, int count);

        protected static byte[] Reverse(byte[] bytes, int startIndex, int count)
        {
            CheckArguments(bytes, startIndex, count);

            byte[] result = new byte[count];
            Array.Copy(bytes, startIndex, result, 0, count);
            result.Reverse();
            return result;
        }

        private static void CheckArguments(byte[] bytes, int startIndex, int count)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException($"{nameof(bytes)}");
            }
            if (startIndex < 0 || startIndex >= bytes.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            }
            if (startIndex + count > bytes.Length)
            {
                throw new ArgumentException("The array does not have enough bytes to operate.");
            }
        }
    }
}
