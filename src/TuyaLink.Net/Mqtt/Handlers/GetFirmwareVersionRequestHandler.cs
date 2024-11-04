using System.Diagnostics;

using TuyaLink.Communication;
using TuyaLink.Communication.Firmware;

namespace TuyaLink.Mqtt.Handlers
{
    internal class GetFirmwareVersionRequestHandler : MqttDeviceRequestHandler
    {
        public GetFirmwareVersionRequestHandler(MqttCommunicationProtocol communication, ResponseHandler responseHandler) : base(communication, responseHandler)
        {
        }

        public override void HandleMessage(FunctionMessage message)
        {
            GetFirmwareVersionResponse response = (GetFirmwareVersionResponse)message;

            if (!response.Code.IsSuccess)
            {
                Debug.WriteLine($"Get firmware response error: {response.Code}");
                AcknowledgeResponse(response);
                return;
            }

            Communication.IssueFirmware(response.Data);

            AcknowledgeResponse(response);

        }
    }
}
