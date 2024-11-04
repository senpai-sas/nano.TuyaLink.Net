using System;
using System.Collections;
using System.Text;

using nanoFramework.Json;
using nanoFramework.Json.Converters;

namespace TuyaLink.Json.Converters
{
    internal abstract class GenericHashtableConverter : IConverter
    {
        private readonly IJsonNamingConvention _namingConvention;

        protected GenericHashtableConverter(IJsonNamingConvention namingConvention, Type type)
        {
            _namingConvention = namingConvention;
            Type = type;
        }

        public Type Type { get; }

        public virtual string ToJson(object value)
        {
            if (value is null)
            {
                return "null";
            }

            if (value is Hashtable propertyHashtable)
            {
                StringBuilder builder = new();
                builder.Append("{");
                int index = 0;
                foreach (DictionaryEntry entry in propertyHashtable)
                {
                    string item = JsonUtils.Serialize(entry.Value, false);

                    builder.Append($"\"{_namingConvention.SerializeName(entry.Key)}\":{item}");
                    index++;
                    if (index < propertyHashtable.Count)
                    {
                        builder.Append(",");
                    }
                }
                builder.Append("}");
                return builder.ToString();
            }
            throw new System.ArgumentException($"The value is not a {Type.FullName}");
        }
        public object ToType(object value)
        {
            if (value is null)
            {
                return null;
            }
            JsonObject jsonObject = (JsonObject)value;

            Hashtable members = jsonObject.GetMembers();
            Hashtable hashtable = CreateGenericHashtable(members.Count);
            foreach (DictionaryEntry member in members)
            {
                object propety = CreateValue(member);
                hashtable.Add(_namingConvention.DeserializeName((string)member.Key), propety);
            }
            return hashtable;
        }

        protected abstract object CreateValue(DictionaryEntry member);
        protected abstract Hashtable CreateGenericHashtable(int count);
    }
}
