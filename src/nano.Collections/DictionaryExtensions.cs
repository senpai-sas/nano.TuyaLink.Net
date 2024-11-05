
namespace System.Collections
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Tries to get the value associated with the specified key from the dictionary.
        /// </summary>
        /// <param name="dictionary">The dictionary to search.</param>
        /// <param name="key">The key of the value to get.</param>
        /// <param name="value">When this method returns, contains the value associated with the specified key, if the key is found; otherwise, null. This parameter is passed uninitialized.</param>
        /// <returns>true if the dictionary contains an element with the specified key; otherwise, false.</returns>
        public static bool TryGetValue(this IDictionary dictionary, object key, out object value)
        {
            if (dictionary is null || key is null)
            {
                value = null;
                return false;
            }
            value = dictionary[key];
            return value is not null;
        }

        public static bool TryGetValue(this Hashtable hashtable, object key, out object value)
        {
            if(hashtable is null || key is null)
            {
                value = null;
                return false;
            }

            value = hashtable[key];
            return value is not null;
        }
    }
}
