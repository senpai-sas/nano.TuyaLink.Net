using System;

using nanoFramework.TestFramework;

namespace TuyaLink
{
    [TestClass]
    internal class TuyaDeteTimeTests
    {
        [TestMethod]
        public void Constructor_ShouldSetDateTimeValue()
        {
            // Arrange
            DateTime dateTime = new(2023, 10, 1);

            // Act
            TuyaDateTime tuyaDateTime = new(dateTime);

            // Assert
            Assert.AreEqual(dateTime, tuyaDateTime.Value);
        }

        [TestMethod]
        public void ToString_ShouldReturnFormattedString()
        {
            // Arrange
            DateTime dateTime = new(2023, 10, 1, 12, 30, 45, 123);
            TuyaDateTime tuyaDateTime = new(dateTime);
            string expected = "2023-10-01T12:30:45:123";

            // Act
            string result = tuyaDateTime.ToString();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void FromUnixTime_ShouldReturnCorrectDateTime()
        {
            // Arrange
            long unixTime = 1696157445; // Unix time for 2023-10-01T10:50:45
            DateTime expected = new(2023, 10, 1, 10, 50, 45);

            // Act
            TuyaDateTime tuyaDateTime = TuyaDateTime.FromUnixTime(unixTime);

            // Assert
            Assert.AreEqual(expected, tuyaDateTime.Value);
        }

        [TestMethod]
        public void ToUnixTimeMilliseconds_ShouldReturnCorrectMilliseconds()
        {
            // Arrange
            DateTime dateTime = new(2023, 10, 1, 10, 50, 45, 123);
            TuyaDateTime tuyaDateTime = new(dateTime);
            long expected = 1696157445123;

            // Act
            long result = tuyaDateTime.ToUnixTimeMilliseconds();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CompareTo_ShouldReturnZeroForEqualDateTimes()
        {
            // Arrange
            DateTime dateTime = new(2023, 10, 1);
            TuyaDateTime tuyaDateTime1 = new(dateTime);
            TuyaDateTime tuyaDateTime2 = new(dateTime);

            // Act
            int result = tuyaDateTime1.CompareTo(tuyaDateTime2);

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Equals_ShouldReturnTrueForEqualDateTimes()
        {
            // Arrange
            DateTime dateTime = new(2023, 10, 1);
            TuyaDateTime tuyaDateTime1 = new(dateTime);
            TuyaDateTime tuyaDateTime2 = new(dateTime);

            // Act
            bool result = tuyaDateTime1.Equals(tuyaDateTime2);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetHashCode_ShouldReturnSameHashCodeForEqualDateTimes()
        {
            // Arrange
            DateTime dateTime = new(2023, 10, 1);
            TuyaDateTime tuyaDateTime1 = new(dateTime);
            TuyaDateTime tuyaDateTime2 = new(dateTime);

            // Act
            int hashCode1 = tuyaDateTime1.GetHashCode();
            int hashCode2 = tuyaDateTime2.GetHashCode();

            // Assert
            Assert.AreEqual(hashCode1, hashCode2);
        }
    }
}
