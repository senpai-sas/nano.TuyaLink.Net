using TuyaLink.Events;

namespace TuyaLink
{
    internal class FakeDevice : TuyaDevice
    {
        public static readonly DeviceInfo DefaultInfo = new("TestProduct", "testDeviceId", "secret");

        private static FakeDevice _default;

        public static FakeDevice Default => _default ??= new(DefaultInfo, DeviceSettings.Default, FakeCommunicationHandler.Default);

        public static FakeDevice ValidateModelDevice => new(DefaultInfo, new DeviceSettings { ValidateModel = true }, FakeCommunicationHandler.Default);

        public FakeDevice(DeviceInfo info = null, DeviceSettings settings = null, FakeCommunicationHandler fakeCommunication = null) : base(info ?? DefaultInfo, settings ?? DeviceSettings.Default, fakeCommunication ?? FakeCommunicationHandler.Default)
        {
            FakeCommunication = fakeCommunication ?? FakeCommunicationHandler.Default;
        }

        public FakeCommunicationHandler FakeCommunication { get; private set; }


        public new DeviceEvent RegisterEvent(string code, Functions.Events.DeviceEventDelegate triggerDelegate)
        {
            return base.RegisterEvent(code, triggerDelegate);
        }

    }
}
