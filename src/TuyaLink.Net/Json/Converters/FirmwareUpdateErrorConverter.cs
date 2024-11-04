using System;

using nanoFramework.Json.Converters;

using TuyaLink.Firmware;

namespace TuyaLink.Json.Converters
{
    internal class FirmwareUpdateErrorConverter : IConverter
    {
        public string ToJson(object value)
        {
            if (value is FirmwareUdpateError error)
            {
                return error.EnumValue.ToString();
            }
            throw new ArgumentException("The value is not a FirmwareUpdateError");
        }

        public object? ToType(object value)
        {
            if (value is null)
            {
                return null;
            }
            if (value is int error)
            {
                return FirmwareUdpateError.FromValue(error);
            }
            throw new ArgumentException("The value is not a FirmwareUpdateError");
        }
    }
}
