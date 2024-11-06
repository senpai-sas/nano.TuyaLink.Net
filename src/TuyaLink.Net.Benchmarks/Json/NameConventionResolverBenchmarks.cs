using System;

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

        private JsonSerializerOptions _notThrowJesonOptions;
        private JsonSerializerOptions _ignoreCaseOptions;
        private CacheNameConventionResolver _cacheResolver;
        private NameConventionResolver _nameConvetionResolver;
        private Type _type;
        private IMemberResolver _defaultResolver;

        [Setup]
        public void Setup()
        {

            _notThrowJesonOptions = new JsonSerializerOptions { ThrowExceptionWhenPropertyNotFound = false };
            _ignoreCaseOptions = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                ThrowExceptionWhenPropertyNotFound = false
            };
            _defaultResolver = _notThrowJesonOptions.Resolver;

            _cacheResolver = new CacheNameConventionResolver(JsonNamingConventions.CamelCase);
            _nameConvetionResolver = new NameConventionResolver(JsonNamingConventions.CamelCase);
            _type = typeof(JsonTestClass);
            //warpup cache 
            _cacheResolver.Get("testProperty", _type, _notThrowJesonOptions);
        }

        [Benchmark]
        public object DefaultResolver()
        {
            return _defaultResolver.Get("TestProperty", _type, _notThrowJesonOptions);
        }

        [Baseline]
        [Benchmark]
        public object DefaultResovler_IgnoreCase()
        {
            return _defaultResolver.Get("testProperty", _type, _ignoreCaseOptions);
        }

        [Benchmark]
        public object NameConventionResolver_CamelCase()
        {
            return _nameConvetionResolver.Get("testProperty", _type, _notThrowJesonOptions);
        }

        [Benchmark]
        public object CacheNameConventionResolver_CamelCase()
        {
            return _cacheResolver.Get("testProperty", _type, _notThrowJesonOptions);
        }

    }
}
