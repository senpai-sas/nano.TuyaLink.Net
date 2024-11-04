using TuyaLink.Firmware;

namespace TuyaLink.Communication.Firmware
{
    public class FirmwareUpdateData
    {
        public UpdateChannel Channel { get; set; }
        public string Version { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string CdnUrl { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;    
        public string Md5 { get; set; } = string.Empty;
        public string Hmac { get; set; } = string.Empty;
        public string HttpsUrl { get; set; } = string.Empty;
        public int UpgradeType { get; set; }
        public long ExecTime { get; set; }
    }
}
