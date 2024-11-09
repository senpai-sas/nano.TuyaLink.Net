using System;
using System.IO;

using nanoFramework.Json;
using nanoFramework.Json.Configuration;

using TuyaLink.Communication.History;
using TuyaLink.Communication.Model;
using TuyaLink.Communication.Properties;
using TuyaLink.Firmware;
using TuyaLink.Functions;
using TuyaLink.Functions.Events;
using TuyaLink.Functions.Properties;
using TuyaLink.Json.Converters;

namespace TuyaLink.Json
{
    internal class JsonUtils
    {
        private static bool _initialized;

        public static void Initialize()
        {
            if (_initialized)
            {
                return;
            }
            _initialized = true;
            ConvertersMapping.Add(typeof(DeviceModelDataFormat), new DeviceModelDataFormatConverter());
            ConvertersMapping.Add(typeof(AccessMode), new AccessModeConverter());
            ConvertersMapping.Add(typeof(PropertyDataType), new DataTypeConverter());
            ConvertersMapping.Add(typeof(DesiredPropertiesHashtable), new DesiredPropertiesHashtableConverter(JsonNamingConventions.Default));
            ConvertersMapping.Add(typeof(DeleteDesiredPropertiesHashtable), new DeleteDesiredPropertiesHashtableConverter(JsonNamingConventions.Default));
            ConvertersMapping.Add(typeof(DataCenter), new DataCenterConverter());
            ConvertersMapping.Add(typeof(BizType), new BizTypeConverter());
            ConvertersMapping.Add(typeof(FirmwareUdpateError), new FirmwareUpdateErrorConverter());
            ConvertersMapping.Add(typeof(UpdateChannel), new UpdateChannelConverter());
            ConvertersMapping.Add(typeof(PropertyHashtable), new PropertyHashtableConverter(JsonNamingConventions.Default));
            ConvertersMapping.Add(typeof(TriggerEventDataHashtable), new TriggerEventDataHashtableConverter(JsonNamingConventions.Default));
            ConvertersMapping.Add(typeof(EventDataHashtable), new EventDataHashtableConverter(JsonNamingConventions.Default));
            ConvertersMapping.Add(typeof(StatusCode), new StatusCodeConverter());
            ConvertersMapping.Add(typeof(TuyaDateTime), new TuyaDateTimeConverter());
        }

        public static object Deserialize(Stream stream, Type type)
        {
            return JsonConvert.DeserializeObject(stream, type, _jsonOptions);
        }

        public static object Deserialize(string json, Type type)
        {
            return JsonConvert.DeserializeObject(json, type, _jsonOptions);
        }


        public static string Serialize(object obj, bool isRootObject = true)
        {
            return JsonSerializer.SerializeObject(obj, isRootObject, namingConvention: JsonNamingConventions.CamelCase);
        }

        private static readonly JsonSerializerOptions _jsonOptions = new(JsonSerializerDefaults.General)
        {
            PropertyNameCaseInsensitive = false,
            Resolver = new NameConventionResolver(JsonNamingConventions.CamelCase),
            ThrowExceptionWhenPropertyNotFound = false,
        };
    }
}
