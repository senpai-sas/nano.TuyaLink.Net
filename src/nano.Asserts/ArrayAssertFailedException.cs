using System;

namespace nano.Asserts
{
    [Serializable]
    internal class ArrayAssertFailedException : Exception
    {
        public ArrayAssertFailedException()
        {
        }

        public ArrayAssertFailedException(string message) : base(message)
        {
        }

        public ArrayAssertFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
