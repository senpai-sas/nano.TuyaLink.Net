using System.Collections;


using TuyaLink.Communication.Events;

namespace TuyaLink.Functions.Events
{
    internal class TriggerEventDataHashtable : GenericHashtable
    {
        public TriggerEventDataHashtable() : base()
        {
        }

        public TriggerEventDataHashtable(int capacity) : base(capacity)
        {
        }

        public TriggerEventDataHashtable(int capacity, float loadFactor) : base(capacity, loadFactor)
        {
        }

        public void Add(string name, TriggerEventData triggerEventData)
        {
            base.Add(name, triggerEventData);
        }

        public void Remove(string name)
        {
            base.Remove(name);
        }

        public TriggerEventData this[string name]
        {
            get => (TriggerEventData)base[name];
            set => base[name] = value;
        }

        public bool Contains(string name)
        {
            return base.Contains(name);
        }
    }
}
