

using System;

using nanoFramework.Benchmark;
using nanoFramework.Benchmark.Attributes;
using nanoFramework.Json;

using TuyaLink.Json;

namespace TuyaLink.Net.Benchmarks.Json.Resolvers
{
    [IterationCount(20)]
    public class CompositeObjectBenchmarks
    {
        private string _camelCasejson;
        private string _defaultJson;
        private JsonSerializerOptions _defaultOptions;
        private JsonSerializerOptions _ignoreCaseOptions;
        private JsonSerializerOptions _cacheOptions;
        private JsonSerializerOptions _nameOptions;
        private Type _type;

        internal class CompositeObject
        {
            public CompositeObject()
            {

            }
            public JsonTestClass Test { set; get; }

            public int Test1 { get; set; }

            public string Test2 { get; set; }

            public string Test3 { get; set; }

            public SubObject1 SubObject1 { get; set; }

            public SubObject2 SubObject2 { get; set; }

            public SubObject3 SubObject3 { get; set; }

            public SubObject4 SubObject4 { get; set; }
        }

        public class SubObject1
        {
            public int Test1 { get; set; }

            public float Test2 { get; set; }

            public string Test3 { get; set; }

            public SubObject2 SubObject2 { get; set; }
        }

        public class SubObject2
        {
            public double Test1 { get; set; }

            public string Test2 { get; set; }
        }

        public class SubObject3
        {
            public double Test1 { get; set; }

            public string Test2 { get; set; }
        }

        public class SubObject4
        {
            public double Test1 { get; set; }

            public string Test2 { get; set; }
        }

        [Setup]
        public void Setup()
        {
            var dummyObject = new CompositeObject
            {
                Test = new JsonTestClass
                {
                    TestProperty1 = "Value1",
                    TestProperty2 = "Value2",
                    TestProperty3 = "Value3",
                    TestProperty4 = "Value4",
                    TestProperty5 = "Value5",
                    TestProperty6 = "Value6",
                    TestProperty7 = "Value7",
                    TestProperty8 = "Value8",
                    TestProperty9 = "Value9",
                    TestProperty10 = "Value10",
                    TestProperty11 = "Value11",
                    TestProperty12 = "Value12",
                    TestProperty13 = "Value13",
                    TestProperty14 = "Value14",
                    TestProperty15 = "Value15",
                    TestProperty16 = "Value16",
                    TestProperty17 = "Value17",
                    TestProperty18 = "Value18",
                    TestProperty19 = "Value19",
                    TestProperty20 = "Value20",
                    TestProperty21 = "Value21",
                    TestProperty22 = "Value22",
                    TestProperty23 = "Value23",
                    TestProperty24 = "Value24",
                    TestProperty25 = "Value25",
                    TestProperty26 = "Value26",
                    TestProperty27 = "Value27",
                    TestProperty28 = "Value28",
                    TestProperty29 = "Value29"
                },
                Test1 = 123,
                Test2 = "TestString",
                Test3 = "AnotherTestString",
                SubObject1 = new SubObject1
                {
                    Test1 = 456,
                    Test2 = 78.9f,
                    Test3 = "SubTestString",
                    SubObject2 = new SubObject2
                    {
                        Test1 = 123.456,
                        Test2 = "SubSubTestString"
                    }
                },
                SubObject2 = new SubObject2
                {
                    Test1 = 789.012,
                    Test2 = "AnotherSubTestString"
                },
                SubObject3 = new SubObject3
                {
                    Test1 = 789.012,
                    Test2 = "AnotherSubTestString"
                },
                SubObject4 = new SubObject4
                {
                    Test1 = 789.012,
                    Test2 = "AnotherSubTestString"
                }
            };

            _camelCasejson = JsonUtils.Serialize(dummyObject);
            _defaultJson = nanoFramework.Json.JsonSerializer.SerializeObject(dummyObject);
            _defaultOptions = new JsonSerializerOptions
            {
                ThrowExceptionWhenPropertyNotFound = false,
            };
            _ignoreCaseOptions = new JsonSerializerOptions()
            {
                ThrowExceptionWhenPropertyNotFound = false,
                PropertyNameCaseInsensitive = true,
            };
            _cacheOptions = new JsonSerializerOptions
            {
                ThrowExceptionWhenPropertyNotFound = false,
                Resolver = new CacheNameConventionResolver(JsonNamingConventions.CamelCase, 20)
            };
            _nameOptions = new JsonSerializerOptions
            {
                ThrowExceptionWhenPropertyNotFound = false,
                Resolver = new NameConventionResolver(JsonNamingConventions.CamelCase)
            };
            _type = typeof(CompositeObject);

            //warmup cache 
            CacheResolver_CamelCase();
            Console.WriteLine(_camelCasejson);
            Console.WriteLine(_defaultJson);
        }

        [Benchmark]
        [Baseline]
        public object DefaultResolver()
        {
            return JsonConvert.DeserializeObject(_defaultJson, _type, _defaultOptions);
        }

        /// <summary>
        /// Deserialize a json string with camel case naming convention, this is the baseline becasue the propose is to deserialize non conventional property names
        /// </summary>
        /// <returns></returns>
        //[Baseline]
        //[Benchmark]
        public object DefaultResolver_IgnoreCase()
        {
            return JsonConvert.DeserializeObject(_camelCasejson, _type, _ignoreCaseOptions);
        }

        [Benchmark]
        public object CacheResolver_CamelCase()
        {
            return JsonConvert.DeserializeObject(_camelCasejson, _type, _cacheOptions);
        }

        [Benchmark]
        public object NameConvetion_CamelCase()
        {
            return JsonConvert.DeserializeObject(_camelCasejson, _type, _nameOptions);
        }
    }
}
