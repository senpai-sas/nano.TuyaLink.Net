using System;
using System.Reflection;

using nanoFramework.Json;
using nanoFramework.Json.Resolvers;


namespace TuyaLink.Json
{
    public class NameConventionResolver : IMemberResolver
    {
        private readonly IJsonNamingConvention _jsonNamingConvention;
        private static readonly MemberSet _skipMemberSet = new(true);

        public NameConventionResolver(IJsonNamingConvention jsonNamingConvention = null)
        {
            _jsonNamingConvention = jsonNamingConvention ?? JsonNamingConventions.Default;
        }

        public MemberSet Get(string memberName, Type objectType, JsonSerializerOptions options)
        {

            memberName = _jsonNamingConvention.DeserializeName(memberName);

            FieldInfo memberFieldInfo = objectType.GetField(memberName);

            // Value will be set via field
            if (memberFieldInfo != null)
            {
                return new MemberSet((instance, value) => memberFieldInfo.SetValue(instance, value), memberFieldInfo.FieldType);
            }

            MethodInfo? memberPropGetMethod = objectType.GetMethod(
                "get_" + memberName
            );
            if (memberPropGetMethod is null)
            {
                return HandleNullPropertyMember(memberName, objectType, options);
            }
            var setMemberName = "set_" + memberName;
            MethodInfo? memberPropSetMethod = objectType.GetMethod(
                setMemberName, 
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance
            );

            if (memberPropSetMethod is null)
            {
                return HandleNullPropertyMember(memberName, objectType, options);
            }

            return new MemberSet((instance, value) => memberPropSetMethod.Invoke(instance, [value]), memberPropGetMethod.ReturnType);
        }

        private MemberSet HandleNullPropertyMember(string memberName, Type objectType, JsonSerializerOptions options)
        {
            if (options.ThrowExceptionWhenPropertyNotFound)
            {
                throw new DeserializationException($"Member {memberName} of type {objectType} has not a valid property set");
            }

            return _skipMemberSet;
        }
       
    }
}
