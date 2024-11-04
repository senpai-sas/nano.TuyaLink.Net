using nano.SmartEnum;

namespace TuyaLink.Firmware
{
    internal class UpgradeType : SmartEnum
    {
        private UpgradeType(string name, int value) : base(name, value)
        {
        }

        public static readonly UpgradeType UpdateNotification = new(nameof(UpdateNotification), 0);
        public static readonly UpgradeType SilentUpdate = new(nameof(SilentUpdate), 1);
        public static readonly UpgradeType ForceUpdate = new(nameof(ForceUpdate), 2);
        public static readonly UpgradeType ManualCheckForUpdate = new(nameof(ManualCheckForUpdate), 3);
    }
}
