﻿

using System.Text;

namespace System
{
    /// <summary>
    /// Provides extension methods for string manipulation.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Converts the first letter of the input string to lowercase.
        /// </summary>
        /// <param name="inputString">The input string.</param>
        /// <returns>The input string with the first letter converted to lowercase.</returns>
        public static string ToCamelCase(this string inputString)
        {
            if (string.IsNullOrEmpty(inputString))
                return inputString;

            var array = inputString.ToCharArray();

            char firstChar = array[0];

            char firstLowerChar = firstChar.ToLower();

            if (firstLowerChar == firstChar)
            {
                return inputString;
            }

            array[0] = firstLowerChar;

            return new string(array);

        }

        /// <summary>
        /// Converts the first letter of the input string to uppercase.
        /// </summary>
        /// <param name="inputString">The input string.</param>
        /// <returns>The input string with the first letter converted to uppercase.</returns>
        public static string ToTitleCase(this string inputString)
        {
            if (string.IsNullOrEmpty(inputString))
                return inputString;

            var array = inputString.ToCharArray();

            char firstChar = array[0];

            char firstUpperChar = firstChar.ToUpper();

            if (firstUpperChar == firstChar)
            {
                return inputString;
            }
            array[0] = firstUpperChar;

            return new string(array);
        }

        /// <summary>
        /// Replaces all occurrences of a specified string in the input string with another specified string.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <param name="oldString">The string to be replaced.</param>
        /// <param name="newString">The string to replace all occurrences of <paramref name="oldString"/>.</param>
        /// <returns>A new string with all occurrences of <paramref name="oldString"/> replaced by <paramref name="newString"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="input"/>, <paramref name="oldString"/>, or <paramref name="newString"/> is <c>null</c>.
        /// </exception>
        public static string Replace(this string input, string oldString, string newString)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));
            if (oldString == null)
                throw new ArgumentNullException(nameof(oldString));
            if (newString == null)
                throw new ArgumentNullException(nameof(newString));

            if (oldString == string.Empty)
                return input; // No replacement needed for empty oldString

            var result = new StringBuilder();
            int oldStringLength = oldString.Length;
            int inputLength = input.Length;
            int i = 0;

            while (i < inputLength)
            {
                int index = input.IndexOf(oldString, i);
                if (index < 0)
                {
                    result.Append(input, i, inputLength - i);
                    break;
                }

                result.Append(input, i, index - i);
                result.Append(newString);
                i = index + oldStringLength;
            }

            return result.ToString();
        }

        public static bool IsLower(this char value)
        {
            return 'a' <= value && value <= 'z';
        }

        public static bool IsUpper(this char value)
        {
            return 'A' <= value && value <= 'Z';
        }
    }
}
