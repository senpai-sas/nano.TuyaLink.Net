

using nanoFramework.TestFramework;

namespace TuyaLink.Net
{
    [TestClass]
    internal class DataCenterTests
    {
        [TestMethod]
        public void TestChinaDataCenter()
        {
            var dataCenter = DataCenter.China;
            Assert.AreEqual("China", dataCenter.Name);
            Assert.AreEqual("m1.tuyacn.com", dataCenter.Url);
            Assert.AreEqual(8883, dataCenter.Port);
            Assert.AreEqual(1, dataCenter.EnumValue);
        }

        [TestMethod]
        public void TestCentralEuropeDataCenter()
        {
            var dataCenter = DataCenter.CentralEurope;
            Assert.AreEqual("Central Europe", dataCenter.Name);
            Assert.AreEqual("m1.tuyaeu.com", dataCenter.Url);
            Assert.AreEqual(8883, dataCenter.Port);
            Assert.AreEqual(2, dataCenter.EnumValue);
        }

        [TestMethod]
        public void TestWesternAmericaDataCenter()
        {
            var dataCenter = DataCenter.WensterAmerica;
            Assert.AreEqual("Western America", dataCenter.Name);
            Assert.AreEqual("m1.tuyaus.com", dataCenter.Url);
            Assert.AreEqual(8883, dataCenter.Port);
            Assert.AreEqual(3, dataCenter.EnumValue);
        }

        [TestMethod]
        public void TestEasternAmericaDataCenter()
        {
            var dataCenter = DataCenter.EasternAmerica;
            Assert.AreEqual("Eastern America", dataCenter.Name);
            Assert.AreEqual("m1-ueaz.tuyaus.com", dataCenter.Url);
            Assert.AreEqual(8883, dataCenter.Port);
            Assert.AreEqual(4, dataCenter.EnumValue);
        }

        [TestMethod]
        public void TestWesternEuropeDataCenter()
        {
            var dataCenter = DataCenter.WesternEurope;
            Assert.AreEqual("Western Europe", dataCenter.Name);
            Assert.AreEqual("m1-weaz.tuyaeu.com", dataCenter.Url);
            Assert.AreEqual(8883, dataCenter.Port);
            Assert.AreEqual(5, dataCenter.EnumValue);
        }

        [TestMethod]
        public void TestIndiaDataCenter()
        {
            var dataCenter = DataCenter.India;
            Assert.AreEqual("India", dataCenter.Name);
            Assert.AreEqual("m1.tuyain.com", dataCenter.Url);
            Assert.AreEqual(8883, dataCenter.Port);
            Assert.AreEqual(6, dataCenter.EnumValue);
        }

        [TestMethod]
        public void TestFromValue()
        {
            Assert.AreEqual(DataCenter.China, DataCenter.FromValue(1));
            Assert.AreEqual(DataCenter.CentralEurope, DataCenter.FromValue(2));
            Assert.AreEqual(DataCenter.WensterAmerica, DataCenter.FromValue(3));
            Assert.AreEqual(DataCenter.EasternAmerica, DataCenter.FromValue(4));
            Assert.AreEqual(DataCenter.WesternEurope, DataCenter.FromValue(5));
            Assert.AreEqual(DataCenter.India, DataCenter.FromValue(6));
        }
    }
}
