using System.Collections;

using nanoFramework.TestFramework;

using TuyaLink.Communication;
using TuyaLink.Communication.Actions;
using TuyaLink.Communication.Events;
using TuyaLink.Communication.History;
using TuyaLink.Communication.Properties;
using TuyaLink.Functions;
using TuyaLink.Functions.Events;

namespace TuyaLink.Json
{
    [TestClass]
    public class MessagesSerializationTests
    {
        public const string ReportPropertyRequest = @"{""msgId"":""45lkj3551234***"",""time"":1626197189638,""data"":{""color"":{""value"":""red"",""time"":1626197189638},""brightness"":{""value"":80,""time"":1626197189638}}}";

        [Setup]
        public void Setup()
        {
            JsonUtils.Initialize();
        }

        [TestMethod]
        public void ReportPropertyRequest_Serialize()
        {
            ReportPropertyRequest request = new()
            {
                MessageId = "45lkj3551234***",
                Time = 1626197189638,
                Data = new PropertyHashtable()
                {
                    ["Color"] = new PropertyValue()
                    {
                        Value = "red",
                        Time = 1626197189638
                    },
                    ["Brightness"] = new PropertyValue()
                    {
                        Value = 80,
                        Time = 1626197189638
                    }
                }
            };
            string json = JsonUtils.Serialize(request);
            ReportPropertyRequest result = (ReportPropertyRequest)JsonUtils.Deserialize(json, request.GetType());
            Assert.AreEqual(request.MessageId, result.MessageId);
            Assert.AreEqual(request.Time, result.Time);
            Assert.AreEqual(request.Data.Count, result.Data.Count);
            Assert.AreEqual(request.Data["Color"].Value, result.Data["Color"].Value);
            Assert.AreEqual(request.Data["Color"].Time, result.Data["Color"].Time);
            Assert.AreEqual(request.Data["Brightness"].Value, result.Data["Brightness"].Value);
            Assert.AreEqual(request.Data["Brightness"].Time, result.Data["Brightness"].Time);
            Assert.AreEqual(request.Sys.Ack, result.Sys.Ack);
        }


        [DataRow(@"{""msgId"":""45lkj3551234***"",""time"":1626197189640,""code"":0}", "45lkj3551234***", 1626197189640, 0)]
        [DataRow(@"{""msgId"":""45lkj3551234**1"",""time"":1626197189641,""code"":1002}", "45lkj3551234**1", 1626197189641, 1002)]
        public void FunctionResponse_Deserialize(string json, string msgId, long time, int code)
        {
            var expectedCode = StatusCode.FromValue(code);
            Assert.IsNotNull(expectedCode);
            FunctionResponse result = (FunctionResponse)JsonUtils.Deserialize(json, typeof(FunctionResponse));
            Assert.AreEqual(msgId, result.MessageId);
            Assert.AreEqual(time, result.Time);
            Assert.AreEqual(expectedCode, result.Code);
        }

        [TestMethod]
        public void PropertySet_Deserialize()
        {
            string json = @"{""msgId"":""45lkj3551234***"",""time"":1626197189638,""data"":{""color"":""green"",""brightness"":50}}";
            PropertySetRequest result = (PropertySetRequest)JsonUtils.Deserialize(json, typeof(PropertySetRequest));
            Assert.AreEqual("45lkj3551234***", result.MessageId);
            Assert.AreEqual(1626197189638, result.Time);
            Assert.AreEqual(2, result.Data.Count);
            Assert.AreEqual("green", result.Data["color"]);
            Assert.AreEqual(50, result.Data["brightness"]);
        }

        [TestMethod]
        public void ActionExecuteRequest_Deserialize()
        {
            string json = @"{""msgId"":""45lkj3551234***"",""time"":1626197189638,""data"":{""actionCode"":""testAction"",""inputParams"":{""inputParam1"":""value1"",""inputParam2"":""value2"",""inputParam3"":1}}}";
            ActionExecuteRequest result = (ActionExecuteRequest)JsonUtils.Deserialize(json, typeof(ActionExecuteRequest));
            Assert.AreEqual("45lkj3551234***", result.MessageId);
            Assert.AreEqual(1626197189638, result.Time);
            Assert.AreEqual("testAction", result.Data.ActionCode);
            Assert.AreEqual(3, result.Data.InputParams.Count);
            Assert.AreEqual("value1", result.Data.InputParams["inputParam1"]);
            Assert.AreEqual("value2", result.Data.InputParams["inputParam2"]);
            Assert.AreEqual(1, result.Data.InputParams["inputParam3"]);
        }

        [TestMethod]
        public void ExecuteActionResponse_Serialize()
        {
            string json = @"{""msgId"":""45lkj3551234***"",""time"":1626197189640,""code"":0,""data"":{""actionCode"":""testAction"",""outputParams"":{""outputParam1"":""value1"",""outputParam2"":""value2""}}}";
            ActionExecuteResponse response = new()
            {
                MessageId = "45lkj3551234***",
                Time = 1626197189640,
                Code = StatusCode.InvalidParameter,
                Data = new OutputActionData()
                {
                    ActionCode = "testAction",
                    OutputParams = new(2)
                    {
                        ["outputParam1"] = "value1",
                        ["outputParam2"] = "value2"
                    }
                }
            };
            string resultJson = JsonUtils.Serialize(response);
            Assert.IsTrue(json.Contains(@"""msgId"":""45lkj3551234***"""));
            ActionExecuteResponse result = (ActionExecuteResponse)JsonUtils.Deserialize(resultJson, response.GetType());
            Assert.AreEqual(response.MessageId, result.MessageId);
            Assert.AreEqual(response.Time, result.Time);
            Assert.AreEqual(response.Code, result.Code);
            Assert.AreEqual(response.Data.ActionCode, result.Data.ActionCode);
            Assert.AreEqual(response.Data.OutputParams.Count, result.Data.OutputParams.Count);
            Assert.AreEqual(response.Data.OutputParams["outputParam1"], result.Data.OutputParams["outputParam1"]);
            Assert.AreEqual(response.Data.OutputParams["outputParam2"], result.Data.OutputParams["outputParam2"]);
        }

        [TestMethod]
        public void TriggerEventRequest_Serialize()
        {
            TriggerEventRequest request = new()
            {
                MessageId = "45lkj3551234***",
                Time = 1626197189638,
                Data = new TriggerEventData()
                {
                    EventCode = "testEvent",
                    EventTime = 1626197189631,
                    OutputParams = new(2)
                    {
                        ["outputParam1"] = "value1",
                        ["outputParam2"] = "value2"
                    }
                }
            };

            string json = JsonUtils.Serialize(request);
            Assert.IsTrue(json.Contains(@"""msgId"":""45lkj3551234***"""));
            Assert.IsTrue(json.Contains(@"""time"":1626197189638"));
            Assert.IsTrue(json.Contains(@"""eventCode"":""testEvent"""));
            Assert.IsTrue(json.Contains(@"""eventTime"":1626197189631"));
            Assert.IsTrue(json.Contains(@"""outputParams"":"));
            Assert.IsTrue(json.Contains(@"""outputParam1"":""value1"""));
            Assert.IsTrue(json.Contains(@"""outputParam2"":""value2"""));
        }

        [TestMethod]
        public void BatchReport_Serialize()
        {
            string json = @"{""msgId"":""45lkj3551234***"",""time"":1626197189638,""data"":{""properties"":{""color"":{""value"":""red"",""time"":1626197189638}},""events"":{""event1"":{""outputParams"":{""outputParam1"":""value1"",""outputParam2"":""value2""},""eventTime"":1626197189001},""event2"":{""outputParams"":{""outputParam1"":""value1"",""outputParam2"":""value2""},""eventTime"":1626197189001}},""subDevices"":[{""deviceId"":""asd453452***"",""properties"":{""color"":{""value"":""red"",""time"":1626197189638},""brightness"":{""value"":80,""time"":1626197189638}},""events"":{""event1"":{""outputParams"":{""outputParam1"":""value1"",""outputParam2"":""value2""},""eventTime"":1626197189001}}}]}}";
            BatchReportRequest request = new()
            {
                MessageId = "45lkj3551234***",
                Time = 1626197189638,
                Data = new BatchReportRequestData()
                {
                    Properties = new PropertyHashtable()
                    {
                        ["Color"] = new PropertyValue()
                        {
                            Value = "red",
                            Time = 1626197189638
                        }
                    },
                    Events = new BatchEventDataHashtable()
                    {
                        ["event1"] = new EventData()
                        {
                            OutputParams = new(2)
                            {
                                ["outputParam1"] = "value1",
                                ["outputParam2"] = "value2"
                            },
                            EventTime = 1626197189001
                        },
                        ["event2"] = new EventData()
                        {
                            OutputParams = new(2)
                            {
                                ["outputParam1"] = "value1",
                                ["outputParam2"] = "value2"
                            },
                            EventTime = 1626197189001
                        }
                    }
                }
            };
            string resultJson = JsonUtils.Serialize(request);
            Assert.IsTrue(json.Contains(@"""msgId"":""45lkj3551234***"""));
            BatchReportRequest result = (BatchReportRequest)JsonUtils.Deserialize(resultJson, request.GetType());
            Assert.AreEqual(request.MessageId, result.MessageId);
            Assert.AreEqual(request.Time, result.Time);
            Assert.AreEqual(request.Data.Properties.Count, result.Data.Properties.Count);
            Assert.AreEqual(request.Data.Properties["Color"].Value, result.Data.Properties["Color"].Value);
            Assert.AreEqual(request.Data.Properties["Color"].Time, result.Data.Properties["Color"].Time);
            Assert.AreEqual(request.Data.Events.Count, result.Data.Events.Count);
            Assert.AreEqual(request.Data.Events["event1"].OutputParams.Count, result.Data.Events["event1"].OutputParams.Count);
            Assert.AreEqual(request.Data.Events["event1"].OutputParams["outputParam1"], result.Data.Events["event1"].OutputParams["outputParam1"]);
            Assert.AreEqual(request.Data.Events["event1"].OutputParams["outputParam2"], result.Data.Events["event1"].OutputParams["outputParam2"]);
            Assert.AreEqual(request.Data.Events["event1"].EventTime, result.Data.Events["event1"].EventTime);
            Assert.AreEqual(request.Data.Events["event2"].OutputParams.Count, result.Data.Events["event2"].OutputParams.Count);
            Assert.AreEqual(request.Data.Events["event2"].OutputParams["outputParam1"], result.Data.Events["event2"].OutputParams["outputParam1"]);
            Assert.AreEqual(request.Data.Events["event2"].OutputParams["outputParam2"], result.Data.Events["event2"].OutputParams["outputParam2"]);
            Assert.AreEqual(request.Data.Events["event2"].EventTime, result.Data.Events["event2"].EventTime);
        }

    }
}
