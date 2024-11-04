
using System.IO;

using nanoFramework.Json;

namespace TuyaLink.Firmware
{
    internal class MetadataUtils
    {
        public static FirmwareMetadata FromStream(Stream metadataStream)
        {
            return (FirmwareMetadata)JsonConvert.DeserializeObject(metadataStream, typeof(FirmwareMetadata));
        }
    }
}
