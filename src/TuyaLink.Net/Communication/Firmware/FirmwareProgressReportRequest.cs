using TuyaLink.Firmware;

namespace TuyaLink.Communication.Firmware
{
    internal class FirmwareProgressReportRequest : FunctionResponse
    {
        public ProgressReportData Data { get;  }

        public FirmwareProgressReportRequest(ProgressReportData data)
        {
            Data = data;
        }
    }

    public class ProgressReportData(int progress, UpdateChannel channel)
    {
        public int Progress { get; } = progress;
        public UpdateChannel Channel { get; } = channel;
        public string? ErrorMsg { get; set; }
        public FirmwareUdpateError? ErrorCode { get; set; }
    }
}
