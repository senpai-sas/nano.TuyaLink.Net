using System;
using System.Collections;


namespace TuyaLink.Json
{
    public class JsonNamingConventions
    {
        private JsonNamingConventions()
        {
        }

        private abstract class JsonNamingConvention : IJsonNamingConvention
        {
            public Hashtable Replacements { get; } = [];

            //protected bool TryRepalce(string key, out string replacedKey)
            //{
            //    if (Replacements.TryGetValue(key, out object value) && value is string replace)
            //    {
            //        replacedKey = replace;
            //        return true;
            //    }
            //    replacedKey = null;
            //    return false;
            //}

            public abstract string DeserializeName(string key);
            public abstract string SerializeName(object key);
        }

        private class DefaultNamingConvention : JsonNamingConvention, IJsonNamingConvention
        {
            public override string DeserializeName(string key)
            {
                return /*TryRepalce(key, out string replacedKey) ? replacedKey : */key;
            }

            public override string SerializeName(object key) =>/* TryRepalce(key?.ToString(), out string replacedKey) ? replacedKey :*/ key?.ToString();
        }

        private class CamelCaseNamingConvention : JsonNamingConvention, IJsonNamingConvention
        {
            public override string DeserializeName(string key)
            {
                return /*TryRepalce(key, out string replacedKey) ? replacedKey :*/ key.ConvertFirstLetterToUppercase();
            }

            public override string SerializeName(object key)
            {
                //if (TryRepalce(key.ToString(), out string replacedKey))
                //{
                //    return replacedKey;
                //}

                if (key is string s)
                {
                    return s.ConvertFirstLetterToLowercase();
                }

                return key?.ToString()?.ConvertFirstLetterToLowercase();
            }
        }



        public static readonly IJsonNamingConvention Default = new DefaultNamingConvention();

        public static readonly IJsonNamingConvention CamelCase = new CamelCaseNamingConvention();
    }
}
