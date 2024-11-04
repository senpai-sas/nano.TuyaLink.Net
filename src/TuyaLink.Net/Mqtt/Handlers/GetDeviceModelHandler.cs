using System.Diagnostics;

using TuyaLink.Communication;
using TuyaLink.Communication.Model;

namespace TuyaLink.Mqtt.Handlers
{
    internal class GetDeviceModelHandler(MqttCommunicationProtocol communication, ResponseHandler responseHandler) : DefaultRequestHandler(communication, responseHandler)
    {
        public override void HandleMessage(FunctionMessage message)
        {
            if (message is not GetDeviceModelResponse response)
            {
                throw new TuyaMqttException($"Received unexpected message type {message.GetType().Name}");
            }
            Debug.WriteLine("Device model response");
            if (response.Code.IsSuccess)
            {
                Communication.UpdateModel(response.Data);
            }
            else
            {
                Debug.WriteLine($"Failed to get device model: {response.Code}");
            }
            Communication.UpdateModel(response.Data);
            AcknowledgeResponse(response);
        }
    }
}
