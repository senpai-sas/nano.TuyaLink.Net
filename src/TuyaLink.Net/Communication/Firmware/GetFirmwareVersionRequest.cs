
using TuyaLink.Firmware;

namespace TuyaLink.Communication.Firmware
{
    internal class GetFirmwareVersionRequest : FunctionRequest
    {
        public GetFirmwareVersionData Data { get; set; } = new GetFirmwareVersionData();
    }

    public class GetFirmwareVersionData 
    {
    }
}
