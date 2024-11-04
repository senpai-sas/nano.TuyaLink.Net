namespace TuyaLink.Firmware
{
    public interface ITargetDevice
    {
        void InternetConnect();

        bool HasLowBattery();
    }
}
