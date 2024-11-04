using System;
using System.Collections;

using TuyaLink.Communication.Properties;

namespace TuyaLink.Communication.History
{
    internal class HistoryReportRequest(HistoryReportData data) : FunctionRequest
    {
        public HistoryReportData Data { get; } = data;
    }

    public class HistoryReportData(Hashtable[] properties, Hashtable[] events)
    {

        public Hashtable[] Properties { get; } = properties;
        public Hashtable[] Events { get; } = events;
    }

    [Serializable]
    public class PropertyHashtable : GenericHashtable
    {
        public PropertyHashtable() : base()
        {
        }

        public PropertyHashtable(int capacity) : base(capacity)
        {
        }

        public PropertyHashtable(int capacity, float loadFactor) : base(capacity, loadFactor)
        {
        }

        public void Add(string name, PropertyValue propertyValue)
        {
            base.Add(name, propertyValue);
        }

        public void Remove(string name)
        {
            base.Remove(name);
        }

        public PropertyValue this[string name]
        {
            get => (PropertyValue)base[name];
            set => base[name] = value;
        }

        public bool Contains(string name)
        {
            return base.Contains(name);
        }
    }
}
