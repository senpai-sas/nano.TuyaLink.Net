using System;
using System.Collections;

using nanoFramework.Json;

using TuyaLink.Communication.Events;
using TuyaLink.Functions.Events;

namespace TuyaLink.Json.Converters
{
    internal class TriggerEventDataHashtableConverter : GenericHashtableConverter
    {
        public TriggerEventDataHashtableConverter(IJsonNamingConvention namingConvention) : base(namingConvention, typeof(TriggerEventDataHashtable))
        {
        }

        protected override Hashtable CreateGenericHashtable(int count)
        {
            return new TriggerEventDataHashtable(count);
        }

        protected override object CreateValue(DictionaryEntry member)
        {
            JsonProperty jsonProperty = (JsonProperty)member.Value;
            JsonObject propertyValue = (JsonObject)jsonProperty.Value;
            return new TriggerEventData()
            {
                EventTime = (long)((JsonValue)propertyValue.Get("eventTime").Value).Value,
                EventCode = ((JsonValue)propertyValue.Get("eventCode").Value).Value?.ToString()
            };
        }
    }
}
