using System.Diagnostics;

using TuyaLink.Communication;
using TuyaLink.Communication.Properties;

namespace TuyaLink.Mqtt.Handlers
{
    internal class PropertyGetRequestHandler(MqttCommunicationProtocol communication, ResponseHandler responseHandler) 
        : MqttDeviceRequestHandler(communication, responseHandler)
    {
        public override void HandleMessage(FunctionMessage message)
        {
            if (message is not GetPropertiesResponse response)
            {
                throw new TuyaMqttException($"Received unexpected message type {message.GetType().Name}");
            }
            Debug.WriteLine("Property get response");
            Communication.UpdateProperties(response.Data.Properties);
            AcknowledgeResponse(response);
        }
    }
}
