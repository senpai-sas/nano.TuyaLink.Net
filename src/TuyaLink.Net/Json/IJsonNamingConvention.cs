using System.Collections;

namespace TuyaLink.Json
{
    /// <summary>
    /// Interface for naming conventions
    /// </summary>
    public interface IJsonNamingConvention
    {
        /// <summary>
        /// Deserialize the name
        /// </summary>
        /// <param name="key">The original member name</param>
        /// <returns>The serialized name based on convention</returns>
        string SerializeName(object key);


        /// <summary>
        /// Deserialize the name
        /// </summary>
        /// <param name="key">The serialized name based on convention</param>
        /// <returns>The original member name</returns>
        string DeserializeName(string key);
    }
}
