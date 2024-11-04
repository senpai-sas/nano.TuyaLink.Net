using System;

using nanoFramework.Json.Converters;

using TuyaLink.Functions;

namespace TuyaLink.Json.Converters
{
    internal class StatusCodeConverter : IConverter
    {
        public string ToJson(object value)
        {
            if (value is null)
            {
                return "null";
            }

            if (value is StatusCode code)
            {
                return code.EnumValue.ToString();
            }
            throw new ArgumentException("Value is not a StatusCode");
        }

        public object? ToType(object value)
        {
            if (value is null)
            {
                return null;
            }

            if (value is int intValue)
            {
                return StatusCode.FromValue(intValue);
            }

            if (value is string stringValue && int.TryParse(stringValue, out intValue))
            {
                StatusCode? status = StatusCode.FromValue(intValue);
                if (status is not null)
                {
                    return status;
                }
            }

            throw new ArgumentException($"Value {value} is not a valid StatusCode");
        }
    }
}
