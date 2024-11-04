

using System.IO;
using System.Text;

namespace TuyaLink.Firmware
{
    internal class HMACHelper
    {
        internal static void CheckIntegrity(string key, string expected, Stream stream)
        {
            var bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            var hash = new System.Security.Cryptography.HMACSHA256(Encoding.UTF8.GetBytes(key));
            var hashBytes = hash.ComputeHash(bytes);
            var hashString = hashBytes.ToExeString();
            if (hashString != expected)
            {
                throw new FirmwareUpdateException(FirmwareUdpateError.UpdateVersion, "HMAC mismatch");
            }
        }
    }
}
