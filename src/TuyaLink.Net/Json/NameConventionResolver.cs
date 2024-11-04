using System;
using nanoFramework.Json.Resolvers;

using nanoFramework.Json;


namespace TuyaLink.Json
{
    public class NameConventionResolver(IJsonNamingConvention? jsonNamingConvention = null) : IMemberResolver
    {
        private readonly IJsonNamingConvention _jsonNamingConvention = jsonNamingConvention ?? JsonNamingConventions.Default;

        public MemberSet Get(string memberName, Type objectType, JsonSerializerOptions options)
        {

            memberName = _jsonNamingConvention.DeserializeName(memberName);

            var fields = objectType.GetFields();
            var memberFieldInfo = objectType.GetField(memberName);

            // Value will be set via field
            if (memberFieldInfo != null)
            {
                return new MemberSet((instance, value) => memberFieldInfo.SetValue(instance, value), memberFieldInfo.FieldType);
            }

            var memberPropGetMethod = objectType.GetMethod("get_" + memberName);
            if (memberPropGetMethod is null)
            {
                return HandleNullPropertyMember(memberName, objectType, options);
            }

            var memberPropSetMethod = objectType.GetMethod("set_" + memberName);
            if (memberPropSetMethod is null)
            {
                return HandleNullPropertyMember(memberName, objectType, options);
            }

            return new MemberSet((instance, value) => memberPropSetMethod.Invoke(instance, new[] { value }), memberPropGetMethod.ReturnType);
        }

        private MemberSet HandleNullPropertyMember(string memberName, Type objectType, JsonSerializerOptions options)
        {
            return HandlePropertyNotFound(options);
        }

        private static MemberSet HandlePropertyNotFound(JsonSerializerOptions options)
        {
            if (options.ThrowExceptionWhenPropertyNotFound)
            {
                throw new DeserializationException();
            }

            return new MemberSet(true);
        }
    }
}
