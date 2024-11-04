using System;
using System.Diagnostics;

using TuyaLink.Communication;
using TuyaLink.Communication.Properties;
using TuyaLink.Functions;
using static TuyaLink.Functions.FunctionResultCodes;

namespace TuyaLink.Mqtt.Handlers
{
    internal class PropertySetRequestHandler(MqttTopicHandler mqttTopicHandler, MqttCommunicationProtocol communication) 
        : MqttCloudRequestHandler(mqttTopicHandler, communication)
    {
        public MqttTopicHandler MqttTopicHandler { get; } = mqttTopicHandler;

        public override void HandleMessage(FunctionMessage message)
        {
            if (message is not PropertySetRequest request)
            {
                throw new TuyaMqttException($"Received unexpected message type {message.GetType().Name}");
            }

            StatusCode resultCode;
            try
            {
                resultCode = Communication.PropertySet(request);
            }
            catch (FunctionRuntimeException ex)
            {
                Debug.WriteLine($"Runtime exception while handling property set Result Code: {ex.ResultCode} {request}:");
                resultCode = ex.ResultCode;
            }
            catch (Exception ex)
            {
                resultCode = StatusCode.UnknownError;
                Debug.WriteLine($"Error while handling property set {request}:\n\t{ex}\n\t{ex.StackTrace}");
            }

            try
            {
                var response = new FunctionResponse()
                {
                    MessageId = request.MessageId,
                    Time = DateTime.UtcNow.ToUnixTimeSeconds(),
                    Code = resultCode
                };
                Communication.PublishResponse(MqttTopicHandler, response);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error while publishing property set response {request}:\n\t{ex}\n\t{ex.StackTrace}");
            }
        }
    }
}
