using System;
using System.Collections;
using System.Text;

using nanoFramework.Json;

using TuyaLink.Communication.Properties;

namespace TuyaLink.Json.Converters
{
    internal class DesiredPropertiesHashtableConverter : GenericHashtableConverter
    {
        public DesiredPropertiesHashtableConverter(IJsonNamingConvention namingConvention) : base(namingConvention, typeof(DesiredPropertiesHashtable))
        {
        }

        protected override Hashtable CreateGenericHashtable(int count)
        {
            return new DesiredPropertiesHashtable(count);
        }

        protected override object CreateValue(DictionaryEntry member)
        {
            JsonProperty jsonProperty = (JsonProperty)member.Value;
            JsonObject propertyValue = (JsonObject)jsonProperty.Value;
            return new DesiredProperty()
            {
                Version = (string)((JsonValue)propertyValue.Get("version").Value).Value,
                Value = ((JsonValue)propertyValue.Get("value").Value).Value
            };
        }
    }
}
