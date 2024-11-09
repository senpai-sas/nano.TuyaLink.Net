using System;

using nanoFramework.TestFramework;

using TuyaLink.Communication;
using TuyaLink.Model;
using TuyaLink.Properties;

namespace TuyaLink.Functions.Properties
{
    [TestClass]
    public class DevicePropertyTests
    {
        private class TestDeviceProperty : DeviceProperty
        {
            public TestDeviceProperty(string code, TuyaDevice device, PropertyDataType type)
                : base(code, device, type) { }

            protected override void OnUpdate(object? value, object? oldValue) { }

            public new void Update(object? value) => base.Update(value);
        }

        private static FakeDevice _device;
        private static TestDeviceProperty _property;

        [Setup]
        public void Setup()
        {
            _device = FakeDevice.Default;
            _device.FakeCommunication.ReportPropertyDelegate = (property) =>
            {
                return ResponseHandler.FromResponse(new FunctionResponse()
                {
                    Time = DateTime.UtcNow,
                });
            };
            _property = new TestDeviceProperty("testCode", _device, PropertyDataType.String);
        }

        [TestMethod]
        public void TestCloudUpdate_ValidValue_UpdatesValue()
        {
            object newValue = "newValue";
            _property.CloudUpdate(newValue);
            Assert.AreEqual(newValue, _property.GetCloudValue());
        }

        [TestMethod]
        public void TestCloudUpdate_InvalidAccessMode_ThrowsException()
        {
            PropertyModel propertyModel = new()
            {
                TypeSpec = new TypeSpecifications()
                {
                    Type = PropertyDataType.String,
                },
                AccessMode = AccessMode.ReportOnly
            };
            _property.BindModel(propertyModel);
            _property.CloudUpdate("newValue");
        }

        [TestMethod]
        public void TestUpdate_InvalidLocalValue_ThrowsException()
        {
            PropertyModel propertyModel = new()
            {
                TypeSpec = new TypeSpecifications()
                {
                    Type = PropertyDataType.String,
                },
                AccessMode = AccessMode.ReportOnly
            };
            _property.BindModel(propertyModel);

            int value = 100;
            Assert.ThrowsException(typeof(FunctionRuntimeException), () => _property.Update(value));
        }

        [TestMethod]
        public void TestReport_ValidAccessMode_ReportsProperty()
        {
            PropertyModel propertyModel = new()
            {
                TypeSpec = new TypeSpecifications()
                {
                    Type = PropertyDataType.String,
                },
                AccessMode = AccessMode.ReportOnly
            };
            _property.BindModel(propertyModel);
            ResponseHandler response = _property.Report();
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public void TestReport_InvalidAccessMode_ThrowsException()
        {
            PropertyModel propertyModel = new()
            {
                TypeSpec = new TypeSpecifications()
                {
                    Type = PropertyDataType.String,
                },
                AccessMode = AccessMode.SendOnly
            };
            _property.BindModel(propertyModel);
            _property.Report();
        }
    }
}
