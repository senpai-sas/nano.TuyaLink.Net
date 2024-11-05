using nanoFramework.Benchmark;
using nanoFramework.Benchmark.Attributes;
using nanoFramework.Json;
using nanoFramework.Json.Resolvers;

using TuyaLink.Json;

namespace TuyaLink.Net.Benchmarks.Json
{
    [IterationCount(300)]
    public class NameConventionResolverBenchmarks
    {
       
        private JsonSerializerOptions _jsonOptions;
        private JsonSerializerOptions _notThrowJesonOptions;
        private CacheNameConventionResolver _cacheResolver;
        private NameConventionResolver _resolver;

        [Setup]
        public void Setup()
        {
            _jsonOptions = new JsonSerializerOptions();
            _notThrowJesonOptions = new JsonSerializerOptions { ThrowExceptionWhenPropertyNotFound = false };
            _cacheResolver = new CacheNameConventionResolver(JsonNamingConventions.CamelCase);
            _resolver = new NameConventionResolver(JsonNamingConventions.CamelCase);
        }

        [Benchmark]
        [Baseline]
        public void NameConventionResolver_Get_ShouldResolveProperty()
        {
            MemberSet memberSet = _resolver.Get("TestProperty", typeof(JsonTestClass), _jsonOptions);
        }

        [Benchmark]
        public void CacheNameConventionResolver_Get_ShouldResolveProperty()
        {
            MemberSet memberSet = _cacheResolver.Get("TestProperty", typeof(JsonTestClass), _jsonOptions);
        }

        [Benchmark]
        public void NameConventionResolver_Get_PropertyNotFound()
        {
            try
            {
                MemberSet memberSet = _cacheResolver.Get("NotDefinedProperty", typeof(JsonTestClass), _jsonOptions);
            }
            catch (System.Exception ex)
            {
                
            }
        }

        [Benchmark]
        public void CacheNameConventionResolver_Get_PropertyNotFound()
        {
            try
            {
                MemberSet memberSet = _cacheResolver.Get("NotDefinedProperty", typeof(JsonTestClass), _jsonOptions);
            }
            catch (System.Exception ex)
            {

            }
        }
    }
}
