using System;

using nanoFramework.Json.Converters;

using TuyaLink.Communication.Model;

namespace TuyaLink.Json.Converters
{
    internal class DeviceModelDataFormatConverter : IConverter
    {
        public string ToJson(object value)
        {
            if (value is DeviceModelDataFormat format)
            {
                return $"\"{format.EnumValue}\"";
            }
            throw new ArgumentException("Value is not a DeviceModelDataFormat");
        }

        public object ToType(object value)
        {
            return DeviceModelDataFormat.FromValue(value);
        }
    }
}
