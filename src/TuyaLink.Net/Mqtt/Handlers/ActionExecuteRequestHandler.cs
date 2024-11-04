using System;
using System.Diagnostics;

using TuyaLink.Communication;
using TuyaLink.Communication.Actions;
using TuyaLink.Functions;
using TuyaLink.Functions.Actions;

namespace TuyaLink.Mqtt.Handlers
{
    internal class ActionExecuteRequestHandler(MqttTopicHandler topicHandler, MqttCommunicationProtocol communication)
        : MqttCloudRequestHandler(topicHandler, communication)
    {
        public override void HandleMessage(FunctionMessage message)
        {
            if (message is not ActionExecuteRequest request)
            {
                throw new TuyaMqttException($"Received unexpected message type {message.GetType().Name}");
            }

            ActionExecuteResult result;
            try
            {
                result = Communication.ActionExecute(request);
            }
            catch (FunctionRuntimeException ex)
            {
                Debug.WriteLine($"Runtime exception while handling action execute Result Code: {ex.ResultCode} {request}:");
                result = ActionExecuteResult.Failure(ex.ResultCode);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error while handling action execute {request}:\n\t{ex}\n\t{ex.StackTrace}");
                result = ActionExecuteResult.Failure(StatusCode.UnknownError);
            }

            ActionExecuteResponse response = new()
            {
                MessageId = request.MessageId,
                Code = result.Code,
                Time = DateTime.UtcNow.ToUnixTimeSeconds(),
                Data = new OutputActionData()
                {
                    ActionCode = request.Data.ActionCode,
                    OutputParams = ((ActionExecuteResult)null).OutputParameters
                }
            };

            try
            {
                Communication.PublishResponse(TopicHandler, response);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error while publishing action execute response {response}:\n\t{ex}\n\t{ex.StackTrace}");
            }
        }

    }
}
