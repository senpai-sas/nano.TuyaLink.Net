using System.Text;

namespace TuyaLink
{
    internal static class ArrayExtensions
    {
        public static string ToExeString(this byte[] array)
        {
            StringBuilder sb = new(array.Length * 2);
            foreach (var b in array)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
