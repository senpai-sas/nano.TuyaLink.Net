
using System;

using nanoFramework.Json.Converters;
using TuyaLink.Functions.Properties;

namespace TuyaLink.Json.Converters
{
    internal class DataTypeConverter : IConverter
    {
        public string ToJson(object value)
        {
            if (value is PropertyDataType dataType)
            {
                return $"\"{dataType.EnumValue}\"";
            }
            throw new InvalidOperationException($"Object is not a {typeof(PropertyDataType).FullName}");
        }

        public object? ToType(object value)
        {
            if (value is null)
            {
                return null;
            }
            return PropertyDataType.FromValue(value.ToString());
        }
    }
}
