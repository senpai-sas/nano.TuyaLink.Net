using System;

using nanoFramework.TestFramework;

using TuyaLink.Model;

namespace TuyaLink.Functions
{
    [TestClass]
    public class DeviceFunctionTests
    {
        [TestMethod]
        public void Constructor_ShouldThrowArgumentException_WhenNameIsNullOrEmpty()
        {
            DeviceInfo info = new("TestProduct", "TestDevice", "TestSecret");
            Assert.ThrowsException(typeof(ArgumentException), () => new TestDeviceFunction(null, FunctionType.Property, FakeDevice.Default));
            Assert.ThrowsException(typeof(ArgumentException), () => new TestDeviceFunction(string.Empty, FunctionType.Property, FakeDevice.Default));
        }

        [TestMethod]
        public void Constructor_ShouldThrowArgumentNullException_WhenTypeIsNull()
        {
            Assert.ThrowsException(typeof(ArgumentNullException), () => new TestDeviceFunction("TestCode", null, FakeDevice.Default));
        }

        [TestMethod]
        public void Constructor_ShouldThrowArgumentNullException_WhenDeviceIsNull()
        {
            Assert.ThrowsException(typeof(ArgumentNullException), () => new TestDeviceFunction("TestCode", FunctionType.Action, null));
        }

        [TestMethod]
        public void BindModel_ShouldSetModelAndCheckModel()
        {
            TuyaDevice device = new FakeDevice(FakeDevice.DefaultInfo, new DeviceSettings { ValidateModel = true });
            TestDeviceFunction function = new("TestCode", FunctionType.Property, device);
            PropertyModel model = new() { Code = "TestCode" };

            function.BindModel(model);

            Assert.AreEqual(model, function.Model);
        }

        [TestMethod]
        public void CheckModel_ShouldThrowTuyaLinkException_WhenModelIsNull()
        {
            TuyaDevice device = new FakeDevice(FakeDevice.DefaultInfo, new DeviceSettings { ValidateModel = true });
            TestDeviceFunction function = new("TestCode", FunctionType.Property, device);

            Assert.ThrowsException(typeof(TuyaLinkException), () => function.BindModel(null));
        }

        [TestMethod]
        public void CheckModel_ShouldThrowTuyaLinkException_WhenModelCodeDoesNotMatch()
        {
            TuyaDevice device = new FakeDevice(FakeDevice.DefaultInfo, new DeviceSettings { ValidateModel = true });
            TestDeviceFunction function = new("TestCode", FunctionType.Event, device);

            EventModel model = new() { Code = "DifferentCode" };

            Assert.ThrowsException(typeof(TuyaLinkException), () => function.BindModel(model));
        }


        [TestMethod]
        public void CheckModel_ShouldThrowTuyaLinkException_WhenModelFunctionTypeDoesNotMatch()
        {
            TuyaDevice device = new FakeDevice(FakeDevice.DefaultInfo, new DeviceSettings { ValidateModel = true });
            TestDeviceFunction function = new("TestCode", FunctionType.Event, device);
            FunctionModel model = new ActionModel() { Code = "TestCode" };

            Assert.ThrowsException(typeof(TuyaLinkException), () => function.BindModel(model));
        }

        private class TestDeviceFunction : DeviceFunction
        {
            public TestDeviceFunction(string name, FunctionType type, TuyaDevice device) : base(name, type, device)
            {
            }

            protected override void ValidateModel()
            {
                // Custom validation logic for testing
            }

            internal new void BindModel(FunctionModel model)
            {
                base.BindModel(model);
            }
        }
    }
}
