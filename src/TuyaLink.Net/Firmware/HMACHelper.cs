

using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace TuyaLink.Firmware
{
    internal class HMACHelper
    {
        internal static void CheckIntegrity(string key, string expected, Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            HMACSHA256 hash = new(Encoding.UTF8.GetBytes(key));
            byte[] hashBytes = hash.ComputeHash(bytes);
            string hashString = hashBytes.ToExeString();
            if (hashString != expected)
            {
                throw new FirmwareUpdateException(FirmwareUdpateError.UpdateVersion, "HMAC mismatch");
            }
        }
    }
}
