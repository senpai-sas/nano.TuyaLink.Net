
using System.Text;

namespace System.Collections
{
    public static class ArrayExtensions
    {

        public static bool Contains(this object[] array, object value)
        {
            return Array.IndexOf(array, value) != -1;
        }

        public static string Join(this object[] array, string separator)
        {
            StringBuilder stringBuilder = new();
            for (int i = 0; i < array.Length; i++)
            {
                if (i > 0)
                {
                    stringBuilder.Append(separator);
                }
                stringBuilder.Append(array[i]);
            }
            return stringBuilder.ToString();
        }
        public static void Reverse(this object[] array)
        {
            int length = array.Length;
            for (int i = 0; i < length / 2; i++)
            {
                object tmp = array[i];
                array[i] = array[length - i - 1];
                array[length - i - 1] = tmp;
            }
        }

        public static void Reverse(this int[] array)
        {
            int length = array.Length;
            for (int i = 0; i < length / 2; i++)
            {
                int tmp = array[i];
                array[i] = array[length - i - 1];
                array[length - i - 1] = tmp;
            }
        }

        public static void Reverse(this byte[] array)
        {
            int length = array.Length;
            for (int i = 0; i < length / 2; i++)
            {
                byte tmp = array[i];
                array[i] = array[length - i - 1];
                array[length - i - 1] = tmp;
            }
        }

        public static void Reverse(this long[] array)
        {
            int length = array.Length;
            for (int i = 0; i < length / 2; i++)
            {
                long tmp = array[i];
                array[i] = array[length - i - 1];
                array[length - i - 1] = tmp;
            }
        }

        public static void Reverse(this short[] array)
        {
            int length = array.Length;
            for (int i = 0; i < length / 2; i++)
            {
                short tmp = array[i];
                array[i] = array[length - i - 1];
                array[length - i - 1] = tmp;
            }
        }

        public static void Reverse(this double[] array)
        {
            int length = array.Length;
            for (int i = 0; i < length / 2; i++)
            {
                double tmp = array[i];
                array[i] = array[length - i - 1];
                array[length - i - 1] = tmp;
            }
        }

        public static void Reverse(this float[] array)
        {
            int length = array.Length;
            for (int i = 0; i < length / 2; i++)
            {
                float tmp = array[i];
                array[i] = array[length - i - 1];
                array[length - i - 1] = tmp;
            }
        }

        public static void Reverse(this uint[] array)
        {
            int length = array.Length;
            for (int i = 0; i < length / 2; i++)
            {
                uint tmp = array[i];
                array[i] = array[length - i - 1];
                array[length - i - 1] = tmp;
            }
        }

        public static void Reverse(this ulong[] array)
        {
            int length = array.Length;
            for (int i = 0; i < length / 2; i++)
            {
                ulong tmp = array[i];
                array[i] = array[length - i - 1];
                array[length - i - 1] = tmp;
            }
        }

        public static void Reverse(this ushort[] array)
        {
            int length = array.Length;
            for (int i = 0; i < length / 2; i++)
            {
                ushort tmp = array[i];
                array[i] = array[length - i - 1];
                array[length - i - 1] = tmp;
            }
        }

        public static void Reverse(this sbyte[] array)
        {
            int length = array.Length;
            for (int i = 0; i < length / 2; i++)
            {
                sbyte tmp = array[i];
                array[i] = array[length - i - 1];
                array[length - i - 1] = tmp;
            }
        }
    }
}
