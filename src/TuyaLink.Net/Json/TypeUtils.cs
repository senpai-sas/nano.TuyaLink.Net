using System;
using System.Collections;

namespace TuyaLink.Json
{
    internal static class TypeUtils
    {
        // A very small optimization occurs by caching these types
        public static readonly Type ValueTypeType = typeof(ValueType);

        public static bool IsValueType(Type type) => ValueTypeType == type?.BaseType;
    }
}
