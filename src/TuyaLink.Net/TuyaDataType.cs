using System;
using System.Collections;

using nano.SmartEnum;

namespace TuyaLink
{
    /// <summary>
    /// Represents a Tuya data type with a corresponding CLR type.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="TuyaDataType"/> class.
    /// </remarks>
    /// <param name="name">The name of the data type.</param>
    /// <param name="value">The value of the data type.</param>
    /// <param name="clrType">The CLR type associated with the data type.</param>
    public class TuyaDataType(string name, string value, Type clrType) : SmartEnum(name, value)
    {
        private static readonly Hashtable _store = [];

        /// <summary>
        /// Gets the CLR type associated with the Tuya data type.
        /// </summary>
        public Type ClrType { get; private set; } = clrType;

        /// <summary>
        /// Represents a generic value type.
        /// </summary>
        public static readonly TuyaDataType Value = new("Value", "value", typeof(object));

        /// <summary>
        /// Represents a float type.
        /// </summary>
        public static readonly TuyaDataType Float = new("Float", "float", typeof(float));

        /// <summary>
        /// Represents a double type.
        /// </summary>
        public static readonly TuyaDataType Double = new("Double", "double", typeof(double));

        /// <summary>
        /// Represents a string type.
        /// </summary>
        public static readonly TuyaDataType String = new("String", "string", typeof(string));

        /// <summary>
        /// Represents a date type.
        /// </summary>
        public static readonly TuyaDataType Date = new("Date", "date", typeof(DateTime));

        /// <summary>
        /// Represents a boolean type.
        /// </summary>
        public static readonly TuyaDataType Boolean = new("Bool", "bool", typeof(bool));

        /// <summary>
        /// Represents an enum type.
        /// </summary>
        public static readonly TuyaDataType Enum = new("Enum", "enum", typeof(SmartEnum));

        /// <summary>
        /// Represents a raw byte array type.
        /// </summary>
        public static readonly TuyaDataType Raw = new("Raw", "raw", typeof(byte[]));

        /// <summary>
        /// Represents a device fault type.
        /// </summary>
        public static readonly TuyaDataType Fault = new("Fault", "fault", typeof(DeviceFault));

        /// <summary>
        /// Determines whether the specified value is valid for the current data type.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <returns><c>true</c> if the value is valid; otherwise, <c>false</c>.</returns>
        public virtual bool IsValidValue(object value)
        {
            return ClrType.IsInstanceOfType(value);
        }

        /// <summary>
        /// Gets the <see cref="TuyaDataType"/> instance corresponding to the specified value.
        /// </summary>
        /// <param name="value">The value of the data type.</param>
        /// <returns>The <see cref="TuyaDataType"/> instance.</returns>
        public static TuyaDataType FromValue(string value)
        {
            return (TuyaDataType)GetFromValue(value, typeof(TuyaDataType), _store);
        }
    }
}
