
using System;
using System.Security.Cryptography.X509Certificates;

using TuyaLink.Firmware;

namespace TuyaLink
{
    public class DeviceSettings
    {
        private static DeviceSettings _default;

        public X509Certificate2? ClientCertificate { get; set; }

        public bool ValdiateModel { get; set; }

        public bool AutoDeleteDesiredProperties { get; set; }

        public bool BindModel { get; set; } = true;

        public FirmwareManager? FirmwareManager { get; set; }

        public DataCenter DataCenter { get; set; }

        public CommunicationSettings Communication { get; set; }

        public static DeviceSettings Default => _default ??= new DeviceSettings();

        public DeviceSettings()
        {
            DataCenter = DataCenter.China;
            BindModel = true;
            Communication = new CommunicationSettings();
        }

    }

    public class CommunicationSettings
    {
        public CommunicationSettings()
        {
        }

        public TimeSpan KeepAliveTime { get; set; } = TimeSpan.FromMinutes(5);
    }

}
