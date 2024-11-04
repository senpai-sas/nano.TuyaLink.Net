using TuyaLink.Firmware;

namespace TuyaLink.Communication.Firmware
{
    internal class FirmwareReportRequest : FunctionRequest
    {
        public FirmwareReportData Data { get; set; }
    }
}
