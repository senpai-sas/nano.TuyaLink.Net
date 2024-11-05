using nanoFramework.Benchmark;
using nanoFramework.Benchmark.Attributes;
using nanoFramework.Json;
using nanoFramework.Json.Resolvers;

using TuyaLink.Json;

namespace TuyaLink.Net.Benchmarks.Json.Conventions
{
    [IterationCount(300)]
    public class CamelCaseNamingConventionBenchmarks
    {
        private NameConventionResolver _namingConventionResolver;
        private CacheNameConventionResolver _cacheNamingConventionResolver;
        private JsonSerializerOptions _notThrowOptions;
        private JsonSerializerOptions _ignoreCaseNotThrowOptions;

        [Setup]
        public void Setup()
        {
            _namingConventionResolver = new NameConventionResolver(JsonNamingConventions.CamelCase);
            _cacheNamingConventionResolver = new CacheNameConventionResolver(JsonNamingConventions.CamelCase);
            _notThrowOptions = new JsonSerializerOptions { ThrowExceptionWhenPropertyNotFound = false };
            _ignoreCaseNotThrowOptions = new JsonSerializerOptions { ThrowExceptionWhenPropertyNotFound = false, PropertyNameCaseInsensitive = true };
        }

        [Benchmark]
        [Baseline]
        public void DefaultResolver_Get_IgnoreCase()
        {
            MemberSet memberSet = _ignoreCaseNotThrowOptions.Resolver.Get("testproperty", typeof(JsonTestClass), _ignoreCaseNotThrowOptions);
        }


        [Benchmark]
        public void NamingConventionResolver_Get_CamelCase()
        {
            MemberSet memberSet = _namingConventionResolver.Get("testProperty", typeof(JsonTestClass), _ignoreCaseNotThrowOptions);
        }

        [Benchmark]
        public void CacheNamingConventionResolver_Get_CamelCase()
        {
            MemberSet memberSet = _cacheNamingConventionResolver.Get("testProperty", typeof(JsonTestClass), _ignoreCaseNotThrowOptions);
        }
    }
}
