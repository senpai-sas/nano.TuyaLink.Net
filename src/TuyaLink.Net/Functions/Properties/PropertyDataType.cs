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
    public class PropertyDataType : SmartEnum
    {
        private static readonly Hashtable _store = new(14);
        /// <param name="name">The name of the data type.</param>
        /// <param name="value">The value of the data type.</param>
        /// <param name="rawType">The CLR type associated with the data type.</param>
        public PropertyDataType(string name, string value, Type rawType, Type? clrType = null) : base(name, value)
        {
            RawType = rawType;
            ClrType = clrType ?? rawType;
            _store[value] = this;
        }

        /// <summary>
        /// Gets the RAW type associated with the Tuya data type.
        /// </summary>
        /// <remarks>
        /// This is the type used to represent the data in the Tuya API. (JSON)
        /// </remarks>
        public Type RawType { get; private set; }

        /// <summary>
        /// Gets the CLR type associated with the Tuya data type.
        /// </summary>
        public Type ClrType { get; private set; }

        /// <summary>
        /// Represents a generic value type.
        /// </summary>
        public static readonly PropertyDataType Value = new ValueDataType();

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
        public static readonly PropertyDataType String = new StringDataType();

        /// <summary>
        /// Represents a date type.
        /// </summary>
        public static readonly PropertyDataType Date = new("Date", "date", typeof(long), typeof(DateTime));

        /// <summary>
        /// Represents a boolean type.
        /// </summary>
        public static readonly PropertyDataType Boolean = new("Bool", "bool", typeof(bool));

        /// <summary>
        /// Represents an enum type.
        /// </summary>
        public static readonly PropertyDataType Enum = new EnumDataType();

        /// <summary>
        /// Represents a raw byte array type.
        /// </summary>
        public static readonly PropertyDataType Raw = new("Raw", "raw", typeof(byte[]));

        /// <summary>
        /// Represents a device fault type.
        /// </summary>
        public static readonly PropertyDataType Fault = new FaultDataType();

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
            return IsValidCloudValue(value);
        }

        /// <summary>
        /// Determines whether the specified Cloud value is valid for the current data type.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <returns><c>true</c> if the value is valid; otherwise, <c>false</c>.</returns>
        public virtual bool IsValidCloudValue(object value)
        {
            return RawType.IsInstanceOfType(value);
        }

        /// <summary>
        /// Determines whether the specified Local value is valid for the current data type.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual bool IsValidLocalValue(object? value)
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
            object? result = GetFromValue(value, typeof(PropertyDataType), _store);
            if (result == null)
            {
                throw new NotImplementedException($"The data type {value} is not implemented");
            }
            return (PropertyDataType)result;
        }

        public virtual void CheckCouldValue(TypeSpecifications specs, object value)
        {
            if (specs.Type != this)
            {
                throw new ArgumentException($"The model type must be a {this} type.", nameof(specs));
            }

            if (!IsValidCloudValue(value))
            {
                throw new ArgumentException($"The value {value} is not a valid {this} value.", nameof(value));
            }
        }

        private class ValueDataType : PropertyDataType
        {
            public ValueDataType() : base("Value", "value", typeof(object), typeof(double))
            {
            }

            public override void CheckCouldValue(TypeSpecifications specs, object value)
            {
                base.CheckCouldValue(specs, value);
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                double cloudValue = (double)value;

                if (!specs.IsInBoundary(cloudValue))
                {
                    throw new ArgumentOutOfRangeException(nameof(value), $"The value {value} is out of range. Max {specs.Max}, Min {specs.Min}");
                }
            }
        }

        private class EnumDataType : PropertyDataType
        {
            public EnumDataType() : base("Enum", "enum", typeof(string), typeof(SmartEnum))
            {
            }

            public override void CheckCouldValue(TypeSpecifications specs, object value)
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }


                PropertySmartEnum? smartEnum = value as PropertySmartEnum;
                if (smartEnum is null)
                {
                    throw new ArgumentException($"The value {value} is not a valid {this} value.", nameof(value));
                }

                base.CheckCouldValue(specs, smartEnum.EnumValue);

                if (!specs.Label.Contains(smartEnum.EnumValue))
                {
                    throw new ArgumentException($"The value {value} is not a valid {this} value, allowed values are {specs.Label.Join(",")}.", nameof(value));
                }
            }
        }

        private class StringDataType : PropertyDataType
        {
            public StringDataType() : base("String", "string", typeof(string))
            {
            }

            public override void CheckCouldValue(TypeSpecifications specs, object value)
            {
                base.CheckCouldValue(specs, value);
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                string stringValue = (string)value;

                if (specs.Maxlen != -1 && stringValue.Length > specs.Maxlen)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), $"The value {value} is out of range. Max length {specs.Maxlen}");
                }
            }
        }

        private class FaultDataType : PropertyDataType
        {
            public FaultDataType() : base("Fault", "fault", typeof(string))
            {
            }

            public override void CheckCouldValue(TypeSpecifications specs, object value)
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                base.CheckCouldValue(specs, value);
               

                string stringValue = (string)value;

                if (!specs.Range.Contains(stringValue))
                {
                    throw new ArgumentException($"The value {value} is not a valid {this} value, allowed values are {specs.Range.Join(",")}.", nameof(value));
                }
            }
        }
    }
}
