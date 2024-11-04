using System;

using TuyaLink.Functions;

using nanoFramework.Json.Converters;

using TuyaLink.Communication.Model;

namespace TuyaLink.Json.Converters
{
    internal class AccessModeConverter : IConverter
    {
        public string ToJson(object value)
        {
            if (value is DeviceModelDataFormat format)
            {
                return $"\"{format.EnumValue}\"";
            }
            throw new ArgumentException("The value is not a DeviceModelDataFormat");
        }

        public object ToType(object value)
        {
            return AccessMode.FromValue(value.ToString());
        }
    }
}
