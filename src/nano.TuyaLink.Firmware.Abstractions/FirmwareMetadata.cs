namespace TuyaLink.Firmware
{
    public class FirmwareMetadata
    {
        public long TotalSize { get; internal set; }
        public string Version { get; internal set; }
        public string Md5 { get; set; }
        public FirmwareAssemblyMetadata[] Assemblies { get; internal set; }
        public int RunnersCount { get; set; }
    }

    public class FirmwareAssemblyMetadata
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public long Size { get; set; }
        public string Hmac { get; set; }
        public bool Runable { get; set; }
        public string EntryPoint { get; set; }
        public string[] LaunchArgs { get; internal set; }

        public string GenerateKey(string productId)
        {
            return $"tuya_product=${productId};assembly={Name},{Version}";
        }
    }
}
