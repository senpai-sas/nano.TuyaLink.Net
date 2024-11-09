

using nano.Asserts;

using nanoFramework.TestFramework;

using System;
using System.Collections;
namespace nano.Collections.Tests
{
    [TestClass]
    public class ArrayExtensionsTests
    {
        [TestMethod]
        public void Reverse_ObjectArray_ShouldReverseArray()
        {
            object[] array = { 1, "two", 3.0, '4' };
            array.Reverse();
            ArrayAssert.AreEqual(array, new object[] { '4', 3.0, "two", 1 });
        }

        [TestMethod]
        public void Reverse_IntArray_ShouldReverseArray()
        {
            int[] array = { 1, 2, 3, 4 };
            array.Reverse();
            ArrayAssert.AreEqual(array, new int[] { 4, 3, 2, 1 });
        }

        [TestMethod]
        public void Reverse_ByteArray_ShouldReverseArray()
        {
            byte[] array = { 1, 2, 3, 4 };
            array.Reverse();
            ArrayAssert.AreEqual(array, new byte[] { 4, 3, 2, 1 });
        }

        [TestMethod]
        public void Reverse_LongArray_ShouldReverseArray()
        {
            long[] array = { 1L, 2L, 3L, 4L };
            array.Reverse();
            ArrayAssert.AreEqual(array, new long[] { 4L, 3L, 2L, 1L });
        }

        [TestMethod]
        public void Reverse_ShortArray_ShouldReverseArray()
        {
            short[] array = { 1, 2, 3, 4 };
            array.Reverse();
            ArrayAssert.AreEqual(array, new short[] { 4, 3, 2, 1 });
        }

        [TestMethod]
        public void Reverse_DoubleArray_ShouldReverseArray()
        {
            double[] array = { 1.1, 2.2, 3.3, 4.4 };
            array.Reverse();
            ArrayAssert.AreEqual(array, new double[] { 4.4, 3.3, 2.2, 1.1 });
        }

        [TestMethod]
        public void Reverse_FloatArray_ShouldReverseArray()
        {
            float[] array = { 1.1f, 2.2f, 3.3f, 4.4f };
            array.Reverse();
            ArrayAssert.AreEqual(array, new float[] { 4.4f, 3.3f, 2.2f, 1.1f });
        }

        [TestMethod]
        public void Reverse_UIntArray_ShouldReverseArray()
        {
            uint[] array = { 1u, 2u, 3u, 4u };
            array.Reverse();
            ArrayAssert.AreEqual(array, new uint[] { 4u, 3u, 2u, 1u });
        }

        [TestMethod]
        public void Reverse_ULongArray_ShouldReverseArray()
        {
            ulong[] array = { 1ul, 2ul, 3ul, 4ul };
            array.Reverse();
            ArrayAssert.AreEqual(array, new ulong[] { 4ul, 3ul, 2ul, 1ul });
        }

        [TestMethod]
        public void Reverse_UShortArray_ShouldReverseArray()
        {
            ushort[] array = { 1, 2, 3, 4 };
            array.Reverse();
            ArrayAssert.AreEqual(array, new ushort[] { 4, 3, 2, 1 });
        }

        [TestMethod]
        public void Reverse_SByteArray_ShouldReverseArray()
        {
            sbyte[] array = { 1, 2, 3, 4 };
            array.Reverse();
            ArrayAssert.AreEqual(array, new sbyte[] { 4, 3, 2, 1 });
        }
        [TestMethod]
        public void Join_ObjectArray_ShouldJoinWithSeparator()
        {
            object[] array = { 1, "two", 3.0, '4' };
            string result = array.Join(", ");
            Assert.AreEqual("1, two, 3, 4", result);
        }

        [TestMethod]
        public void Join_EmptyArray_ShouldReturnEmptyString()
        {
            object[] array = { };
            string result = array.Join(", ");
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void Join_SingleElementArray_ShouldReturnElementAsString()
        {
            object[] array = { 1 };
            string result = array.Join(", ");
            Assert.AreEqual("1", result);
        }

        [TestMethod]
        public void Join_NullSperator_ShouldThrowArgumentNullException()
        {
            object[] array = { 1 };
            Assert.ThrowsException(typeof(ArgumentNullException), () => array.Join(null));
        }

        [TestMethod]
        public void Join_NullArray_ShouldReturnEmptyString()
        {
            object[] array = null;
            string result = array.Join(", ");
            Assert.AreEqual(string.Empty, result);
        }
    }
}
