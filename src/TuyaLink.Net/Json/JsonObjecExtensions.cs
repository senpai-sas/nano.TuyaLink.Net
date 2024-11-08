using System.Collections;
using System.Reflection;

using nanoFramework.Json;

namespace TuyaLink.Json
{
    internal static class JsonObjecExtensions
    {
        private static FieldInfo? _memberField;

        private static FieldInfo GetMemberFiled()
        {
            return _memberField ??= typeof(JsonObject).GetField("_members", BindingFlags.NonPublic | BindingFlags.Instance);
        }
        internal static Hashtable GetMembers(this JsonObject jsonObject)
        {

            return (Hashtable)GetMemberFiled().GetValue(jsonObject);
        }
    }
}
