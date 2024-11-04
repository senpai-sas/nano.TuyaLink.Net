
using System.Security.Cryptography.X509Certificates;

using TuyaLink.Firmware;

namespace TuyaLink
{
    public class DeviceSettings
    {
        public X509Certificate2? ClientCertificate { get; set; }

        public bool ValdiateModel { get; set; }

        internal static DeviceSettings Default = new();

        public bool BindModel { get; set; }

        public FirmwareManager FirmwareManager { get; set; }

        public DataCenter DataCenter { get; set; } = DataCenter.China;
    }
}
