
namespace TuyaLink
{
    public class DeviceInfo(string productId, string deviceId, string deviceSecret)
    {
        public string ProductId { get; } = productId;
        public string DeviceId { get; } = deviceId;
        public string DeviceSecret { get; } = deviceSecret;
        public string? Model { get; set; }

        public FirmwareInfo[] Firmwares { get; set; } = [];

        public override string ToString()
        {
            return $"ProductId: {ProductId}, DeviceId: {DeviceId}";
        }

        public void Validate()
        {
            if (string.IsNullOrEmpty(ProductId))
                throw new System.ArgumentException("ProductId is required", nameof(ProductId));
            if (string.IsNullOrEmpty(DeviceId))
                throw new System.ArgumentException("DeviceId is required", nameof(DeviceId));
            if (string.IsNullOrEmpty(DeviceSecret))
                throw new System.ArgumentException("DeviceSecret is required", nameof(DeviceSecret));
        }
    }

    public class FirmwareInfo(int channel, string version)
    {
        public int Channel { get; set; } = channel;

        public string Version { get; set; } = version;
    }
}
