using System;
using System.Collections;

using nanoFramework.Json;
using nanoFramework.Json.Converters;

using TuyaLink.Communication.Properties;

namespace TuyaLink.Json.Converters
{
    internal class DeleteDesiredPropertiesHashtableConverter : GenericHashtableConverter
    {
        public DeleteDesiredPropertiesHashtableConverter(IJsonNamingConvention namingConvention) : base(namingConvention, typeof(DeleteDesiredPropertiesHashtable))
        {
        }

        protected override Hashtable CreateGenericHashtable(int count)
        {
            return new DeleteDesiredPropertiesHashtable(count);
        }

        protected override object CreateValue(DictionaryEntry member)
        {
            JsonProperty jsonProperty = (JsonProperty)member.Value;
            JsonObject propertyValue = (JsonObject)jsonProperty.Value;
            return new DeleteDesiredProperty()
            {
                Version = (string)((JsonValue)propertyValue.Get("version").Value).Value,
            };
        }
    }
}
