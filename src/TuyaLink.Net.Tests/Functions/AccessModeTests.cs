using System;

using nanoFramework.TestFramework;

namespace TuyaLink.Functions
{
    [TestClass]
    public class AccessModeTests
    {
        [TestMethod]
        public void SendAndReport_ShouldHaveCorrectProperties()
        {
            var mode = AccessMode.SendAndReport;
            Assert.IsTrue(mode.CanSend);
            Assert.IsTrue(mode.CanReport);
            Assert.AreEqual("rw", mode.EnumValue);
        }

        [TestMethod]
        public void ReportOnly_ShouldHaveCorrectProperties()
        {
            var mode = AccessMode.ReportOnly;
            Assert.IsFalse(mode.CanSend);
            Assert.IsTrue(mode.CanReport);
            Assert.AreEqual("wr", mode.EnumValue);
        }

        [TestMethod]
        public void SendOnly_ShouldHaveCorrectProperties()
        {
            var mode = AccessMode.SendOnly;
            Assert.IsTrue(mode.CanSend);
            Assert.IsFalse(mode.CanReport);
            Assert.AreEqual("ro", mode.EnumValue);
        }

        [TestMethod]
        [DataRow("rw", "Send And Report")]
        [DataRow("wr", "Report Only")]
        [DataRow("ro", "Send Only")]
        public void FromValue_ShouldReturnCorrectAccessMode(string value, string expectedName)
        {
            var mode = AccessMode.FromValue(value);
            Assert.AreEqual(expectedName, mode.Name);
        }

        [TestMethod]
        public void FromValue_ShouldThrowExceptionForUnknownValue()
        {
            Assert.ThrowsException(typeof(NotImplementedException), () => AccessMode.FromValue("unknown"));
        }
    }
}
