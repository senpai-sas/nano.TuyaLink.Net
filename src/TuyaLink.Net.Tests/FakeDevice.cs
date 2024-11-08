namespace TuyaLink
{
    internal class FakeDevice : TuyaDevice
    {
        public static readonly DeviceInfo DefaultInfo = new("TestProduct", "TestDevice", "secret");

        public static readonly FakeDevice Default = new(DefaultInfo);

        public FakeDevice(DeviceInfo info = null, DeviceSettings settings = null) : base(info ?? DefaultInfo, settings, FakeCommunicationHandler.Default)
        {
        }
    }
}
