

using System;

using nanoFramework.Benchmark;
using nanoFramework.Benchmark.Attributes;
using nanoFramework.Json;

using TuyaLink.Json;

namespace TuyaLink.Net.Benchmarks.Json.Resolvers
{
    [IterationCount(1)]
    public class JsonResolversBenchmarks
    {

        JsonSerializerOptions _defaultOptions;
        JsonSerializerOptions _ingnoreCaseOptions;
        JsonSerializerOptions _cacheOptions;
        JsonSerializerOptions _nameOptions;
        private string _camelCaseTestJson;
        private string _defaultJson;
        private Type _type;

        [Setup]
        public void Setup()
        {
            _defaultOptions = new JsonSerializerOptions
            {
                ThrowExceptionWhenPropertyNotFound = false,
            };
            _ingnoreCaseOptions = new JsonSerializerOptions
            {
                ThrowExceptionWhenPropertyNotFound = false,
                PropertyNameCaseInsensitive = true,
            };
            _cacheOptions = new JsonSerializerOptions
            {
                ThrowExceptionWhenPropertyNotFound = false,
                Resolver = new CacheNameConventionResolver(JsonNamingConventions.CamelCase)
            };
            _nameOptions = new JsonSerializerOptions
            {
                ThrowExceptionWhenPropertyNotFound = false,
                Resolver = new NameConventionResolver(JsonNamingConventions.CamelCase)
            };
            var testObject = new JsonTestClass()
            {
                TestProperty1 = "DummyText1",
                TestProperty2 = "DummyText2",
                TestProperty3 = "DummyText3",
                TestProperty4 = "DummyText4",
                TestProperty5 = "DummyText5",
                TestProperty6 = "DummyText6",
                TestProperty7 = "DummyText7",
                TestProperty8 = "DummyText8",
                TestProperty9 = "DummyText9",
                TestProperty10 = "DummyText10",
                TestProperty11 = "DummyText11",
                TestProperty12 = "DummyText12",
                TestProperty13 = "DummyText13",
                TestProperty14 = "DummyText14",
                TestProperty15 = "DummyText15",
                TestProperty16 = "DummyText16",
                TestProperty17 = "DummyText17",
                TestProperty18 = "DummyText18",
                TestProperty19 = "DummyText19",
                TestProperty20 = "DummyText20",
                TestProperty21 = "DummyText21",
                TestProperty22 = "DummyText22",
                TestProperty23 = "DummyText23",
                TestProperty24 = "DummyText24",
                TestProperty25 = "DummyText25",
                TestProperty26 = "DummyText26",
                TestProperty27 = "DummyText27",
                TestProperty28 = "DummyText28",
                TestProperty29 = "DummyText29",
            };

            _camelCaseTestJson = JsonUtils.Serialize(testObject);
            _defaultJson = JsonConvert.SerializeObject(testObject);
            _type = typeof(JsonTestClass);
            CacheNamingConventionResolver_CamelCase_Deserialize();
        }

        [Baseline]
        [Benchmark]
        public object DefaultResolver_Deserialize()
        {
            return JsonConvert.DeserializeObject(_defaultJson, _type, _defaultOptions);
        }

        [Benchmark]
        public object DefaultResolver_IgnoreCase_Deserialize()
        {
            return JsonConvert.DeserializeObject(_defaultJson, _type, _ingnoreCaseOptions);
        }

        [Benchmark]
        public object NamingConventionResolver_CamelCase_Deserialize()
        {
            return JsonConvert.DeserializeObject(_camelCaseTestJson, _type, _nameOptions);
        }

        [Benchmark]
        public object CacheNamingConventionResolver_CamelCase_Deserialize()
        {
            return JsonConvert.DeserializeObject(_camelCaseTestJson, _type, _cacheOptions);
        }
    }
}
