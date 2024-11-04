using System;
using System.Collections;
using System.Reflection;

using nanoFramework.Json;
using nanoFramework.Json.Converters;

using TuyaLink.Communication.Properties;

namespace TuyaLink.Json.Converters
{
    internal class DesiredPropertiesMapConverter : IConverter
    {
        
        public string ToJson(object value)
        {
            throw new NotImplementedException();
        }

        public object? ToType(object value)
        {
            if (value is null)
            {
                return null;
            }
            var jsonObject = (JsonObject)value;

            var proertiesHastable = jsonObject.GetMembers();

            var dictionary = new DesiredPropertiesMap();

            foreach (DictionaryEntry entry in proertiesHastable)
            {
                var desiredPropertyJson = (JsonProperty)entry.Value;
                var desiredProperyJsonObject = (JsonObject)desiredPropertyJson.Value;
                var desiredProperty = new DesiredProperty()
                {
                    Version = desiredProperyJsonObject.Get("version").ToString(),
                    Value = desiredProperyJsonObject.Get("value")
                };
                dictionary.Add(entry, desiredProperty);
            }
            return dictionary;
        }
    }
}
