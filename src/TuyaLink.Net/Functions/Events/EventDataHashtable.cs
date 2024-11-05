using System.Collections;

namespace TuyaLink.Functions.Events
{
    public class EventDataHashtable : GenericHashtable
    {
        public EventDataHashtable() : base()
        {
        }

        public EventDataHashtable(int capacity) : base(capacity)
        {
        }

        public EventDataHashtable(int capacity, float loadFactor) : base(capacity, loadFactor)
        {
        }

        public void Add(string name, EventData batchEventData)
        {
            base.Add(name, batchEventData);
        }

        public void Remove(string name)
        {
            base.Remove(name);
        }

        public EventData this[string name]
        {
            get => (EventData)base[name];
            set => base[name] = value;
        }

        public bool Contains(string name)
        {
            return base.Contains(name);
        }
    }
}
