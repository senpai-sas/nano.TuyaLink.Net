using System;
using System.Text;

using nanoFramework.TestFramework;

namespace TuyaLink.Functions
{
    [TestClass]
    public class FunctionTypeTests
    {
        [TestMethod]
        public void Property_ShouldHaveCorrectNameAndValue()
        {
            // Arrange & Act
            var property = FunctionType.Property;

            // Assert
            Assert.AreEqual("Property", property.Name);
            Assert.AreEqual(1, property.EnumValue);
        }

        [TestMethod]
        public void Action_ShouldHaveCorrectNameAndValue()
        {
            // Arrange & Act
            var action = FunctionType.Action;

            // Assert
            Assert.AreEqual("Service", action.Name);
            Assert.AreEqual(2, action.EnumValue);
        }

        [TestMethod]
        public void Event_ShouldHaveCorrectNameAndValue()
        {
            // Arrange & Act
            var eventType = FunctionType.Event;

            // Assert
            Assert.AreEqual("Event", eventType.Name);
            Assert.AreEqual(3, eventType.EnumValue);
        }
    }
}
