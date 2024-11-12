using System;

using nanoFramework.TestFramework;

using TuyaLink.Model;


namespace TuyaLink.Functions.Properties
{
    [TestClass]
    public class PropertyDataTypeTests
    {
        private class TestEnum : PropertySmartEnum
        {
            public TestEnum(string value) : base(value, value)
            {

            }
        }
        [TestMethod]
        public void TestFromValue_ValidValue_ReturnsCorrectType()
        {
            PropertyDataType result = PropertyDataType.FromValue("float");
            Assert.AreEqual(PropertyDataType.Float, result);
        }

        [TestMethod]
        public void TestFromValue_InvalidValue_ThrowsException()
        {
            Assert.ThrowsException(typeof(NotImplementedException), () => PropertyDataType.FromValue("invalid"));
        }

        [TestMethod]
        public void TestIsValidValue_ValidValue_ReturnsTrue()
        {
            bool result = PropertyDataType.Float.IsValidCloudValue(1.23f);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestIsValidValue_InvalidValue_ReturnsFalse()
        {
            bool result = PropertyDataType.Float.IsValidCloudValue("not a float");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestValidateModel_ValidModel_ReturnsTrue()
        {
            TypeSpecifications specifications = new() { Type = PropertyDataType.Float };
            bool result = PropertyDataType.Float.ValidateModel(specifications, 1.23f);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestValidateModel_InvalidModel_ReturnsFalse()
        {
            TypeSpecifications specifications = new() { Type = PropertyDataType.Float };
            bool result = PropertyDataType.Float.ValidateModel(specifications, "not a float");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValidValue_ShouldReturnTrue_ForValidValues()
        {
            Assert.IsTrue(PropertyDataType.Float.IsValidCloudValue(1.23f));
            Assert.IsTrue(PropertyDataType.Double.IsValidCloudValue(1.23));
            Assert.IsTrue(PropertyDataType.String.IsValidCloudValue("test"));
            Assert.IsTrue(PropertyDataType.Date.IsValidCloudValue(1230124L));
            Assert.IsTrue(PropertyDataType.Boolean.IsValidCloudValue(true));
            Assert.IsTrue(PropertyDataType.Enum.IsValidCloudValue("1"));
            Assert.IsTrue(PropertyDataType.Raw.IsValidCloudValue(new byte[] { 1, 2, 3 }));
            Assert.IsTrue(PropertyDataType.Fault.IsValidCloudValue("any fault"));
        }

        [TestMethod]
        public void IsValidValue_ShouldReturnFalse_ForInvalidValues()
        {
            Assert.IsFalse(PropertyDataType.Float.IsValidCloudValue("not a float"));
            Assert.IsFalse(PropertyDataType.Double.IsValidCloudValue("not a double"));
            Assert.IsFalse(PropertyDataType.String.IsValidCloudValue(123));
            Assert.IsFalse(PropertyDataType.Date.IsValidCloudValue("not a date"));
            Assert.IsFalse(PropertyDataType.Boolean.IsValidCloudValue("not a bool"));
            Assert.IsFalse(PropertyDataType.Enum.IsValidCloudValue(123));
            Assert.IsFalse(PropertyDataType.Raw.IsValidCloudValue("not a byte array"));
            Assert.IsFalse(PropertyDataType.Fault.IsValidCloudValue(123));
        }

        [TestMethod]
        public void TestCheckCouldValue_ValueDataType_ValidValue()
        {
            TypeSpecifications specs = new() { Type = PropertyDataType.Value, Min = 0, Max = 100 };
            PropertyDataType.Value.CheckCouldValue(specs, 50.0);
        }

        [TestMethod]
        public void TestCheckCouldValue_ValueDataType_InvalidValue()
        {
            TypeSpecifications specs = new() { Type = PropertyDataType.Value, Min = 0, Max = 100 };
            Assert.ThrowsException(typeof(ArgumentOutOfRangeException), () => PropertyDataType.Value.CheckCouldValue(specs, 150.0));
        }

        [TestMethod]
        public void TestCheckCouldValue_EnumDataType_ValidValue()
        {
            TypeSpecifications specs = new() { Type = PropertyDataType.Enum, Label = new[] { "1", "2", "3" } };
            PropertyDataType.Enum.CheckCouldValue(specs, new TestEnum("1"));
        }

        [TestMethod]
        public void TestCheckCouldValue_EnumDataType_InvalidValue()
        {
            TypeSpecifications specs = new() { Type = PropertyDataType.Enum, Label = new[] { "1", "2", "3" } };
            Assert.ThrowsException(typeof(ArgumentException), () => PropertyDataType.Enum.CheckCouldValue(specs, new TestEnum("4")));
        }

        [TestMethod]
        public void TestCheckCouldValue_StringDataType_ValidValue()
        {
            TypeSpecifications specs = new() { Type = PropertyDataType.String, Maxlen = 10 };
            PropertyDataType.String.CheckCouldValue(specs, "test");
        }

        [TestMethod]
        public void TestCheckCouldValue_StringDataType_InvalidValue()
        {
            TypeSpecifications specs = new() { Type = PropertyDataType.String, Maxlen = 3 };
            Assert.ThrowsException(typeof(ArgumentOutOfRangeException), () => PropertyDataType.String.CheckCouldValue(specs, "test"));
        }

        [TestMethod]
        public void TestCheckCouldValue_FaultDataType_ValidValue()
        {
            TypeSpecifications specs = new() { Type = PropertyDataType.Fault, Range = new[] { "fault1", "fault2" } };
            PropertyDataType.Fault.CheckCouldValue(specs, "fault1");
        }

        [TestMethod]
        public void TestCheckCouldValue_FaultDataType_InvalidValue()
        {
            TypeSpecifications specs = new() { Type = PropertyDataType.Fault, Range = new[] { "fault1", "fault2" } };
            Assert.ThrowsException(typeof(ArgumentException), () => PropertyDataType.Fault.CheckCouldValue(specs, "fault3"));
        }
        [TestMethod]
        public void TestIsValidLocalValue_ValidValue_ReturnsTrue()
        {
            Assert.IsTrue(PropertyDataType.Float.IsValidLocalValue(1.23f));
            Assert.IsTrue(PropertyDataType.Double.IsValidLocalValue(1.23));
            Assert.IsTrue(PropertyDataType.String.IsValidLocalValue("test"));
            Assert.IsTrue(PropertyDataType.Date.IsValidLocalValue(DateTime.UtcNow));
            Assert.IsTrue(PropertyDataType.Boolean.IsValidLocalValue(true));
            Assert.IsTrue(PropertyDataType.Enum.IsValidLocalValue(PropertyDataType.Enum));
            Assert.IsTrue(PropertyDataType.Raw.IsValidLocalValue(new byte[] { 1, 2, 3 }));
            Assert.IsTrue(PropertyDataType.Fault.IsValidLocalValue("any fault"));
        }

        [TestMethod]
        public void TestIsValidLocalValue_InvalidValue_ReturnsFalse()
        {
            Assert.IsFalse(PropertyDataType.Float.IsValidLocalValue("not a float"));
            Assert.IsFalse(PropertyDataType.Double.IsValidLocalValue("not a double"));
            Assert.IsFalse(PropertyDataType.String.IsValidLocalValue(123));
            Assert.IsFalse(PropertyDataType.Date.IsValidLocalValue("not a date"));
            Assert.IsFalse(PropertyDataType.Boolean.IsValidLocalValue("not a bool"));
            Assert.IsFalse(PropertyDataType.Enum.IsValidLocalValue(123));
            Assert.IsFalse(PropertyDataType.Raw.IsValidLocalValue("not a byte array"));
            Assert.IsFalse(PropertyDataType.Fault.IsValidLocalValue(123));
        }
    }
}
