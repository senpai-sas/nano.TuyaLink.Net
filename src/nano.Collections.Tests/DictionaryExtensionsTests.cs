
using System.Collections;

using nanoFramework.TestFramework;

namespace nano.Collections.Tests
{
    internal class DictionaryExtensionsTests
    {
        [TestMethod]
        public void TryGetValue_KeyExists_ReturnsTrue()
        {
            // Arrange
            Hashtable dictionary = new ()
            {
                { "key1", "value1" }
            };
            object value;

            // Act
            bool result = DictionaryExtensions.TryGetValue(dictionary, "key1", out value);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual("value1", value);
        }

        [TestMethod]
        public void TryGetValue_KeyDoesNotExist_ReturnsFalse()
        {
            // Arrange
            Hashtable dictionary = new()
            {
                { "key1", "value1" }
            };
            object value;

            // Act
            bool result = DictionaryExtensions.TryGetValue(dictionary, "key2", out value);

            // Assert
            Assert.IsFalse(result);
            Assert.IsNull(value);
        }

        [TestMethod]
        public void TryGetValue_NullDictionary_ReturnsFalse()
        {
            // Arrange
            IDictionary dictionary = null;
            object value;

            // Act
            bool result = DictionaryExtensions.TryGetValue(dictionary, "key1", out value);

            // Assert
            Assert.IsFalse(result);
            Assert.IsNull(value);
        }

        [TestMethod]
        public void TryGetValue_NullKey_ReturnsFalse()
        {
            // Arrange
            Hashtable dictionary = new()
            {
                { "key1", "value1" }
            };
            object value;

            // Act
            bool result = DictionaryExtensions.TryGetValue(dictionary, null, out value);

            // Assert
            Assert.IsFalse(result);
            Assert.IsNull(value);
        }
    }
}
