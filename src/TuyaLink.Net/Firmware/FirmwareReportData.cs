namespace TuyaLink.Firmware
{
    public class FirmwareReportData
    {
        public BizType BizType { get; set; }
        public string Pid { get; set; }
        public string FirmwareKey { get; set; }
        public FirmwareInfo[] OtaChannels { get; set; }
    }
}
