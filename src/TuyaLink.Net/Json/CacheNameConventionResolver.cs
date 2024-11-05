using System;
using System.Collections;
using System.Reflection;

using nanoFramework.Json;
using nanoFramework.Json.Resolvers;

namespace TuyaLink.Json
{
    public class CacheNameConventionResolver(IJsonNamingConvention jsonNamingConvention = null) : IMemberResolver
    {
        private readonly IJsonNamingConvention _jsonNamingConvention = jsonNamingConvention ?? JsonNamingConventions.Default;
        private readonly Hashtable _cache = [];

        private static readonly MemberSet _skipMemberSet = new(true);

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
            string cacheKey = objectType + "." + memberName;

            if (_cache.TryGetValue(cacheKey, out object? value))
            {
                CacheItem item = (CacheItem)value;
                if (item.Found)
                {
                    return item.MemberSet;
                }

                throw item.DeserializationException;
            }

            memberName = _jsonNamingConvention.DeserializeName(memberName);

            FieldInfo memberFieldInfo = objectType.GetField(memberName);

            // Value will be set via field
            if (memberFieldInfo is not null)
            {
                MemberSet member = new((instance, value) => memberFieldInfo.SetValue(instance, value), memberFieldInfo.FieldType);
                return AddToCache(cacheKey, member);
            }

            MethodInfo? memberPropGetMethod = objectType.GetMethod(
                "get_" + memberName
            );

            if (memberPropGetMethod is null)
            {
                return HandleNullPropertyMember(cacheKey, memberName, objectType, options, "get");
            }

            string setMemberName = "set_" + memberName;
            MethodInfo? memberPropSetMethod = objectType.GetMethod(
                setMemberName,
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance
            );

            if (memberPropSetMethod is null)
            {
                return HandleNullPropertyMember(cacheKey, memberName, objectType, options, "set");
            }

            return AddToCache(cacheKey, new MemberSet((instance, value) => memberPropSetMethod.Invoke(instance, [value]), memberPropGetMethod.ReturnType));
        }

        private MemberSet HandleNullPropertyMember(string cacheKey, string memberName, Type objectType, JsonSerializerOptions options, string access)
        {
            if (options.ThrowExceptionWhenPropertyNotFound)
            {
                DeserializationException exception = new($"Member {memberName} of type {objectType} has not a valid property {access}");
                _cache[cacheKey] = new CacheItem(exception);
                throw exception;
            }

            return _skipMemberSet;
        }


        private MemberSet AddToCache(string key, MemberSet memberSet)
        {
            _cache[key] = new CacheItem(memberSet);
            return memberSet;
        }
    }
}
