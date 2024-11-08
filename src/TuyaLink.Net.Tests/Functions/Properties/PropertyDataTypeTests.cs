using System;

using nanoFramework.TestFramework;

using TuyaLink.Model;


namespace TuyaLink.Functions.Properties
{
    [TestClass]
    public class PropertyDataTypeTests
    {
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
        public void FromValue_ShouldReturnCorrectTuyaDataType()
        {
            Assert.AreEqual(PropertyDataType.FromValue("float"), PropertyDataType.Float);
            Assert.AreEqual(PropertyDataType.FromValue("double"), PropertyDataType.Double);
            Assert.AreEqual(PropertyDataType.FromValue("string"), PropertyDataType.String);
            Assert.AreEqual(PropertyDataType.FromValue("date"), PropertyDataType.Date);
            Assert.AreEqual(PropertyDataType.FromValue("bool"), PropertyDataType.Boolean);
            Assert.AreEqual(PropertyDataType.FromValue("enum"), PropertyDataType.Enum);
            Assert.AreEqual(PropertyDataType.FromValue("raw"), PropertyDataType.Raw);
            Assert.AreEqual(PropertyDataType.FromValue("fault"), PropertyDataType.Fault);
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
