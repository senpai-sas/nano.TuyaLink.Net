using System;
using System.Text;

namespace TuyaLink.Communication
{
    public static class GuidExtensions
    {
        /// <summary>
        /// Produce an 32 -character string representation of a GUID. 00000000000000000000000000000000
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static string To32String(this Guid guid)
        {
            var bytes = guid.ToByteArray();
            var sb = new StringBuilder();
            for (var i = 0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
