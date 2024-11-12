using System;
using System.Collections;

using nanoFramework.TestFramework;

using TuyaLink.Communication;
using TuyaLink.Events;
using TuyaLink.Functions.Properties;
using TuyaLink.Model;

namespace TuyaLink.Functions.Events
{
    [TestClass]
    internal class DeviceEventTests
    {
        private static DeviceEvent _deviceEvent;
        private static FakeDevice _device;
        private static EventModel _eventModel;

        [Setup]
        public void SetUp()
        {
            _device = FakeDevice.ValidateModelDevice;
            _device.FakeCommunication.TriggerEventDelegate = (deviceEvent, parameters, time) => ResponseHandler.FromResponse(new FunctionResponse()
            {
                Time = DateTime.UtcNow
            });
            _deviceEvent = new DeviceEvent("testCode", _device, true);
            _eventModel = new EventModel
            {
                Code = "testCode",
                OutputParams =
                [
                        new() { Code = "param1", TypeSpec = new TypeSpecifications { Type = PropertyDataType.String } },
                        new() { Code = "param2", TypeSpec = new TypeSpecifications { Type = PropertyDataType.String } }
                ]
            };
        }

        [TestMethod]
        public void TestAcknowledgeProperty()
        {
            Assert.IsTrue(_deviceEvent.Acknowledge);
        }

        [TestMethod]
        public void TestBindModel()
        {
            _deviceEvent.BindModel(_eventModel);
            Assert.AreEqual(_eventModel, _deviceEvent.Model);
        }

        [TestMethod]
        public void TestTriggerEvent()
        {
            _deviceEvent.BindModel(_eventModel);
            Hashtable parameters = new()
            {
                    { "param1", "value1" },
                    { "param2", "value2" }
                };
            DateTime time = DateTime.UtcNow;

            _deviceEvent.Trigger(parameters, time);
        }

        [TestMethod]
        public void TestTriggerEventWithMissingParameter()
        {
            _deviceEvent.BindModel(_eventModel);
            Hashtable parameters = new()
            {
                    { "param1", "value1" }
                };
            DateTime time = DateTime.UtcNow;

            Assert.ThrowsException(typeof(FunctionRuntimeException), () => _deviceEvent.Trigger(parameters, time));
        }

        [TestMethod]
        public void TestTriggerEventWithInvalidParameter()
        {
            _deviceEvent.BindModel(_eventModel);
            Hashtable parameters = new()
            {
                    { "param1", "value1" },
                    { "param2", 123 } // Assuming 123 is an invalid value for param2
                };
            DateTime time = DateTime.UtcNow;

            Assert.ThrowsException(typeof(FunctionRuntimeException), () => _deviceEvent.Trigger(parameters, time));
        }
    }
}
