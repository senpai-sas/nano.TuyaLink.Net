
using TuyaLink.Firmware;

namespace TuyaLink.Communication.Firmware
{
    internal class ReportUpdateProgressRequest : FunctionRequest
    {
        public ReportUpdateProgressRequestData Data { get; set; }
    }

    public class ReportUpdateProgressRequestData
    {
        public int Channel { get; set; }

        public int Progress { get; set; }

        public FirmwareUdpateError ErrorCode { get; set; } = FirmwareUdpateError.None;

        public string ErrorMsg { get; set; }
    }
}
