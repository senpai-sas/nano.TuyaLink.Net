﻿using System.Collections;
using System.Text;

namespace TuyaLink.Communication.Properties
{
    internal class GetPropertiesResponse : FunctionResponse
    {
        public GetPropertiesResponseData Data { get; set; } = new GetPropertiesResponseData();
    }

    internal class GetPropertiesResponseData
    {
        public DesiredPropertiesHashtable Properties { get; set; }
    }

    public class DesiredPropertiesHashtable : GenericHashtable
    {
        public DesiredPropertiesHashtable()
        {
        }

        public DesiredPropertiesHashtable(int capacity) : base(capacity)
        {
        }

        public DesiredPropertiesHashtable(int capacity, float loadFactor) : base(capacity, loadFactor)
        {
        }

        public DesiredProperty this[string key]
        {
            get => (DesiredProperty)base[key];
            set => base[key] = value;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("{");
            foreach (DictionaryEntry entry in this)
            {
                sb.Append($"\"{entry.Key}\":{entry.Value},");
            }
            if (sb.Length > 1)
            {
                sb.Remove(sb.Length - 1, 1);
            }
            sb.Append("}");
            return sb.ToString();
        }
    }

    public class DesiredProperty
    {
        public string Version { get; set; }
        public object Value { get; set; }
    }
}
