
namespace TuyaLink.Firmware
{
    public class FirmwarePaths
    {
        public const string Tuya = "I:/tuya";

        public const string Firmware = $"{Tuya}/firmware";

        public const string OTA = $"{Firmware}/ota";

        public const string Transitive = $"{OTA}/transitive";

        public const string Current = $"{Firmware}/current";

        public const string Backup = $"{Firmware}/backup";
    }
}
