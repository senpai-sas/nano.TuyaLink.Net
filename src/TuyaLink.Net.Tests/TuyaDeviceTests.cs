using System;
using System.Collections;

using nanoFramework.TestFramework;
using TuyaLink.Events;
using TuyaLink.Functions.Actions;
using TuyaLink.Functions;

using TuyaLink.Properties;
using TuyaLink.Functions.Properties;
using TuyaLink.Actions;
using TuyaLink.Communication;
using TuyaLink.Communication.Events;
using TuyaLink.Functions.Events;

namespace TuyaLink
{
    [TestClass]
    public class TuyaDeviceTests
    {

        static ICommunicationHandler protocol = FakeCommunicationHandler.Default;

        
        [TestMethod]
        public void TestDeviceInitialization()
        {
            var deviceInfo = new DeviceInfo("deviceId", "deviceName", "secrect");
            var deviceSettings = new DeviceSettings();
            var device = new TuyaDevice(deviceInfo, deviceSettings, protocol);

            Assert.AreEqual(deviceInfo, device.Info);
            Assert.AreEqual(deviceSettings, device.Settings);
        }

        [TestMethod]
        public void TestAddProperty()
        {
            var deviceInfo = new DeviceInfo("deviceId", "deviceName", "secrect");
            var device = new TuyaDevice(deviceInfo, null, protocol);
            object lastValue = null;
            var property = new DelegateDeviceProperty("propertyCode", device, PropertyDataType.String, (oldValue, value) => lastValue = value);

            device.AddProperty(property);

            Assert.IsTrue(device.Properties.Contains("propertyCode"));
            Assert.AreEqual(property, device.Properties["propertyCode"]);
        }

        [TestMethod]
        public void TestAddEvent()
        {
            var deviceInfo = new DeviceInfo("deviceId", "deviceName", "secrect");
            var device = new TuyaDevice(deviceInfo, null, communicationProtocol: protocol);
            var deviceEvent = new DeviceEvent("eventCode", device, true);

            device.AddEvent(deviceEvent);

            Assert.IsTrue(device.Events.Contains("eventCode"));
            Assert.AreEqual(deviceEvent, device.Events["eventCode"]);
        }

        [TestMethod]
        public void TestAddAction()
        {
            var deviceInfo = new DeviceInfo("deviceId", "deviceName", "secrect");
            var device = new TuyaDevice(deviceInfo, null,  protocol);

            Hashtable args = null;
            Hashtable outputParams = new Hashtable();
            var deviceAction = new DelegateDeviceAction("actionCode", device, (inputArgs) =>
            {
                args = inputArgs;
                return ActionExecuteResult.Success(outputParams);
            });

            device.AddAction(deviceAction);

            Assert.IsTrue(device.Actions.Contains("actionCode"));
            Assert.AreEqual(deviceAction, device.Actions["actionCode"]);
        }

        [TestMethod]
        public void TestActionExecute()
        {
            var deviceInfo = new DeviceInfo("deviceId", "deviceName", "secrect");
            var device = new TuyaDevice(deviceInfo, null, protocol);

            Hashtable args = null;
            Hashtable outputParams = new Hashtable();
            var deviceAction = new DelegateDeviceAction("actionCode", device, (inputArgs) =>
            {
                args = inputArgs;
                return ActionExecuteResult.Success(outputParams);
            });
            device.AddAction(deviceAction);
            var inputParameters = new Hashtable
            {
                { "key", "value" }
            };
            var result = device.ActionExecute("actionCode", inputParameters);

            Assert.AreEqual(StatusCode.Success, result.Code);
            Assert.AreEqual(outputParams, result.OutputParameters);
            Assert.AreEqual(inputParameters, args);
        }

        [TestMethod]
        public void TestPropertySet()
        {
            var deviceInfo = new DeviceInfo("deviceId", "deviceName", "secrect");
            var device = new TuyaDevice(deviceInfo, null, protocol);
            object lastValue = null;
            var property = new DelegateDeviceProperty("propertyCode", device, PropertyDataType.String, (oldValue, value) => lastValue = value);

            device.AddProperty(property);

            var properties = new Hashtable
    {
        { "propertyCode", "newValue" }
    };

            var result = device.PropertySet(properties);

            Assert.AreEqual(StatusCode.Success, result);
            Assert.AreEqual("newValue", property.GetCloudValue());
        }
        [TestMethod]
        public void TestRegisterEvent()
        {
            bool eventTriggered = false;
            void triggerDelegate(Hashtable parameters, DateTime time) => eventTriggered = true;
            var device = FakeDevice.Default;
            var deviceEvent = device.RegisterEvent("eventCode", triggerDelegate);

            Assert.IsTrue(device.Events.Contains("eventCode"));
            Assert.AreEqual(deviceEvent, device.Events["eventCode"]);
            Assert.IsFalse(eventTriggered);
        }
        [TestMethod]
        public void TestActionExecute_NonRegisterFunction()
        {
            var deviceInfo = new DeviceInfo("deviceId", "deviceName", "secrect");
            var device = new TuyaDevice(deviceInfo, null, protocol);

            var inputParameters = new Hashtable
            {
                { "key", "value" }
            };
            Assert.ThrowsException(typeof (FunctionRuntimeException) , () =>  device.ActionExecute("nonExistentActionCode", inputParameters));

        }
        [TestMethod]
        public void TestPropertySet_FunctionNotFound()
        {
            var deviceInfo = new DeviceInfo("deviceId", "deviceName", "secrect");
            var device = new TuyaDevice(deviceInfo, null, protocol);

            var properties = new Hashtable
            {
                { "nonExistentPropertyCode", "newValue" }
            };

            var result = device.PropertySet(properties);

            Assert.AreEqual(StatusCode.FunctionNotFound, result);
        }
    }
}
