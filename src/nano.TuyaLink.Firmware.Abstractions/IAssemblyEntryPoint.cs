namespace TuyaLink.Firmware
{
    public interface IAssemblyEntryPoint
    {
        public void Run(string [] args);

        public void Stop();

        public void FirmwareUpdated(FirmwareMetadata metadata);
    }
}
