using System;
using System.Collections;
using System.Reflection;

using TuyaLink.Json;

using nanoFramework.Json.Configuration;

using nanoFramework.Json.Converters;

namespace TuyaLink.Json
{
    internal class JsonSerializer
    {
        private static MethodInfo? _getConverterMethod;

        static MethodInfo GetConverterMethod => _getConverterMethod ??= typeof(ConvertersMapping).GetMethod("GetConverter", BindingFlags.Static | BindingFlags.NonPublic);
        private JsonSerializer()
        {
        }

        //
        // Summary:
        //     Convert an object to a JSON string.
        //
        // Parameters:
        //   o:
        //     The value to convert. Supported types are: System.Boolean, System.String, System.Byte,
        //     System.UInt16, System.Int16, System.UInt32, System.Int32, System.Single, System.Double,
        //     System.Array, System.Collections.IDictionary, System.Collections.IEnumerable,
        //     System.Guid, System.DateTime, System.TimeSpan, System.Collections.DictionaryEntry,
        //     System.Object and null.
        //
        //   topObject:
        //     Is the object top in hierarchy. Default true.
        //
        // Returns:
        //     The JSON object as a string or null when the value type is not supported.
        //
        // Remarks:
        //     For objects, only internal properties with getters are converted.
        public static string SerializeObject(object o, bool topObject = true, IJsonNamingConvention? namingConvention = null)
        {
            if (o == null)
            {
                return "null";
            }

            namingConvention ??= JsonNamingConventions.Default;

            Type type = o.GetType();
            if (topObject && !type.IsArray && TypeUtils.IsValueType(type))
            {
                return "[" + SerializeObject(o, topObject: false, namingConvention) + "]";
            }

            IConverter converter = (IConverter)GetConverterMethod.Invoke(null, [type]);
            if (converter != null)
            {
                return converter.ToJson(o);
            }

            if (type.IsEnum)
            {
                return o.ToString();
            }

            if (o is IDictionary dictionary && !type.IsArray)
            {
                return SerializeIDictionary(dictionary, namingConvention);
            }

            if (o is IEnumerable enumerable)
            {
                return SerializeIEnumerable(enumerable, namingConvention);
            }

            if (type.IsClass)
            {
                return SerializeClass(o, type, namingConvention);
            }

            return null;
        }

        private static string SerializeClass(object o, Type type, IJsonNamingConvention namingConvention)
        {
            Hashtable hashtable = [];
            MethodInfo[] methods = type.GetMethods();
            MethodInfo[] array = methods;
            foreach (MethodInfo methodInfo in array)
            {
                if (ShouldSerializeMethod(methodInfo))
                {
                    object value = methodInfo.Invoke(o, null);
                    hashtable.Add(methodInfo.Name.Substring(4), value);
                }
            }

            return SerializeIDictionary(hashtable, namingConvention);
        }

        private static bool ShouldSerializeMethod(MethodInfo method)
        {
            if (!method.Name.StartsWith("get_"))
            {
                return false;
            }

            if (method.IsAbstract)
            {
                return false;
            }

            if (method.IsStatic)
            {
                return false;
            }

            if (method.ReturnType == typeof(Delegate) || method.ReturnType == typeof(MulticastDelegate) || method.ReturnType == typeof(MethodInfo))
            {
                return false;
            }

            if (method.DeclaringType == typeof(Delegate) || method.DeclaringType == typeof(MulticastDelegate))
            {
                return false;
            }

            return true;
        }

        //
        // Summary:
        //     Convert an IEnumerable to a JSON string.
        //
        // Parameters:
        //   enumerable:
        //     The value to convert.
        //
        // Returns:
        //     The JSON object as a string or null when the value type is not supported.
        internal static string SerializeIEnumerable(IEnumerable enumerable, IJsonNamingConvention namingConvention)
        {
            string text = "[";
            foreach (object item in enumerable)
            {
                if (text.Length > 1)
                {
                    text += ",";
                }

                text += SerializeObject(item, topObject: false, namingConvention);
            }

            return text + "]";
        }

        //
        // Summary:
        //     Convert an IDictionary to a JSON string.
        //
        // Parameters:
        //   dictionary:
        //     The value to convert.
        //
        // Returns:
        //     The JSON object as a string or null when the value type is not supported.
        internal static string SerializeIDictionary(IDictionary dictionary, IJsonNamingConvention namingConvention)
        {
            string text = "{";
            foreach (DictionaryEntry item in dictionary)
            {
                if (text.Length > 1)
                {
                    text += ",";
                }

                text += $"\"{namingConvention.SerializeName(item.Key)}\":";
                text += SerializeObject(item.Value, topObject: false, namingConvention);
            }

            return text + "}";
        }
    }
}
