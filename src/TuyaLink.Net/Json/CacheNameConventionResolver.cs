using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;

using nanoFramework.Json;
using nanoFramework.Json.Resolvers;

namespace TuyaLink.Json
{
    public class CacheNameConventionResolver : IMemberResolver
    {
        private readonly IJsonNamingConvention _jsonNamingConvention;

        private readonly Hashtable _typeCache;

        private static readonly MemberSet _skipMemberSet = new(true);

        public CacheNameConventionResolver(IJsonNamingConvention? jsonNamingConvention = null, int typeCount = 10)
        {
            _jsonNamingConvention = jsonNamingConvention ?? JsonNamingConventions.Default;
            _typeCache = new(typeCount);
            GC.SuppressFinalize(_typeCache);
        }

        private class CacheItem
        {
            public MemberSet MemberSet { get; }

            public bool Found { get; }

            public CacheItem(MemberSet memberSet)
            {
                MemberSet = memberSet;
                Found = true;
            }

            public CacheItem(DeserializationException deserializationException)
            {
                DeserializationException = deserializationException;
            }

            public DeserializationException DeserializationException { get; }
        }

        public MemberSet Get(string memberName, Type objectType, JsonSerializerOptions options)
        {
            // Create cache key with the original member name and type
            // before apply naming convention to improve performance
            object cacheKey = memberName;

            //var fields = objectType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            if (TryGetMemberCache(objectType, out Hashtable cache) && cache.TryGetValue(cacheKey, out var value))
            {
                CacheItem item = (CacheItem)value;
                if (item.Found)
                {
                    return item.MemberSet;
                }

                throw item.DeserializationException;
            }

            memberName = _jsonNamingConvention.DeserializeName(memberName);

            MethodInfo? memberPropGetMethod = objectType.GetMethod(
                "get_" + memberName
            );

            if (memberPropGetMethod is not null)
            {
                string setMemberName = "set_" + memberName;
                MethodInfo? memberPropSetMethod = objectType.GetMethod(
                    setMemberName,
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance
                );

                if (memberPropSetMethod is null)
                {
                    return HandleNullPropertyMember(cacheKey, memberName, objectType, options, "set", cache);
                }
                var memberSet = new MemberSet((instance, value) => memberPropSetMethod.Invoke(instance, [value]), memberPropGetMethod.ReturnType);
                cache.Add(cacheKey, new CacheItem(memberSet));
                return memberSet;
            }

            FieldInfo memberFieldInfo = objectType.GetField(memberName);
            // Value will be set via field
            if (memberFieldInfo is not null)
            {
                var memberSet = new MemberSet((instance, value) => memberFieldInfo.SetValue(instance, value), memberFieldInfo.FieldType);
                cache.Add(cacheKey, new CacheItem(memberSet));
                return memberSet;
            }
            return HandleNullPropertyMember(cacheKey, memberName, objectType, options, "set", cache);

        }

        private static MemberSet HandleNullPropertyMember(object cacheKey, string memberName, Type objectType, JsonSerializerOptions options, string access, Hashtable cache)
        {
            if (options.ThrowExceptionWhenPropertyNotFound)
            {
                DeserializationException exception = new($"Member {memberName} of type {objectType} has not a valid property {access}");
                cache.Add(cacheKey, new CacheItem(exception));
                throw exception;
            }
            return _skipMemberSet;
        }

        private bool TryGetMemberCache(Type objectType, out Hashtable memberCache)
        {
            if (_typeCache.TryGetValue(objectType, out object? value))
            {
                memberCache = (Hashtable)value;
                return true;
            }
            memberCache = new Hashtable(40);
            _typeCache.Add(objectType, memberCache);
            return false;
        }
    }
}
