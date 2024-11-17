using System;
using System.Collections;
using System.Text;

using nanoFramework.M2Mqtt.Messages;
using nanoFramework.TestFramework;

using TuyaLink.Actions;
using TuyaLink.Communication.Events;
using TuyaLink.Events;
using TuyaLink.Functions;
using TuyaLink.Functions.Actions;
using TuyaLink.Functions.Properties;
using TuyaLink.Model;
using TuyaLink.Properties;

namespace TuyaLink.Communication.Mqtt
{
    [TestClass]
    public class MqttCommunicationProtocolTests
    {
        private static FakeMqttClient _fakeMqttClient;
        private static MqttCommunicationProtocol _protocol;
        private static FakeDevice _device;

        [Setup]
        public void Setup()
        {
            _fakeMqttClient = new();
            _device = FakeDevice.ValidateModelDevice;
            _protocol = new MqttCommunicationProtocol(_device, _device.Settings, _fakeMqttClient);
        }

        [TestMethod]
        public void Connect_ValidDeviceInfo_InitializesAndConnects()
        {
            _protocol.Connect();
            Assert.IsTrue(_fakeMqttClient.IsConnected);
        }

        [TestMethod]
        public void Disconnect_DisconnectsClient()
        {
            _protocol.Disconnect();
            Assert.IsFalse(_fakeMqttClient.IsConnected);
        }

        [TestMethod]
        public void ReportProperty_ValidProperty_PublishesRequest()
        {
            DeviceProperty property = new DelegateDeviceProperty("testProp", _device, PropertyDataType.String,
                (oldValue, newValue) =>
                {

                });

            ResponseHandler response = _protocol.ReportProperty(property);

            Assert.IsNotNull(response);
            Assert.AreEqual("tylink/testDeviceId/thing/property/report", _fakeMqttClient.LastPublishedTopic);
        }

        [TestMethod]
        public void BatchReport_ValidPropertiesAndEvents_PublishesRequest()
        {
            DeviceProperty property = new DelegateDeviceProperty("testProp", _device, PropertyDataType.String,
                (oldValue, newValue) =>
                {

                });
            DeviceProperty[] properties = new[] { property };
            TriggerEventData[] events = new[] { new TriggerEventData { EventCode = "testEvent1" }, new TriggerEventData { EventCode = "testEvent2" } };
            ResponseHandler response = _protocol.BatchReport(properties, events);

            Assert.IsNotNull(response);
            Assert.AreEqual("tylink/testDeviceId/thing/data/batch_report", _fakeMqttClient.LastPublishedTopic);
        }

        [TestMethod]
        public void TriggerEvent_ValidEvent_PublishesRequest()
        {
            DeviceEvent deviceEvent = new("event1", _device, true);
            Hashtable parameters = new();
            ResponseHandler response = _protocol.TriggerEvent(deviceEvent, parameters, DateTime.UtcNow);

            Assert.IsNotNull(response);
            Assert.AreEqual("tylink/testDeviceId/thing/event/trigger", _fakeMqttClient.LastPublishedTopic);
        }

        [TestMethod]
        public void GetDeviceModel_PublishesRequest()
        {
            ResponseHandler response = _protocol.GetDeviceModel();

            Assert.IsNotNull(response);
            Assert.AreEqual("tylink/testDeviceId/thing/model/get", _fakeMqttClient.LastPublishedTopic);
        }

        [TestMethod]
        public void HistoryReport_ValidEventsAndProperties_PublishesRequest()
        {
            DeviceProperty property = new DelegateDeviceProperty("testProp", _device, PropertyDataType.String,
                (oldValue, newValue) =>
                {

                });
            TriggerEventData[][] events = new[] { new[] { new TriggerEventData { EventCode = "testEvent1" } } };
            DeviceProperty[][] properties = new[] { new[] { property } };
            ResponseHandler response = _protocol.HistoryReport(events, properties);

            Assert.IsNotNull(response);
            Assert.AreEqual("tylink/testDeviceId/thing/data/history_report", _fakeMqttClient.LastPublishedTopic);
        }

        [TestMethod]
        public void GetProperties_ValidProperties_PublishesRequest()
        {

            DeviceProperty property = new DelegateDeviceProperty("testProp", _device, PropertyDataType.String,
                (oldValue, newValue) =>
                {

                });
            DeviceProperty[] properties = new[] { property };
            ResponseHandler response = _protocol.GetProperties(properties);

            Assert.IsNotNull(response);
            Assert.AreEqual("tylink/testDeviceId/thing/property/desired/get", _fakeMqttClient.LastPublishedTopic);
        }

        [TestMethod]
        public void SetProperties_ValidProperties_PublishesRequest()
        {

            object lastValue = null;
            DeviceProperty property = new DelegateDeviceProperty("testProp", _device, PropertyDataType.String,
                (value, oldValue) =>
                {
                    lastValue = value;
                });

            string expectedValue = "testValue";

            _device.AddProperty(property);

            property.BindModel(new PropertyModel()
            {
                AccessMode = AccessMode.SendAndReport,
                Code = property.Code,
                TypeSpec = new TypeSpecifications()
                {
                    Type = PropertyDataType.String,
                    Maxlen = 200,
                }
            });


            string propertySetMessage =
@$"{{
	""msgId"":""45lkj3551234***"",
  	""time"":1626197189638,
	""data"":{{
    	""{property.Code}"":""{expectedValue}""
	}}
}}";

            byte[] bytes = Encoding.UTF8.GetBytes(propertySetMessage);
            _fakeMqttClient.OnMqttMsgPublishReceived(new MqttMsgPublishEventArgs("tylink/testDeviceId/thing/property/set", bytes, false, MqttQoSLevel.AtLeastOnce, false));

            Assert.IsNotNull(lastValue);
            Assert.AreEqual(expectedValue, lastValue);
            Assert.AreEqual("tylink/testDeviceId/thing/property/set_response", _fakeMqttClient.LastPublishedTopic);
        }

        [TestMethod]
        public void ActionExecute_ValidAction_PublishedRequest()
        {

            Hashtable inputParameters = null;
            Hashtable outputParameters = [];
            outputParameters.Add("outParam1", "outValue1");

            DelegateDeviceAction action = new("testAction", _device,
                (parameters) =>
                {
                    inputParameters = parameters;
                    return ActionExecuteResult.Success(outputParameters);
                });

            action.BindModel(new ActionModel()
            {
                Code = action.Code,
                InputParams = [
                        new ParameterModel()
                    {
                        Code = "inParam1",
                        TypeSpec = new TypeSpecifications(){
                            Type = PropertyDataType.String,
                            Maxlen = 200,
                        }}
                    ],
                OutputParams = [
                        new ParameterModel()
                    {
                        Code = "outParam1",
                        TypeSpec = new TypeSpecifications(){
                            Type = PropertyDataType.String,
                            Maxlen = 200,
                        }}
                    ]
            });


            _device.AddAction(action);

            string actionExecuteMessage =
    $@"{{
	""msgId"":""45lkj3551234***"",
  	""time"":1626197189638,
	""data"":{{
      	""actionCode"": ""{action.Code}"",
      	""inputParams"": {{
          ""inParam1"":""value1"",
          ""inParam2"":""value2""
    	}}
	}}
}}
";
            byte[] bytes = Encoding.UTF8.GetBytes(actionExecuteMessage);
            _fakeMqttClient.OnMqttMsgPublishReceived(new MqttMsgPublishEventArgs("tylink/testDeviceId/thing/action/execute", bytes, false, MqttQoSLevel.AtLeastOnce, false));

            Assert.IsNotNull(inputParameters);
            Assert.AreEqual("value1", inputParameters["inParam1"]);
            Assert.AreEqual("value2", inputParameters["inParam2"]);
            Assert.AreEqual("tylink/testDeviceId/thing/action/execute_response", _fakeMqttClient.LastPublishedTopic);
        }
    }
}
