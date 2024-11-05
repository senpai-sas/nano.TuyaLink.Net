

using System.Collections;
using System.Text;

namespace TuyaLink.Communication.Properties
{
    internal class DeleteDesiredPropertyRequest : FunctionRequest
    {
        public DeleteDesiredPropertyData Data { get; internal set; } = new DeleteDesiredPropertyData();
    }

    public class DeleteDesiredPropertyData
    {
        public DeleteDesiredPropertiesHashtable Properties { get; set; } = new DeleteDesiredPropertiesHashtable();
    }

    public class DeleteDesiredPropertiesHashtable : GenericHashtable
    {
        public DeleteDesiredPropertiesHashtable()
        {
        }

        public DeleteDesiredPropertiesHashtable(int capacity) : base(capacity)
        {
        }

        public DeleteDesiredPropertiesHashtable(int capacity, float loadFactor) : base(capacity, loadFactor)
        {
        }

        public void Add(string name, DeleteDesiredProperty propertyValue)
        {
            base.Add(name, propertyValue);
        }

        public void Remove(string name)
        {
            base.Remove(name);
        }

        public DeleteDesiredProperty this[string name]
        {
            get => (DeleteDesiredProperty)base[name];
            set => base[name] = value;
        }

        public bool Contains(string name)
        {
            return base.Contains(name);
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

    public class DeleteDesiredProperty
    {
        public string Version { get; set; }
    }
}
