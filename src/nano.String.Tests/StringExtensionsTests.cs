using System;

using nanoFramework.TestFramework;

namespace nano.String.Tests
{
    [TestClass]
    public class StringExtensionsTests
    {
        [TestMethod]
        public void ConvertFirstLetterToLowercase_ConvertsFirstLetter()
        {
            var input = "TestString";
            var expected = "testString";
            var result = input.ToCamelCase();
            Assert.AreEqual(expected, result);
            Assert.AreEqual("TestString", input);
        }

        [TestMethod]
        public void ConvertFirstLetterToLowercase_EmptyString_ReturnsEmpty()
        {
            var input = "";
            var result = input.ToCamelCase();
            Assert.AreEqual(input, result);
        }

        [TestMethod]
        public void ConvertFirstLetterToUppercase_ConvertsFirstLetter()
        {
            var input = "testString";
            var expected = "TestString";
            var result = input.ToTitleCase();
            Assert.AreEqual(expected, result);
            Assert.AreEqual("testString", input);
        }

        [TestMethod]
        public void ConvertFirstLetterToUppercase_EmptyString_ReturnsEmpty()
        {
            var input = "";
            var result = input.ToTitleCase();
            Assert.AreEqual(input, result);
        }

        [TestMethod]
        public void Replace_ReplacesOccurrences()
        {
            var input = "Hello World";
            var oldString = "World";
            var newString = "nanoFramework";
            var expected = "Hello nanoFramework";
            var result = input.Replace(oldString, newString);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Replace_ReplacesMultipleOccurrences()
        {
            var input = "Hello World World";
            var oldString = "World";
            var newString = "nanoFramework";
            var expected = "Hello nanoFramework nanoFramework";
            var result = input.Replace(oldString, newString);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Replace_OldStringNotFound_ReturnsOriginal()
        {
            var input = "Hello World";
            var oldString = "Universe";
            var newString = "nanoFramework";
            var result = input.Replace(oldString, newString);
            Assert.AreEqual(input, result);
        }
    }
}
