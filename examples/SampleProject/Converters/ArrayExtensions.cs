using System;
using System.Collections;
using System.Text;

namespace AutomaticPetFeeder.Converters
{
    public static class ArrayExtensions
    {
        public static void Reverse(this byte[] array)
        {
            for (int i = 0; i < array.Length / 2; i++)
            {
                byte tmp = array[i];
                array[i] = array[array.Length - i - 1];
                array[array.Length - i - 1] = tmp;
            }

        }
    }
}
