using System.Collections;

using nanoFramework.TestFramework;

namespace nano.SmartEnum.Tests
{
    [TestClass]
    public class SmartEnumTests
    {
        private class TestEnum : SmartEnum
        {
            private static readonly Hashtable _store = new();

            public static readonly TestEnum Value1 = new("Value1", 1);
            public static readonly TestEnum Value2 = new("Value2", 2);

            private TestEnum(string name, object value) : base(name, value) { }

            public static TestEnum GetFromValue(int value)
            {
                return (TestEnum)GetFromValue(value, typeof(TestEnum), _store);
            }
        }

        private class TestEnum2 : SmartEnum
        {
            private static readonly Hashtable _store = new();
            public static readonly TestEnum2 Value1 = new("Value1", "value1");
            public static readonly TestEnum2 Value2 = new("Value2", "value2");

            private TestEnum2(string name, string value) : base(name, value) 
            {
                
            }

            public static TestEnum2 GetFromValue(string value)
            {
                return (TestEnum2)GetFromValue(value, typeof(TestEnum2), _store);
            }
        }

        [TestMethod]
        public void ToString_ReturnsName()
        {
            Assert.AreEqual("Value1", TestEnum.Value1.ToString());
        }

        [TestMethod]
        public void Equals_ReturnsTrueForSameValue()
        {
            Assert.IsTrue(TestEnum.Value1.Equals(TestEnum.Value1));
        }

        [TestMethod]
        public void Equals_ReturnsFalseForDifferentValue()
        {
            Assert.IsFalse(TestEnum.Value1.Equals(TestEnum.Value2));
        }

        [TestMethod]
        public void GetHashCode_ReturnsSameHashCodeForSameValue()
        {
            Assert.AreEqual(TestEnum.Value1.GetHashCode(), TestEnum.Value1.GetHashCode());
        }

        [TestMethod]
        public void GetHashCode_ReturnsDifferentHashCodeForDifferentValue()
        {
            Assert.AreNotEqual(TestEnum.Value1.GetHashCode(), TestEnum.Value2.GetHashCode());
        }

        [TestMethod]
        public void OperatorEquals_ReturnsTrueForSameValue()
        {
            Assert.IsTrue(TestEnum.Value1 == TestEnum.Value1);
        }

        [TestMethod]
        public void OperatorNotEquals_ReturnsTrueForDifferentValue()
        {
            Assert.IsTrue(TestEnum.Value1 != TestEnum.Value2);
        }

        [TestMethod]
        public void GetFromValue_ReturnsCorrectEnum()
        {
            var result = TestEnum.GetFromValue(1);
            Assert.AreEqual(TestEnum.Value1, result);
            var result2 = TestEnum2.GetFromValue("value1");
            Assert.AreEqual(TestEnum2.Value1, result2);
        }

        [TestMethod]
        public void GetFromValue_ReturnsNullForInvalidValue()
        {
            var result = (TestEnum)TestEnum.GetFromValue(3);
            Assert.IsNull(result);
        }
    }
}
