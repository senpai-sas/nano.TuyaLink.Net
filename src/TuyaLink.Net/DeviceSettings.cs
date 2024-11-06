
using System;
using System.Security.Cryptography.X509Certificates;

using TuyaLink.Firmware;

namespace TuyaLink
{
    public class DeviceSettings
    {
        public X509Certificate2? ClientCertificate { get; set; }

        public bool ValdiateModel { get; set; }

        public bool AutoDeleteDesiredProperties { get; set; }

        public bool BindModel { get; set; }

        public FirmwareManager? FirmwareManager { get; set; }

        public DataCenter DataCenter { get; set; }

        public CommunicationSettings Communication { get; set; } = new CommunicationSettings();

  
        internal readonly static DeviceSettings Default = new ();

        public DeviceSettings()
        {
            DataCenter = DataCenter.China;
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
