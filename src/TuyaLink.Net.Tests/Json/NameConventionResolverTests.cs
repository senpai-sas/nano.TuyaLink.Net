using System;

using TuyaLink.Json;

using nanoFramework.TestFramework;
using nanoFramework.Json;


namespace TuyaLink.Net.Json
{
    [TestClass]
    public class NameConventionResolverTests
    {
        private class TestClass
        {
            public string TestProperty { get; set; }
            public int TestField;
        }

        [TestMethod]
        public void Get_ShouldResolveProperty()
        {
            var resolver = new NameConventionResolver();
            var options = new JsonSerializerOptions();
            var memberSet = resolver.Get("TestProperty", typeof(TestClass), options);

            var testInstance = new TestClass();
            memberSet.SetValue(testInstance, "TestValue");

            Assert.AreEqual("TestValue", testInstance.TestProperty);
        }

        [TestMethod]
        public void Get_ShouldResolveField()
        {
            var resolver = new NameConventionResolver();
            var options = new JsonSerializerOptions();
            var memberSet = resolver.Get("TestField", typeof(TestClass), options);

            var testInstance = new TestClass();
            memberSet.SetValue(testInstance, 123);

            Assert.AreEqual(123, testInstance.TestField);
        }

        [TestMethod]
        public void Get_ShouldThrowExceptionWhenPropertyNotFound()
        {
            var resolver = new NameConventionResolver();
            var options = new JsonSerializerOptions { ThrowExceptionWhenPropertyNotFound = true };

            Assert.ThrowsException(typeof(DeserializationException), () => resolver.Get("NonExistentProperty", typeof(TestClass), options));
        }

        [TestMethod]
        public void Get_ShouldReturnMemberSetWithIgnoreWhenPropertyNotFound()
        {
            var resolver = new NameConventionResolver();
            var options = new JsonSerializerOptions { ThrowExceptionWhenPropertyNotFound = false };
            var memberSet = resolver.Get("NonExistentProperty", typeof(TestClass), options);

            Assert.IsTrue(memberSet.Skip);
        }

        [TestMethod]
        public void Get_ShouldResolvePropertyWithCamelCaseNamingConvention()
        {
            var resolver = new NameConventionResolver(JsonNamingConventions.CamelCase);
            var options = new JsonSerializerOptions();
            var memberSet = resolver.Get("testProperty", typeof(TestClass), options);

            var testInstance = new TestClass();
            memberSet.SetValue(testInstance, "TestValue");

            Assert.AreEqual("TestValue", testInstance.TestProperty);
        }

        [TestMethod]
        public void Get_ShouldResolveFieldWithCamelCaseNamingConvention()
        {
            var resolver = new NameConventionResolver(JsonNamingConventions.CamelCase);
            var options = new JsonSerializerOptions();
            var memberSet = resolver.Get("testField", typeof(TestClass), options);

            var testInstance = new TestClass();
            memberSet.SetValue(testInstance, 123);

            Assert.AreEqual(123, testInstance.TestField);
        }
    }
}
