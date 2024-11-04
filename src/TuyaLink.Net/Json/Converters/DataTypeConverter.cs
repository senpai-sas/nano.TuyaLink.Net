
using System;

using nanoFramework.Json.Converters;

namespace TuyaLink.Json.Converters
{
    internal class DataTypeConverter : IConverter
    {
        public string ToJson(object value)
        {
            if (value is TuyaDataType dataType)
            {
                return $"\"{dataType.EnumValue}\"";
            }
            throw new InvalidOperationException($"Object is not a {typeof(TuyaDataType).FullName}");
        }

        public object? ToType(object value)
        {
            if (value is null)
            {
                return null;
            }
            return TuyaDataType.FromValue(value.ToString());
        }
    }
}
