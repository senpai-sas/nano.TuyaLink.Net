using System;

using nanoFramework.Json.Converters;

using TuyaLink.Firmware;

namespace TuyaLink.Json.Converters
{
    internal class UpdateChannelConverter : IConverter
    {
        public string ToJson(object value)
        {
            if (value is UpdateChannel channel)
            {
                return channel.EnumValue.ToString();
            }
            throw new ArgumentException("The value is not an UpdateChannel");
        }

        public object? ToType(object value)
        {
            if (value is null)
            {
                return null;
            }
            if (value is int channel)
            {
                return UpdateChannel.FromValue(channel);
            }
            throw new ArgumentException("The value is not an UpdateChannel");
        }
    }
}
