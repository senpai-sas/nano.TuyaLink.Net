using System;
using System.Collections;

using nano.SmartEnum;
using TuyaLink.Model;

namespace TuyaLink.Functions.Properties
{
    /// <summary>
    /// Represents a Tuya data type with a corresponding CLR type.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="PropertyDataType"/> class.
    /// </remarks>
    /// <param name="name">The name of the data type.</param>
    /// <param name="value">The value of the data type.</param>
    /// <param name="clrType">The CLR type associated with the data type.</param>
    public class PropertyDataType(string name, string value, Type clrType) : SmartEnum(name, value)
    {
        private static readonly Hashtable _store = [];

        /// <summary>
        /// Gets the CLR type associated with the Tuya data type.
        /// </summary>
        public Type ClrType { get; private set; } = clrType;

        /// <summary>
        /// Represents a generic value type.
        /// </summary>
        public static readonly PropertyDataType Value = new("Value", "value", typeof(object));

        /// <summary>
        /// Represents a float type.
        /// </summary>
        public static readonly PropertyDataType Float = new("Float", "float", typeof(float));

        /// <summary>
        /// Represents a double type.
        /// </summary>
        public static readonly PropertyDataType Double = new("Double", "double", typeof(double));

        /// <summary>
        /// Represents a string type.
        /// </summary>
        public static readonly PropertyDataType String = new("String", "string", typeof(string));

        /// <summary>
        /// Represents a date type.
        /// </summary>
        public static readonly PropertyDataType Date = new("Date", "date", typeof(long));

        /// <summary>
        /// Represents a boolean type.
        /// </summary>
        public static readonly PropertyDataType Boolean = new("Bool", "bool", typeof(bool));

        /// <summary>
        /// Represents an enum type.
        /// </summary>
        public static readonly PropertyDataType Enum = new("Enum", "enum", typeof(SmartEnum));

        /// <summary>
        /// Represents a raw byte array type.
        /// </summary>
        public static readonly PropertyDataType Raw = new("Raw", "raw", typeof(byte[]));

        /// <summary>
        /// Represents a device fault type.
        /// </summary>
        public static readonly PropertyDataType Fault = new("Fault", "fault", typeof(string));

        public static readonly PropertyDataType ValueArray = new("ValueArray", "value_array", typeof(object[]));

        public static readonly PropertyDataType StringArray = new("StringArray", "string_array", typeof(string[]));

        public static readonly PropertyDataType BooleanArray = new("BoolArray", "bool_array", typeof(bool[]));

        public static readonly PropertyDataType RawArray = new("RawArray", "raw_array", typeof(byte[][]));

        public static readonly PropertyDataType Struct = new("Struct", "struct", typeof(Hashtable));

        public virtual bool ValidateModel(TypeSpecifications specifications, object value)
        {
            if (specifications.Type != this)
            {
                return false;
            }
            return IsValidValue(value);
        }

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
        /// Gets the <see cref="PropertyDataType"/> instance corresponding to the specified value.
        /// </summary>
        /// <param name="value">The value of the data type.</param>
        /// <returns>The <see cref="PropertyDataType"/> instance.</returns>
        public static PropertyDataType FromValue(string value)
        {
            return (PropertyDataType)GetFromValue(value, typeof(PropertyDataType), _store);
        }

        //private class ValueDataType : TuyaDataType
        //{
        //    public ValueDataType() : base("Value", "value", typeof(object)) { }

        //    public override bool ValidateModel(TypeSpecifications specifications, object value)
        //    {

        //        if (!base.ValidateModel(specifications, value))
        //        {
        //            return false;
        //        }

        //        double doubleValue = (double)value;

        //        specifications.
        //    }
        //}
    }
}
