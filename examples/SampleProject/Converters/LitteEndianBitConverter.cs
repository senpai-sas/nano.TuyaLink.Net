using System.Text;

using AutomaticPetFeeder.Converters;

namespace System
{

    public partial class EndianBitConverter
    {
        internal class LittleEndianBitConverter : EndianBitConverter
        {
            public override byte[] GetBytes(bool value)
            {
                byte[] bytes = BitConverter.GetBytes(value);

                if (BitConverter.IsLittleEndian)
                {
                    return bytes;
                }

                bytes.Reverse();
                return bytes;
            }

            public override byte[] GetBytes(char value)
            {
                byte[] bytes = BitConverter.GetBytes(value);

                if (BitConverter.IsLittleEndian)
                {
                    return bytes;
                }

                bytes.Reverse();
                return bytes;
            }

            public override byte[] GetBytes(short value)
            {
                byte[] bytes = BitConverter.GetBytes(value);

                if (BitConverter.IsLittleEndian)
                {
                    return bytes;
                }

                bytes.Reverse();
                return bytes;
            }

            public override byte[] GetBytes(ushort value)
            {
                byte[] bytes = BitConverter.GetBytes(value);

                if (BitConverter.IsLittleEndian)
                {
                    return bytes;
                }

                bytes.Reverse();
                return bytes;
            }

            public override byte[] GetBytes(int value)
            {
                byte[] bytes = BitConverter.GetBytes(value);

                if (BitConverter.IsLittleEndian)
                {
                    return bytes;
                }

                bytes.Reverse();
                return bytes;
            }

            public override byte[] GetBytes(uint value)
            {
                byte[] bytes = BitConverter.GetBytes(value);

                if (BitConverter.IsLittleEndian)
                {
                    return bytes;
                }

                bytes.Reverse();
                return bytes;
            }

            public override byte[] GetBytes(long value)
            {
                byte[] bytes = BitConverter.GetBytes(value);

                if (BitConverter.IsLittleEndian)
                {
                    return bytes;
                }

                bytes.Reverse();
                return bytes;
            }

            public override byte[] GetBytes(ulong value)
            {
                byte[] bytes = BitConverter.GetBytes(value);

                if (BitConverter.IsLittleEndian)
                {
                    return bytes;
                }

                bytes.Reverse();
                return bytes;
            }

            public override byte[] GetBytes(float value)
            {
                byte[] bytes = BitConverter.GetBytes(value);

                if (BitConverter.IsLittleEndian)
                {
                    return bytes;
                }

                bytes.Reverse();
                return bytes;
            }

            public override byte[] GetBytes(double value)
            {
                byte[] bytes = BitConverter.GetBytes(value);

                if (BitConverter.IsLittleEndian)
                {
                    return bytes;
                }

                bytes.Reverse();
                return bytes;
            }


            public override byte[] GetUTF8Bytes(string value)
            {
                return Encoding.UTF8.GetBytes(value);
            }

            public override bool ToBoolean(byte[] bytes, int startIndex = 0)
            {
                // Boolean is represented using 1 byte.
                return BitConverter.ToBoolean(bytes, startIndex);
            }

            public override char ToChar(byte[] bytes, int startIndex = 0)
            {
                if (BitConverter.IsLittleEndian)
                {
                    return BitConverter.ToChar(bytes, startIndex);
                }

                byte[] buf = Reverse(bytes, startIndex, 2);
                return BitConverter.ToChar(buf, 0);
            }


            public override double ToDouble(byte[] bytes, int startIndex = 0)
            {
                if (BitConverter.IsLittleEndian)
                {
                    return BitConverter.ToDouble(bytes, startIndex);
                }

                byte[] buf = Reverse(bytes, startIndex, 8);
                return BitConverter.ToDouble(buf, 0);
            }

            public override short ToInt16(byte[] bytes, int startIndex = 0)
            {
                if (BitConverter.IsLittleEndian)
                {
                    return BitConverter.ToInt16(bytes, startIndex);
                }

                byte[] buf = Reverse(bytes, startIndex, 2);
                return BitConverter.ToInt16(buf, 0);
            }

            public override int ToInt32(byte[] bytes, int startIndex = 0)
            {
                if (BitConverter.IsLittleEndian)
                {
                    return BitConverter.ToInt32(bytes, startIndex);
                }

                byte[] buf = Reverse(bytes, startIndex, 4);
                return BitConverter.ToInt32(buf, 0);
            }

            public override long ToInt64(byte[] bytes, int startIndex = 0)
            {
                if (BitConverter.IsLittleEndian)
                {
                    return BitConverter.ToInt64(bytes, startIndex);
                }

                byte[] buf = Reverse(bytes, startIndex, 8);
                return BitConverter.ToInt64(buf, 0);
            }

            public override float ToSingle(byte[] bytes, int startIndex = 0)
            {
                if (BitConverter.IsLittleEndian)
                {
                    return BitConverter.ToSingle(bytes, startIndex);
                }

                byte[] buf = Reverse(bytes, startIndex, 4);
                return BitConverter.ToSingle(buf, 0);
            }

            public override string ToString(byte[] bytes, int startIndex, int count)
            {
                return Encoding.UTF8.GetString(bytes, startIndex, count);
            }

            public override ushort ToUInt16(byte[] bytes, int startIndex = 0)
            {
                if (BitConverter.IsLittleEndian)
                {
                    return BitConverter.ToUInt16(bytes, startIndex);
                }

                byte[] buf = Reverse(bytes, startIndex, 2);
                return BitConverter.ToUInt16(buf, 0);
            }

            public override uint ToUInt32(byte[] bytes, int startIndex = 0)
            {
                if (BitConverter.IsLittleEndian)
                {
                    return BitConverter.ToUInt32(bytes, startIndex);
                }

                byte[] buf = Reverse(bytes, startIndex, 4);
                return BitConverter.ToUInt32(buf, 0);
            }

            public override ulong ToUInt64(byte[] bytes, int startIndex = 0)
            {
                if (BitConverter.IsLittleEndian)
                {
                    return BitConverter.ToUInt64(bytes, startIndex);
                }

                byte[] buf = Reverse(bytes, startIndex, 8);
                return BitConverter.ToUInt64(buf, 0);
            }
        }

    }
}

