using System.Collections;

namespace TuyaLink.Json
{
    public interface IJsonNamingConvention
    {
        string SerializeName(object key);

        string DeserializeName(string key);

        Hashtable Replacements { get; }
    }
}
