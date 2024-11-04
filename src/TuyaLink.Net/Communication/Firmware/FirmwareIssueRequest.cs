
using TuyaLink.Firmware;

namespace TuyaLink.Communication.Firmware
{
    internal class FirmwareIssueRequest : FunctionRequest
    {
        public FirmwareIssueData Data { get; set; }
    }
}
