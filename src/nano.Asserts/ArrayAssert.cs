

using System;

namespace nano.Asserts
{
    public static class ArrayAssert
    {
        public static void AreEqual(Array expected, Array actual)
        {
            if (expected == null && actual == null)
            {
                return;
            }
            if (expected == null && actual != null)
            {
                throw new ArrayAssertFailedException($"Expected array {expected} but was {actual}");
            }

            if (expected != null && actual == null)
            {
                throw new ArrayAssertFailedException($"Expected array {expected} but was {actual}");
            }

            if (expected.Length != actual.Length)
            {
                throw new ArrayAssertFailedException($"Expected array length {expected.Length} but was {actual.Length}");
            }

            for (var i = 0; i < expected.Length; i++)
            {
                var expectedValue = expected.GetValue(i);
                var actualValue = actual.GetValue(i);
                if (!expectedValue.Equals(actualValue))
                {
                    throw new ArrayAssertFailedException($"Expected {expectedValue} at index {i} but was {actualValue}");
                }
            }
        }
    }
}
