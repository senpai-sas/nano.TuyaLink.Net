using System;

using nanoFramework.Json.Converters;

using TuyaLink.Firmware;

namespace TuyaLink.Json.Converters
{
    internal class BizTypeConverter : IConverter
    {
        public string ToJson(object value)
        {
            if (value is BizType bizType)
            {
                return $"\"{bizType.EnumValue}\"";
            }
            throw new ArgumentException("The value is not a BizType");
        }

        public object? ToType(object value)
        {
            if (value is null)
            {
                return null;
            }
            return BizType.FromValue(value.ToString());
        }
    }
}
