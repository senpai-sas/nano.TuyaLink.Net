﻿using System.Collections;

using nanoFramework.Json;

using TuyaLink.Functions.Events;

namespace TuyaLink.Json.Converters
{
    internal class BatchEventDataHashtableConverter : GenericHashtableConverter
    {
        public BatchEventDataHashtableConverter(IJsonNamingConvention namingConvention) : base(namingConvention, typeof(BatchEventDataHashtable))
        {
        }

        protected override Hashtable CreateGenericHashtable(int count)
        {
            return new BatchEventDataHashtable(count);
        }

        protected override object CreateValue(DictionaryEntry member)
        {
            JsonProperty jsonProperty = (JsonProperty)member.Value;
            JsonObject propertyValue = (JsonObject)jsonProperty.Value;
            Hashtable? outputParams = null;

            JsonProperty outputParamsJson = propertyValue.Get("outputParams");
            if (outputParamsJson != null && outputParamsJson.Value is JsonObject outputParamsValue)
            {
                Hashtable members = outputParamsValue.GetMembers();
                outputParams = new(members.Count);
                foreach (DictionaryEntry outputParamEntry in members)
                {
                    outputParams.Add(outputParamEntry.Key,((JsonValue)((JsonProperty)outputParamEntry.Value).Value).Value);
                }
            }
            return new EventData()
            {
                OutputParams = outputParams ?? [],
                EventTime = (long)((JsonValue)propertyValue.Get("eventTime").Value).Value,
            };
        }
    }
}
