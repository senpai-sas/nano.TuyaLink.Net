

using System.Collections;
using System.Text;

using nanoFramework.Json;

using TuyaLink.Communication.History;
using TuyaLink.Communication.Properties;

namespace TuyaLink.Json.Converters
{
    internal class PropertyHashtableConverter : GenericHashtableConverter
    {
        private readonly IJsonNamingConvention _namingConvention;

        public PropertyHashtableConverter(IJsonNamingConvention namingConvention)
            : base(namingConvention, typeof(PropertyHashtable))
        {
            _namingConvention = namingConvention;
        }
        protected override Hashtable CreateGenericHashtable(int count)
        {
            return new PropertyHashtable(count);
        }

        protected override object CreateValue(DictionaryEntry member)
        {
            JsonProperty jsonProperty = (JsonProperty)member.Value;
            JsonObject propertyValue = (JsonObject)jsonProperty.Value;
            return new PropertyValue()
            {
                Time = (long)((JsonValue)propertyValue.Get("time").Value).Value,
                Value = ((JsonValue)propertyValue.Get("value").Value).Value
            };
        }
    }
}
