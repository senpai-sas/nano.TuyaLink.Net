using System;
using System.Collections;

using nanoFramework.TestFramework;

using TuyaLink.Communication;
using TuyaLink.Communication.Actions;
using TuyaLink.Communication.Events;
using TuyaLink.Communication.Firmware;
using TuyaLink.Communication.History;
using TuyaLink.Communication.Model;
using TuyaLink.Communication.Properties;
using TuyaLink.Firmware;
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
                MsgId = "45lkj3551234***",
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
            Assert.AreEqual(request.MsgId, result.MsgId);
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
            Assert.AreEqual(msgId, result.MsgId);
            Assert.AreEqual(time, result.Time.ToUnixTimeMilliseconds());
            Assert.AreEqual(expectedCode, result.Code);
        }

        [TestMethod]
        public void PropertySet_Deserialize()
        {
            string json = @"{""msgId"":""45lkj3551234***"",""time"":1626197189638,""data"":{""color"":""green"",""brightness"":50}}";
            PropertySetRequest result = (PropertySetRequest)JsonUtils.Deserialize(json, typeof(PropertySetRequest));
            Assert.AreEqual("45lkj3551234***", result.MsgId);
            Assert.AreEqual(1626197189638, result.Time.ToUnixTimeMilliseconds());
            Assert.AreEqual(2, result.Data.Count);
            Assert.AreEqual("green", result.Data["color"]);
            Assert.AreEqual(50, result.Data["brightness"]);
        }

        [TestMethod]
        public void ActionExecuteRequest_Deserialize()
        {
            string json = @"{""msgId"":""45lkj3551234***"",""time"":1626197189638,""data"":{""actionCode"":""testAction"",""inputParams"":{""inputParam1"":""value1"",""inputParam2"":""value2"",""inputParam3"":1}}}";
            ActionExecuteRequest result = (ActionExecuteRequest)JsonUtils.Deserialize(json, typeof(ActionExecuteRequest));
            Assert.AreEqual("45lkj3551234***", result.MsgId);
            Assert.AreEqual(1626197189638, result.Time.ToUnixTimeMilliseconds());
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
                MsgId = "45lkj3551234***",
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
            Assert.AreEqual(response.MsgId, result.MsgId);
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
                MsgId = "45lkj3551234***",
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
            BatchReportRequest request = new()
            {
                MsgId = "45lkj3551234***",
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
                    Events = new EventDataHashtable()
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
            Console.WriteLine(resultJson);
            BatchReportRequest result = (BatchReportRequest)JsonUtils.Deserialize(resultJson, request.GetType());
            Assert.AreEqual(request.MsgId, result.MsgId);
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

        [TestMethod]
        public void HistoryReportRequest_Serialize()
        {
            var data = new HistoryReportData(
                [
                    new PropertyHashtable()
                    {
                        ["Color"] = new PropertyValue()
                        {
                            Value = "red",
                            Time = 1626197189638
                        }
                    },
                    new PropertyHashtable()
                    {
                        ["Brightness"] = new PropertyValue()
                        {
                            Value = 80,
                            Time = 1626197189638
                        }
                    }
                ],
                [
                    new EventDataHashtable()
                    {
                        ["event1"] = new EventData()
                        {
                            OutputParams = new(2)
                            {
                                ["outputParam1"] = "value1",
                                ["outputParam2"] = "value2"
                            },
                            EventTime = 1626197189001
                        }
                    },
                    new EventDataHashtable()
                    {
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
                ]
            );
            HistoryReportRequest request = new(data)
            {
                MsgId = "45lkj3551234***",
                Time = 1626197189638,
            };
            string resultJson = JsonUtils.Serialize(request);
            Assert.IsTrue(resultJson.Contains($"\"msgId\":\"{request.MsgId}\""));
            HistoryReportRequest historyReportRequest = (HistoryReportRequest)JsonUtils.Deserialize(resultJson, request.GetType());
            Assert.AreEqual(request.MsgId, historyReportRequest.MsgId);
            Assert.AreEqual(request.Time, historyReportRequest.Time);
            Assert.AreEqual(request.Data.Properties.Length, historyReportRequest.Data.Properties.Length);
            Assert.AreEqual(request.Data.Properties[0].Count, historyReportRequest.Data.Properties[0].Count);
            Assert.AreEqual(request.Data.Properties[0]["Color"].Value, historyReportRequest.Data.Properties[0]["Color"].Value);

            Assert.AreEqual(request.Data.Properties[0]["Color"].Time, historyReportRequest.Data.Properties[0]["Color"].Time);
            Assert.AreEqual(request.Data.Properties[1].Count, historyReportRequest.Data.Properties[1].Count);
            Assert.AreEqual(request.Data.Properties[1]["Brightness"].Value, historyReportRequest.Data.Properties[1]["Brightness"].Value);
            Assert.AreEqual(request.Data.Properties[1]["Brightness"].Time, historyReportRequest.Data.Properties[1]["Brightness"].Time);

            Assert.AreEqual(request.Data.Events.Length, historyReportRequest.Data.Events.Length);

            Assert.AreEqual(request.Data.Events[0].Count, historyReportRequest.Data.Events[0].Count);
            Assert.AreEqual(request.Data.Events[0]["event1"].OutputParams.Count, historyReportRequest.Data.Events[0]["event1"].OutputParams.Count);
            Assert.AreEqual(request.Data.Events[0]["event1"].OutputParams["outputParam1"], historyReportRequest.Data.Events[0]["event1"].OutputParams["outputParam1"]);
            Assert.AreEqual(request.Data.Events[0]["event1"].OutputParams["outputParam2"], historyReportRequest.Data.Events[0]["event1"].OutputParams["outputParam2"]);
            Assert.AreEqual(request.Data.Events[0]["event1"].EventTime, historyReportRequest.Data.Events[0]["event1"].EventTime);

            Assert.AreEqual(request.Data.Events[1].Count, historyReportRequest.Data.Events[1].Count);
            Assert.AreEqual(request.Data.Events[1]["event2"].OutputParams.Count, historyReportRequest.Data.Events[1]["event2"].OutputParams.Count);
            Assert.AreEqual(request.Data.Events[1]["event2"].OutputParams["outputParam1"], historyReportRequest.Data.Events[1]["event2"].OutputParams["outputParam1"]);
            Assert.AreEqual(request.Data.Events[1]["event2"].OutputParams["outputParam2"], historyReportRequest.Data.Events[1]["event2"].OutputParams["outputParam2"]);
            Assert.AreEqual(request.Data.Events[1]["event2"].EventTime, historyReportRequest.Data.Events[1]["event2"].EventTime);

        }

        [TestMethod]
        public void GetDeviceModelRequest_Serilize()
        {
            var request = new GetDeviceModelRequest()
            {
                MsgId = "45lkj3551234***",
                Data =
                {
                    Format = DeviceModelDataFormat.Simple,
                },
                Time = 1626197189638
            };

            string resultJson = JsonUtils.Serialize(request);

            var result = (GetDeviceModelRequest)JsonUtils.Deserialize(resultJson, typeof(GetDeviceModelRequest));
            Assert.AreEqual(request.MsgId, result.MsgId);
            Assert.AreEqual(request.Time, result.Time);
            Assert.AreEqual(request.Data.Format, result.Data.Format);
        }

        [TestMethod]
        public void GetDeviceModelRequest_Deserilize()
        {
            string json = @"{
	""msgId"":""45lkj3551234***"",
    ""time"":1626197189640,
	""code"":0,
    ""data"":{
      ""modelId"":""0000001***"",
      ""services"":[
          {
              ""code"":"""",
              ""properties"":[
                  {
                      ""abilityId"":1,
                      ""code"":""foodRemaining"",
                      ""accessMode"":""ro"",
                      ""typeSpec"":{
                          ""type"":""value"",
                          ""min"":0,
                          ""max"":2000,
                          ""step"":1,
                          ""unit"":""g"",
                          ""scale"":0
                      }
                  }
              ],
              ""events"":[
                  {
                      ""abilityId"":101,
                      ""code"":""feedEvent"",
                      ""outputParams"":[
                          {
                              ""code"":""time"",
                              ""typeSpec"":{
                                  ""type"":""date""
                              }
                          },
                          {
                              ""code"":""quantity"",
                              ""typeSpec"":{
                                  ""type"":""value"",
                                  ""min"":0,
                                  ""max"":2000,
                                  ""step"":1,
                                  ""unit"":""g"",
                        		  ""scale"":0
                              }
                          }
                      ]
                  }
              ],
              ""actions"":[
                  {
                     ""abilityId"":101,
                      ""code"":""feed"",
                  }
              ]
          }
      ]
	}
}";
            var result = (GetDeviceModelResponse)JsonUtils.Deserialize(json, typeof(GetDeviceModelResponse));
            Assert.AreEqual("45lkj3551234***", result.MsgId);
            Assert.AreEqual(1626197189640, result.Time.ToUnixTimeMilliseconds());
            Assert.AreEqual(0, result.Code.EnumValue);
            Assert.AreEqual("0000001***", result.Data.ModelId);
            Assert.AreEqual(1, result.Data.Services.Length);
            Assert.AreEqual("", result.Data.Services[0].Code);
            Assert.AreEqual(1, result.Data.Services[0].Properties.Length);
            Assert.AreEqual(1, result.Data.Services[0].Properties[0].AbilityId);
            Assert.AreEqual("foodRemaining", result.Data.Services[0].Properties[0].Code);
            Assert.AreEqual("ro", result.Data.Services[0].Properties[0].AccessMode.EnumValue);
            Assert.AreEqual("value", result.Data.Services[0].Properties[0].TypeSpec.Type.EnumValue);
            Assert.AreEqual(0, result.Data.Services[0].Properties[0].TypeSpec.Min);
            Assert.AreEqual(2000, result.Data.Services[0].Properties[0].TypeSpec.Max);
            Assert.AreEqual(1, result.Data.Services[0].Properties[0].TypeSpec.Step);
            Assert.AreEqual("g", result.Data.Services[0].Properties[0].TypeSpec.Unit);
            Assert.AreEqual(0, result.Data.Services[0].Properties[0].TypeSpec.Scale);
            Assert.AreEqual(1, result.Data.Services[0].Events.Length);
            Assert.AreEqual(101, result.Data.Services[0].Events[0].AbilityId);
            Assert.AreEqual("feedEvent", result.Data.Services[0].Events[0].Code);
            Assert.AreEqual(2, result.Data.Services[0].Events[0].OutputParams.Length);
            Assert.AreEqual("time", result.Data.Services[0].Events[0].OutputParams[0].Code);
            Assert.AreEqual("date", result.Data.Services[0].Events[0].OutputParams[0].TypeSpec.Type.EnumValue);
            Assert.AreEqual("quantity", result.Data.Services[0].Events[0].OutputParams[1].Code);
            Assert.AreEqual("value", result.Data.Services[0].Events[0].OutputParams[1].TypeSpec.Type.EnumValue);
            Assert.AreEqual(0, result.Data.Services[0].Events[0].OutputParams[1].TypeSpec.Min);
            Assert.AreEqual(2000, result.Data.Services[0].Events[0].OutputParams[1].TypeSpec.Max);
            Assert.AreEqual(1, result.Data.Services[0].Events[0].OutputParams[1].TypeSpec.Step);
            Assert.AreEqual("g", result.Data.Services[0].Events[0].OutputParams[1].TypeSpec.Unit);
            Assert.AreEqual(0, result.Data.Services[0].Events[0].OutputParams[1].TypeSpec.Scale);
            Assert.AreEqual(1, result.Data.Services[0].Actions.Length);
            Assert.AreEqual(101, result.Data.Services[0].Actions[0].AbilityId);
            Assert.AreEqual("feed", result.Data.Services[0].Actions[0].Code);
        }

        [TestMethod]
        public void GetDesiredPropertyRequest_Serialize()
        {
            var request = new GetPropertiesRequest()
            {
                MsgId = "45lkj3551234***",
                Time = 1626197189638,
                Data = new GetPropertiesRequestData()
                {
                    Properties = ["color", "brightness"]
                }
            };

            string resultJson = JsonUtils.Serialize(request);
            var result = (GetPropertiesRequest)JsonUtils.Deserialize(resultJson, request.GetType());
            Assert.AreEqual(request.MsgId, result.MsgId);
            Assert.AreEqual(request.Time, result.Time);
            Assert.AreEqual(request.Data.Properties.Length, result.Data.Properties.Length);
            Assert.AreEqual(request.Data.Properties[0], result.Data.Properties[0]);
            Assert.AreEqual(request.Data.Properties[1], result.Data.Properties[1]);
        }

        [TestMethod]
        public void GetDesiredPropertyResponse_Deserialize()
        {
            string json = @"{
    ""msgId"":""45lkj3551234***"",
    ""time"":1626197189640,
    ""code"":0,
    ""data"":{
        ""properties"":{
            ""color"":{
                ""version"":""1"",
                ""value"":""red""
            },
            ""brightness"":{
                ""version"":""1"",
                ""value"":80
            }
        }
   }
}";
            var result = (GetPropertiesResponse)JsonUtils.Deserialize(json, typeof(GetPropertiesResponse));
            Assert.AreEqual("45lkj3551234***", result.MsgId);
            Assert.AreEqual(1626197189640, result.Time.ToUnixTimeMilliseconds());
            Assert.AreEqual(StatusCode.FromValue(0), result.Code);
            Assert.AreEqual(2, result.Data.Properties.Count);
            Assert.AreEqual("1", result.Data.Properties["color"].Version);
            Assert.AreEqual("red", result.Data.Properties["color"].Value);
            Assert.AreEqual("1", result.Data.Properties["brightness"].Version);
            Assert.AreEqual(80, result.Data.Properties["brightness"].Value);
        }

        [TestMethod]
        public void DeleteDesiredPropertyRequets_Serialize()
        {
            var requets = new DeleteDesiredPropertyRequest()
            {
                MsgId = "45lkj3551234***",
                Time = 1626197189638,
                Data = new DeleteDesiredPropertyData()
                {
                    Properties =
                    {
                        ["color"] = new DeleteDesiredProperty()
                        {
                            Version = "1"
                        },
                        ["brightness"] = new DeleteDesiredProperty()
                        {
                            Version = "1"
                        }
                    }
                }
            };

            string resultJson = JsonUtils.Serialize(requets);
            var result = (DeleteDesiredPropertyRequest)JsonUtils.Deserialize(resultJson, requets.GetType());
            Assert.AreEqual(requets.MsgId, result.MsgId);
            Assert.AreEqual(requets.Time, result.Time);
            Assert.AreEqual(requets.Data.Properties.Count, result.Data.Properties.Count);
            Assert.AreEqual(requets.Data.Properties["color"].Version, result.Data.Properties["color"].Version);
            Assert.AreEqual(requets.Data.Properties["brightness"].Version, result.Data.Properties["brightness"].Version);
        }

        [TestMethod]
        public void FirmwareReportRequest_Serialize()
        {
            var request = new FirmwareReportRequest()
            {
                MsgId = "45lkj3551234***",
                Time = 1626197189638,
                Data = new FirmwareReportData()
                {
                    BizType = BizType.Initial,
                    FirmwareKey = "firmwareKey",
                    Pid = "123456",
                    OtaChannels = [new FirmwareInfo() {
                        Channel = UpdateChannel.Main,
                        Version =  "1.0.0",
                    }]
                }
            };

            string resultJson = JsonUtils.Serialize(request);
            var result = (FirmwareReportRequest)JsonUtils.Deserialize(resultJson, request.GetType());
            Assert.AreEqual(request.MsgId, result.MsgId);
            Assert.AreEqual(request.Time, result.Time);
            Assert.AreEqual(request.Data.BizType, result.Data.BizType);
            Assert.AreEqual(request.Data.FirmwareKey, result.Data.FirmwareKey);
            Assert.AreEqual(request.Data.Pid, result.Data.Pid);
            Assert.AreEqual(request.Data.OtaChannels.Length, result.Data.OtaChannels.Length);
            Assert.AreEqual(request.Data.OtaChannels[0].Channel, result.Data.OtaChannels[0].Channel);
            Assert.AreEqual(request.Data.OtaChannels[0].Version, result.Data.OtaChannels[0].Version);


        }

        [TestMethod]
        public void FirmwareOtaIssueRequest_Deserialize()
        {
            string json = @"{
    ""msgId"":""45lkj3551234***"",
  	""time"":1626197189638,
	""data"":{
        ""ctId"":""bee8d56c2868302ec4d094d652325***"",
        ""channel"":0,
        ""version"":""1.3.13"",
        ""url"":""http://airtake-public-data-1254153901.cos.tuyacn.com/smart/firmware/upgrade/202006/1592465280-tuya_rtl8196e_gw_tar_UG_1.3***.bin"",
        ""cdnUrl"":""https://images.tuyacn.com/smart/firmware/upgrade/202006/1592465280-tuya_rtl8196e_gw_tar_UG_1.3***.bin"",
        ""size"":""1306707"",
        ""md5"":""e7471fe76cecc562f93d57745acac***"",
        ""hmac"":""952C262E4F40D25B2DF2D3923485AE4EBD8B194F77D41BED2B67B1A335022***"",
        ""httpsUrl"":""https://fireware.tuyacn.com:1443/smart/firmware/upgrade/202006/1592465280-tuya_rtl8196e_gw_tar_UG_1.3***.bin"",
        ""upgradeType"":1,
        ""execTime"":0
    }
}
";
            var result = (FirmwareIssueRequest)JsonUtils.Deserialize(json, typeof(FirmwareIssueRequest));

            Assert.AreEqual("45lkj3551234***", result.MsgId);
            Assert.AreEqual(1626197189638, result.Time.ToUnixTimeMilliseconds());
            Assert.AreEqual("bee8d56c2868302ec4d094d652325***", result.Data.CtId);
            Assert.AreEqual(UpdateChannel.Main, result.Data.Channel);
            Assert.AreEqual("1.3.13", result.Data.Version);
            Assert.AreEqual("http://airtake-public-data-1254153901.cos.tuyacn.com/smart/firmware/upgrade/202006/1592465280-tuya_rtl8196e_gw_tar_UG_1.3***.bin", result.Data.Url);
            Assert.AreEqual("https://images.tuyacn.com/smart/firmware/upgrade/202006/1592465280-tuya_rtl8196e_gw_tar_UG_1.3***.bin", result.Data.CdnUrl);
            Assert.AreEqual("1306707", result.Data.Size);
            Assert.AreEqual("e7471fe76cecc562f93d57745acac***", result.Data.Md5);
            Assert.AreEqual("952C262E4F40D25B2DF2D3923485AE4EBD8B194F77D41BED2B67B1A335022***", result.Data.Hmac);
            Assert.AreEqual("https://fireware.tuyacn.com:1443/smart/firmware/upgrade/202006/1592465280-tuya_rtl8196e_gw_tar_UG_1.3***.bin", result.Data.HttpsUrl);
            Assert.AreEqual(1, result.Data.UpgradeType);
            Assert.AreEqual(0, result.Data.ExecTime);
        }

        [TestMethod]
        public void GetFirmwareVersionResponse_Deserialize()
        {
            var json = @"{
    ""msgId"":""45lkj3551234***"",
    ""time"":1626197189638,
    ""data"":{
        ""url"":""http://airtake-public-data-1254153901.cos.tuyacn.com/smart/firmware/upgrade/202006/1592465280-tuya_rtl8196e_gw_tar_UG_1.3***.bin"",
        ""cdnUrl"":""https://images.tuyacn.com/smart/firmware/upgrade/202006/1592465280-tuya_rtl8196e_gw_tar_UG_1.3***.bin"",
        ""size"":""1306707"",
        ""md5"":""e7471fe76cecc562f93d57745acac***"",
        ""hmac"":""952C262E4F40D25B2DF2D3923485AE4EBD8B194F77D41BED2B67B1A335022***"",
        ""version"":""1.3.13"",
        ""channel"":9,
        ""httpsUrl"":""https://fireware.tuyacn.com:1443/smart/firmware/upgrade/202006/1592465280-tuya_rtl8196e_gw_tar_UG_1.3***.bin"",
        ""upgradeType"":1,
        ""execTime"":0
    }
}";
            var result = (GetFirmwareVersionResponse)JsonUtils.Deserialize(json, typeof(GetFirmwareVersionResponse));
            Assert.AreEqual("45lkj3551234***", result.MsgId);
            Assert.AreEqual(1626197189638, result.Time.ToUnixTimeMilliseconds());
            Assert.AreEqual("http://airtake-public-data-1254153901.cos.tuyacn.com/smart/firmware/upgrade/202006/1592465280-tuya_rtl8196e_gw_tar_UG_1.3***.bin", result.Data.Url);

            Assert.AreEqual("https://images.tuyacn.com/smart/firmware/upgrade/202006/1592465280-tuya_rtl8196e_gw_tar_UG_1.3***.bin", result.Data.CdnUrl);
            Assert.AreEqual("1306707", result.Data.Size);
            Assert.AreEqual("e7471fe76cecc562f93d57745acac***", result.Data.Md5);
            Assert.AreEqual("952C262E4F40D25B2DF2D3923485AE4EBD8B194F77D41BED2B67B1A335022***", result.Data.Hmac);
            Assert.AreEqual("1.3.13", result.Data.Version);
            Assert.AreEqual(UpdateChannel.MCU, result.Data.Channel);
            Assert.AreEqual("https://fireware.tuyacn.com:1443/smart/firmware/upgrade/202006/1592465280-tuya_rtl8196e_gw_tar_UG_1.3***.bin", result.Data.HttpsUrl);
            Assert.AreEqual(1, result.Data.UpgradeType);
            Assert.AreEqual(0, result.Data.ExecTime);
        }
        [TestMethod]
        public void ReportFirmwareProgressRequest_Serialize()
        {
            var data = new ProgressReportData(10, UpdateChannel.Bluetooht)
            {
            };
            var request = new FirmwareProgressReportRequest(data)
            {
                MsgId = "45lkj3551234***",
                Time = 1626197189638,
            };
            var json = JsonUtils.Serialize(request);
            var result = (FirmwareProgressReportRequest)JsonUtils.Deserialize(json, request.GetType());
            Assert.AreEqual(request.MsgId, result.MsgId);
            Assert.AreEqual(request.Time, result.Time);
            Assert.AreEqual(request.Data.Progress, result.Data.Progress);
            Assert.AreEqual(request.Data.Channel, result.Data.Channel);
            Assert.IsNull(request.Data.ErrorMsg);
            Assert.IsNull(request.Data.ErrorCode);
        }

        [TestMethod]
        public void ReportFirmwareProgressRequest_WithError_Serialize()
        {
            var data = new ProgressReportData(10, UpdateChannel.Bluetooht)
            {
                ErrorMsg = "Error message",
                ErrorCode = FirmwareUdpateError.DownloadVerification
            };
            var request = new FirmwareProgressReportRequest(data)
            {
                MsgId = "45lkj3551234***",
                Time = 1626197189638,
            };
            var json = JsonUtils.Serialize(request);
            var result = (FirmwareProgressReportRequest)JsonUtils.Deserialize(json, request.GetType());
            Assert.AreEqual(request.MsgId, result.MsgId);
            Assert.AreEqual(request.Time, result.Time);
            Assert.AreEqual(request.Data.Progress, result.Data.Progress);
            Assert.AreEqual(request.Data.Channel, result.Data.Channel);
            Assert.AreEqual(request.Data.ErrorMsg, result.Data.ErrorMsg);
            Assert.AreEqual(request.Data.ErrorCode, result.Data.ErrorCode);
        }
    }
}
