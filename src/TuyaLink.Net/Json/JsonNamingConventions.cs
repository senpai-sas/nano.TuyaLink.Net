using System;
using System.Collections;


namespace TuyaLink.Json
{
    /// <summary>
    /// Provides various JSON naming conventions for serialization and deserialization.
    /// </summary>
    /// <example>
    /// <code>
    /// var defaultConvention = JsonNamingConventions.Default;
    /// var camelCaseConvention = JsonNamingConventions.CamelCase;
    /// </code>
    /// </example>
    public static class JsonNamingConventions
    {
        private class DefaultNamingConvention : IJsonNamingConvention
        {
            /// <summary>
            /// Returns the key as is for deserialization.
            /// </summary>
            /// <param name="key">The key to deserialize.</param>
            /// <returns>The deserialized key.</returns>
            /// <example>
            /// <code>
            /// var deserializedKey = defaultConvention.DeserializeName("MyKey");
            /// </code>
            /// </example>
            public string DeserializeName(string key)
            {
                return key;
            }

            /// <summary>
            /// Returns the key as is for serialization.
            /// </summary>
            /// <param name="key">The key to serialize.</param>
            /// <returns>The serialized key.</returns>
            /// <example>
            /// <code>
            /// var serializedKey = defaultConvention.SerializeName("MyKey");
            /// </code>
            /// </example>
            public string SerializeName(object key) => key?.ToString();
        }

        private class CamelCaseNamingConvention : IJsonNamingConvention
        {
            /// <inheritdoc />
            /// <summary>
            /// Converts the key to title case for deserialization.
            /// </summary>
            /// <param name="key">The key to deserialize.</param>
            /// <returns>The deserialized key in title case.</returns>
            /// <example>
            /// <code>
            /// var deserializedKey = camelCaseConvention.DeserializeName("myKey");
            /// </code>
            /// </example>
            public string DeserializeName(string key)
            {
                return key.ToTitleCase();
            }

            /// <inheritdoc />
            /// <summary>
            /// Converts the key to camel case for serialization.
            /// </summary>
            /// <param name="key">The key to serialize.</param>
            /// <returns>The serialized key in camel case.</returns>
            /// <example>
            /// <code>
            /// var serializedKey = camelCaseConvention.SerializeName("MyKey");
            /// </code>
            /// </example>
            public string SerializeName(object key)
            {
                if (key is string s)
                {
                    return s.ToCamelCase();
                }

                return key?.ToString() ?? string.Empty;
            }
        }

        /// <summary>
        /// The default naming convention.
        /// </summary>
        public static readonly IJsonNamingConvention Default = new DefaultNamingConvention();

        /// <summary>
        /// The camel case naming convention.
        /// </summary>
        public static readonly IJsonNamingConvention CamelCase = new CamelCaseNamingConvention();
    }
}
