using System;

using nanoFramework.Json;
using nanoFramework.Json.Resolvers;
using nanoFramework.TestFramework;

using TuyaLink.Json;


namespace TuyaLink.Net.Json
{
    public enum Resolvers
    {
        Cache,
        NoCache
    }

    [TestClass]
    public class NameConventionResolverSpecs
    {


        private static IMemberResolver CreateResolver(int type)
        {
            Resolvers resolverType = (Resolvers)type;
            if (resolverType == Resolvers.Cache)
            {
                return new CacheNameConventionResolver(JsonNamingConventions.CamelCase);
            }
            else if (resolverType == Resolvers.NoCache)
            {
                return new NameConventionResolver(JsonNamingConventions.CamelCase);
            }
            else
            {
                throw new ArgumentException("Invalid IMemberResolver type");
            }
        }

        private class TestClass
        {
            public string TestProperty { get; set; }
            public int TestField;
            public string InternalProperty { get; internal set; }
        }


        [DataRow(0)]
        [DataRow(1)]
        public void Get_ShouldResolveProperty(int type)
        {
            JsonSerializerOptions options = new();
            IMemberResolver resolver = CreateResolver(type);
            MemberSet memberSet = resolver.Get("TestProperty", typeof(TestClass), options);

            TestClass testInstance = new();
            memberSet.SetValue(testInstance, "TestValue");

            Assert.AreEqual("TestValue", testInstance.TestProperty);
        }


        [DataRow(0)]
        [DataRow(1)]
        public void Get_ShouldResolveField(int type)
        {
            JsonSerializerOptions options = new();
            IMemberResolver resolver = CreateResolver(type);
            MemberSet memberSet = resolver.Get("TestField", typeof(TestClass), options);

            TestClass testInstance = new();
            memberSet.SetValue(testInstance, 123);

            Assert.AreEqual(123, testInstance.TestField);
        }


        [DataRow(0)]
        [DataRow(1)]
        public void Get_ShouldThrowExceptionWhenPropertyNotFound(int type)
        {
            JsonSerializerOptions options = new() { ThrowExceptionWhenPropertyNotFound = true };
            IMemberResolver resolver = CreateResolver(type);
            Assert.ThrowsException(typeof(TuyaLink.Json.DeserializationException), () => resolver.Get("NonExistentProperty", typeof(TestClass), options));
        }


        [DataRow(0)]
        [DataRow(1)]
        public void Get_ShouldReturnMemberSetWithIgnoreWhenPropertyNotFound(int type)
        {
            JsonSerializerOptions options = new() { ThrowExceptionWhenPropertyNotFound = false };
            IMemberResolver resolver = CreateResolver(type);
            MemberSet memberSet = resolver.Get("NonExistentProperty", typeof(TestClass), options);

            Assert.IsTrue(memberSet.Skip);
        }


        [DataRow(0)]
        [DataRow(1)]
        public void Get_ShouldResolvePropertyWithCamelCaseNamingConvention(int type)
        {
            JsonSerializerOptions options = new();
            IMemberResolver resolver = CreateResolver(type);
            MemberSet memberSet = resolver.Get("testProperty", typeof(TestClass), options);

            TestClass testInstance = new();
            memberSet.SetValue(testInstance, "TestValue");

            Assert.AreEqual("TestValue", testInstance.TestProperty);
        }


        [DataRow(0)]
        [DataRow(1)]
        public void Get_ShouldResolveFieldWithCamelCaseNamingConvention(int type)
        {
            JsonSerializerOptions options = new();
            IMemberResolver resolver = CreateResolver(type);
            MemberSet memberSet = resolver.Get("testField", typeof(TestClass), options);

            TestClass testInstance = new();
            memberSet.SetValue(testInstance, 123);

            Assert.AreEqual(123, testInstance.TestField);
        }


        [DataRow(0)]
        [DataRow(1)]
        public void Get_ShouldResolveInternalProperty(int type)
        {
            JsonSerializerOptions options = new();
            IMemberResolver resolver = CreateResolver(type);
            MemberSet memberSet = resolver.Get("InternalProperty", typeof(TestClass), options);

            TestClass testInstance = new();
            memberSet.SetValue(testInstance, "TestValue");

            Assert.AreEqual("TestValue", testInstance.InternalProperty);
        }
    }
}
