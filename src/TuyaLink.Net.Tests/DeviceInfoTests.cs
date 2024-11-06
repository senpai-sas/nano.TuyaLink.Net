using nanoFramework.TestFramework;

using TuyaLink;

[TestClass]
public class DeviceInfoTests
{
    [TestMethod]
    public void ToString_ShouldReturnCorrectFormat()
    {
        // Arrange
        var deviceInfo = new DeviceInfo("product123", "device123", "secret123");

        // Act
        var result = deviceInfo.ToString();

        // Assert
        Assert.AreEqual("ProductId: product123, DeviceId: device123", result);
    }

    [TestMethod]
    public void Validate_ShouldNotThrowException_WhenAllPropertiesAreValid()
    {
        // Arrange
        var deviceInfo = new DeviceInfo("product123", "device123", "secret123");

        // Act & Assert
        deviceInfo.Validate();
    }

    [TestMethod]
    public void Validate_ShouldThrowException_WhenProductIdIsNullOrEmpty()
    {
        // Arrange
        var deviceInfo = new DeviceInfo(null, "device123", "secret123");

        // Act & Assert
        Assert.ThrowsException(typeof(System.ArgumentException), () => deviceInfo.Validate());
    }

    [TestMethod]
    public void Validate_ShouldThrowException_WhenDeviceIdIsNullOrEmpty()
    {
        // Arrange
        var deviceInfo = new DeviceInfo("product123", null, "secret123");

        // Act & Assert
        Assert.ThrowsException(typeof(System.ArgumentException), () => deviceInfo.Validate());
    }

    [TestMethod]
    public void Validate_ShouldThrowException_WhenDeviceSecretIsNullOrEmpty()
    {
        // Arrange
        var deviceInfo = new DeviceInfo("product123", "device123", null);

        // Act & Assert
        Assert.ThrowsException(typeof(System.ArgumentException), () => deviceInfo.Validate());
    }
}
