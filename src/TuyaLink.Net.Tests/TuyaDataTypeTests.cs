
using nanoFramework.TestFramework;

using System;

namespace TuyaLink.Tests
{
    [TestClass]
    public class TuyaDataTypeTests
    {
        [TestMethod]
        public void IsValidValue_ShouldReturnTrue_ForValidValues()
        {
            Assert.IsTrue(TuyaDataType.Float.IsValidValue(1.23f));
            Assert.IsTrue(TuyaDataType.Double.IsValidValue(1.23));
            Assert.IsTrue(TuyaDataType.String.IsValidValue("test"));
            Assert.IsTrue(TuyaDataType.Date.IsValidValue(DateTime.UtcNow));
            Assert.IsTrue(TuyaDataType.Boolean.IsValidValue(true));
            Assert.IsTrue(TuyaDataType.Enum.IsValidValue(TuyaDataType.Value));
            Assert.IsTrue(TuyaDataType.Raw.IsValidValue(new byte[] { 1, 2, 3 }));
            Assert.IsTrue(TuyaDataType.Fault.IsValidValue(new DeviceFault()));
        }

        [TestMethod]
        public void IsValidValue_ShouldReturnFalse_ForInvalidValues()
        {
            Assert.IsFalse(TuyaDataType.Float.IsValidValue("not a float"));
            Assert.IsFalse(TuyaDataType.Double.IsValidValue("not a double"));
            Assert.IsFalse(TuyaDataType.String.IsValidValue(123));
            Assert.IsFalse(TuyaDataType.Date.IsValidValue("not a date"));
            Assert.IsFalse(TuyaDataType.Boolean.IsValidValue("not a bool"));
            Assert.IsFalse(TuyaDataType.Enum.IsValidValue("not an enum"));
            Assert.IsFalse(TuyaDataType.Raw.IsValidValue("not a byte array"));
            Assert.IsFalse(TuyaDataType.Fault.IsValidValue("not a device fault"));
        }

        [TestMethod]
        public void FromValue_ShouldReturnCorrectTuyaDataType()
        {
            Assert.AreEqual(TuyaDataType.FromValue("float"), TuyaDataType.Float);
            Assert.AreEqual(TuyaDataType.FromValue("double"), TuyaDataType.Double);
            Assert.AreEqual(TuyaDataType.FromValue("string"), TuyaDataType.String);
            Assert.AreEqual(TuyaDataType.FromValue("date"), TuyaDataType.Date);
            Assert.AreEqual(TuyaDataType.FromValue("bool"), TuyaDataType.Boolean);
            Assert.AreEqual(TuyaDataType.FromValue("enum"), TuyaDataType.Enum);
            Assert.AreEqual(TuyaDataType.FromValue("raw"), TuyaDataType.Raw);
            Assert.AreEqual(TuyaDataType.FromValue("fault"), TuyaDataType.Fault);
        }
    }
}
