using TuyaLink.Firmware;

namespace TuyaLink.Communication.Firmware
{
    internal class FirmwareProgressReportRequest : FunctionResponse
    {
        public ProgressReportData Data { get; internal set; }
        public FirmwareProgressReportRequest()
        {
            
        }
        public FirmwareProgressReportRequest(ProgressReportData data)
        {
            Data = data;
        }
    }

    public class ProgressReportData
    {
        public int Progress { get; internal set; }
        public UpdateChannel Channel { get; internal set; }
        public string? ErrorMsg { get; set; }
        public FirmwareUdpateError? ErrorCode { get; set; }

        public ProgressReportData()
        {
            
        }
        public ProgressReportData(int progress, UpdateChannel channel)
        {
            Progress = progress;
            Channel = channel;
        }
    }
}
