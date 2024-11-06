using System;

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
        private Type _type;

        [Setup]
        public void Setup()
        {
            _namingConventionResolver = new NameConventionResolver(JsonNamingConventions.CamelCase);
            _cacheNamingConventionResolver = new CacheNameConventionResolver(JsonNamingConventions.CamelCase);
            _notThrowOptions = new JsonSerializerOptions { ThrowExceptionWhenPropertyNotFound = false };
            _ignoreCaseNotThrowOptions = new JsonSerializerOptions { ThrowExceptionWhenPropertyNotFound = false, PropertyNameCaseInsensitive = true };
            _type = typeof(JsonTestClass);
            CacheNamingConventionResolver_Get_CamelCase();
        }

        [Benchmark]
        [Baseline]
        public object DefaultResolver_Get_IgnoreCase()
        {
            return _ignoreCaseNotThrowOptions.Resolver.Get("testproperty", _type, _ignoreCaseNotThrowOptions);
        }


        [Benchmark]
        public object NamingConventionResolver_Get_CamelCase()
        {
            return _namingConventionResolver.Get("testProperty", _type, _ignoreCaseNotThrowOptions);
        }

        [Benchmark]
        public object CacheNamingConventionResolver_Get_CamelCase()
        {
            return _cacheNamingConventionResolver.Get("testProperty", _type, _ignoreCaseNotThrowOptions);
        }
    }
}
