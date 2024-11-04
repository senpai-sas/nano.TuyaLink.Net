namespace System.Collections
{
    //
    // Summary:
    //     Initializes a new instance of the System.Collections.GenericHashtable class.
    //
    // Remarks:
    //     The implementation for .NET nanoFramework, unlike the full .NET, doesn't support
    //     collisions so every key has to be truly unique through it's System.Object.GetHashCode.
    [Serializable]
    public abstract class GenericHashtable : Hashtable, IDictionary
    {
        protected GenericHashtable()
        {
        }

        protected GenericHashtable(int capacity) : base(capacity)
        {
        }

        protected GenericHashtable(int capacity, float loadFactor) : base(capacity, loadFactor)
        {
        }

        object IDictionary.this[object key] { get => base[key]; set => base[key] = value; }

        ICollection IDictionary.Keys => base.Keys;
        ICollection IDictionary.Values => base.Values;


        void IDictionary.Add(object key, object value)
        {
            base.Add(key, value);
        }

        bool IDictionary.Contains(object key)
        {
            return base.Contains(key);
        }

        void IDictionary.Remove(object key)
        {
            base.Remove(key);
        }
    }
}
