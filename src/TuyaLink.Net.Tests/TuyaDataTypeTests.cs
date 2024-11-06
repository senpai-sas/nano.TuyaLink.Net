
using nanoFramework.TestFramework;

using System;
using TuyaLink.Functions.Properties;

namespace TuyaLink.Tests
{
    [TestClass]
    public class TuyaDataTypeTests
    {
        [TestMethod]
        public void IsValidValue_ShouldReturnTrue_ForValidValues()
        {
            Assert.IsTrue(PropertyDataType.Float.IsValidValue(1.23f));
            Assert.IsTrue(PropertyDataType.Double.IsValidValue(1.23));
            Assert.IsTrue(PropertyDataType.String.IsValidValue("test"));
            Assert.IsTrue(PropertyDataType.Date.IsValidValue(DateTime.UtcNow));
            Assert.IsTrue(PropertyDataType.Boolean.IsValidValue(true));
            Assert.IsTrue(PropertyDataType.Enum.IsValidValue(PropertyDataType.Value));
            Assert.IsTrue(PropertyDataType.Raw.IsValidValue(new byte[] { 1, 2, 3 }));
            Assert.IsTrue(PropertyDataType.Fault.IsValidValue(new DeviceFault()));
        }

        [TestMethod]
        public void IsValidValue_ShouldReturnFalse_ForInvalidValues()
        {
            Assert.IsFalse(PropertyDataType.Float.IsValidValue("not a float"));
            Assert.IsFalse(PropertyDataType.Double.IsValidValue("not a double"));
            Assert.IsFalse(PropertyDataType.String.IsValidValue(123));
            Assert.IsFalse(PropertyDataType.Date.IsValidValue("not a date"));
            Assert.IsFalse(PropertyDataType.Boolean.IsValidValue("not a bool"));
            Assert.IsFalse(PropertyDataType.Enum.IsValidValue("not an enum"));
            Assert.IsFalse(PropertyDataType.Raw.IsValidValue("not a byte array"));
            Assert.IsFalse(PropertyDataType.Fault.IsValidValue("not a device fault"));
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
    }
}
