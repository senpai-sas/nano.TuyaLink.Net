using nanoFramework.Json.Converters;

namespace TuyaLink.Json.Converters
{
    internal class TuyaDateTimeConverter : IConverter
    {
        public string ToJson(object value)
        {
            TuyaDateTime dateTime = (TuyaDateTime)value;
            return dateTime.ToUnixTimeMilliseconds().ToString();
        }

        public object ToType(object value)
        {
            if (value is long timeLong)
            {
                return TuyaDateTime.FromUnixTime(timeLong);
            }
            if (value is int timeInt)
            {
                return TuyaDateTime.FromUnixTime(timeInt);
            }
            if (value is string str)
            {
                return TuyaDateTime.FromUnixTime(long.Parse(str));
            }
            throw new TuyaLinkException($"Value {value} can't be deserialized to a TuyaDateTime value");
        }
    }
}
