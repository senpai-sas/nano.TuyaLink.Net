using System.Collections;

using nano.SmartEnum;

namespace TuyaLink.Firmware
{
    public class FirmwareUdpateError : SmartEnum
    {
        private static readonly Hashtable _hashtable = new(12);
        private FirmwareUdpateError(string name, int value) : base(name, value)
        {
            _hashtable[value] = this;
        }

        public static readonly FirmwareUdpateError None = new(nameof(None), 0);

        public static readonly FirmwareUdpateError Unknown = new(nameof(Unknown), 40);

        public static readonly FirmwareUdpateError DownloadLowBattery = new(nameof(DownloadLowBattery), 41);

        public static readonly FirmwareUdpateError DownloadNoSpace = new(nameof(DownloadNoSpace), 42);

        public static readonly FirmwareUdpateError DownloadNoRAM = new(nameof(DownloadNoRAM), 43);

        public static readonly FirmwareUdpateError DownloadTimeout = new(nameof(DownloadTimeout), 44);

        public static readonly FirmwareUdpateError DownloadVerification = new(nameof(DownloadVerification), 45);

        public static readonly FirmwareUdpateError UpdateLowBattery = new(nameof(UpdateLowBattery), 46);

        public static readonly FirmwareUdpateError UpdateNoRAM = new(nameof(UpdateNoRAM), 47);

        public static readonly FirmwareUdpateError UpdateVersion = new(nameof(UpdateVersion), 48);

        public static readonly FirmwareUdpateError HMAC = new(nameof(HMAC), 49);

        public static readonly FirmwareUdpateError GatewayBussy = new(nameof(GatewayBussy), 50);


        public static FirmwareUdpateError FromValue(int value)
        {
            return (FirmwareUdpateError)GetFromValue(value, typeof(FirmwareUdpateError), _hashtable);
        }

    }
}
