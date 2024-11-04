
namespace System.Collections
{
    public static class ArrayExtensions
    {
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
                var tmp = array[i];
                array[i] = array[length - i - 1];
                array[length - i - 1] = tmp;
            }
        }

        public static void Reverse(this byte[] array)
        {
            int length = array.Length;
            for (int i = 0; i < length / 2; i++)
            {
                var tmp = array[i];
                array[i] = array[length - i - 1];
                array[length - i - 1] = tmp;
            }
        }

        public static void Reverse(this long[] array)
        {
            int length = array.Length;
            for (int i = 0; i < length / 2; i++)
            {
                var tmp = array[i];
                array[i] = array[length - i - 1];
                array[length - i - 1] = tmp;
            }
        }

        public static void Reverse(this short[] array)
        {
            int length = array.Length;
            for (int i = 0; i < length / 2; i++)
            {
                var tmp = array[i];
                array[i] = array[length - i - 1];
                array[length - i - 1] = tmp;
            }
        }

        public static void Reverse(this double[] array)
        {
            int length = array.Length;
            for (int i = 0; i < length / 2; i++)
            {
                var tmp = array[i];
                array[i] = array[length - i - 1];
                array[length - i - 1] = tmp;
            }
        }

        public static void Reverse(this float[] array)
        {
            int length = array.Length;
            for (int i = 0; i < length / 2; i++)
            {
                var tmp = array[i];
                array[i] = array[length - i - 1];
                array[length - i - 1] = tmp;
            }
        }

        public static void Reverse(this uint[] array)
        {
            int length = array.Length;
            for (int i = 0; i < length / 2; i++)
            {
                var tmp = array[i];
                array[i] = array[length - i - 1];
                array[length - i - 1] = tmp;
            }
        }

        public static void Reverse(this ulong[] array)
        {
            int length = array.Length;
            for (int i = 0; i < length / 2; i++)
            {
                var tmp = array[i];
                array[i] = array[length - i - 1];
                array[length - i - 1] = tmp;
            }
        }

        public static void Reverse(this ushort[] array)
        {
            int length = array.Length;
            for (int i = 0; i < length / 2; i++)
            {
                var tmp = array[i];
                array[i] = array[length - i - 1];
                array[length - i - 1] = tmp;
            }
        }

        public static void Reverse(this sbyte[] array)
        {
            int length = array.Length;
            for (int i = 0; i < length / 2; i++)
            {
                var tmp = array[i];
                array[i] = array[length - i - 1];
                array[length - i - 1] = tmp;
            }
        }
    }
}
