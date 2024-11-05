using System;
using System.Collections;

using TuyaLink.Communication.Properties;
using TuyaLink.Functions.Events;

namespace TuyaLink.Communication.History
{
    internal class HistoryReportRequest : FunctionRequest
    {
        internal HistoryReportRequest()
        {

        }

        public HistoryReportRequest(HistoryReportData data)
        {
            Data = data;
        }
      
        public HistoryReportData Data { get; internal set; }
    }

    public class HistoryReportData
    {

        public PropertyHashtable[] Properties { get; internal set; }
        public EventDataHashtable[] Events { get; internal set; }

        public HistoryReportData()
        {
            
        }

        public HistoryReportData(PropertyHashtable[] properties, EventDataHashtable[] events)
        {
            Properties = properties;
            Events = events;
        }
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
