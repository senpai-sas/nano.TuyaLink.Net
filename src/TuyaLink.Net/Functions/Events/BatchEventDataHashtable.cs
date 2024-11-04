using System.Collections;

namespace TuyaLink.Functions.Events
{
    internal class BatchEventDataHashtable :GenericHashtable
    {
        public BatchEventDataHashtable() : base()
        {
        }

        public BatchEventDataHashtable(int capacity) : base(capacity)
        {
        }

        public BatchEventDataHashtable(int capacity, float loadFactor) : base(capacity, loadFactor)
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
