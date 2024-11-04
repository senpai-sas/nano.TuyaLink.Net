using nanoFramework.Json.Converters;

namespace TuyaLink.Json.Converters
{
    internal class DataCenterConverter : IConverter
    {
        public string ToJson(object value)
        {
            return $"\"{value?.ToString()}\"";
        }

        public object ToType(object value)
        {
            return DataCenter.FromValue((int)value);
        }
    }
}
