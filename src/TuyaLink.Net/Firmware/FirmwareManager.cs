
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;

using nano.System.IO.Compression;

using nanoFramework.Runtime.Native;

using TuyaLink.Communication.Firmware;

namespace TuyaLink.Firmware
{
    public abstract class FirmwareManager
    {
        protected FirmwareManager(ITargetDevice targetDevice, DeviceInfo deviceInfo)
        {
            TargetDevice = targetDevice;
            DeviceInfo = deviceInfo;
        }

        protected TimeSpan DownloadTimeout { get; } = TimeSpan.FromMinutes(2);

        public ITargetDevice TargetDevice { get; }
        public DeviceInfo DeviceInfo { get; }

        public abstract FirmwareReportData GetFirmwareReport();

        public void IssueFirmware(FirmwareUpdateData data, FirmwareUpdateProgressDelegate progressDelegate)
        {

            try
            {
                var fileSize = long.Parse(data.Size);
                var freeRam = nanoFramework.Runtime.Native.GC.Run(true) - (fileSize * 5);

                if (fileSize > freeRam)
                {
                    throw new FirmwareUpdateException(FirmwareUdpateError.DownloadNoRAM);
                }

                if (TargetDevice.HasLowBattery())
                {
                    throw new FirmwareUpdateException(FirmwareUdpateError.DownloadLowBattery);
                }
                if (data.Channel == UpdateChannel.MCU)
                {
                    UpdateApplication(data, progressDelegate);
                }


            }
            catch (FirmwareUpdateException ex)
            {
                Debug.WriteLine("Error updating firmware: " + ex.Message);
                progressDelegate?.Invoke(new ProgressReportData(5, data.Channel)
                {
                    ErrorMsg = ex.Message,
                    ErrorCode = ex.Error
                });
            }
            catch (HttpRequestException ex)
            {
                progressDelegate?.Invoke(new ProgressReportData(0, data.Channel)
                {
                    ErrorMsg = ex.Message,
                    ErrorCode = FirmwareUdpateError.DownloadTimeout
                });
            }
            catch (IOException ex)
            {
                progressDelegate?.Invoke(new ProgressReportData(0, data.Channel)
                {
                    ErrorMsg = ex.Message,
                    ErrorCode = FirmwareUdpateError.Unknown
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error updating firmware: " + ex.Message);
                progressDelegate?.Invoke(new ProgressReportData(0, data.Channel) { ErrorMsg = ex.Message });
            }
        }

        private void UpdateApplication(FirmwareUpdateData data, FirmwareUpdateProgressDelegate progressDelegate)
        {
            var tuyaPath = FirmwarePaths.Tuya;
            var firmwareFilePath = Path.Combine(tuyaPath, $"OTA/update/channel-{data.Channel}/{data.Version}.zip");
            if (File.Exists(firmwareFilePath))
            {
                File.Delete(firmwareFilePath);
            }

            progressDelegate?.Invoke(new ProgressReportData(5, data.Channel));
            using var firmwareFile = File.Create(firmwareFilePath);

            using var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + data.Hmac);
            client.Timeout = DownloadTimeout;
            using var firmwareStream = client.GetStream(data.HttpsUrl);


            progressDelegate?.Invoke(new ProgressReportData(5, data.Channel));
            if (TargetDevice.HasLowBattery())
            {
                throw new FirmwareUpdateException(FirmwareUdpateError.UpdateLowBattery);
            }
            firmwareStream.CopyTo(firmwareFile);
            firmwareFile.Flush();

            progressDelegate?.Invoke(new ProgressReportData(5, data.Channel));
            using var zipArchive = new ZipFile(firmwareFile);

            var metadataEntry = zipArchive.GetEntry("metadata.json");
            if (metadataEntry == null)
            {
                throw new FirmwareUpdateException(FirmwareUdpateError.UpdateVersion, "Not valid firmware file");
            }
            var updateMetadataStrem = zipArchive.GetInputStream(metadataEntry);
            var firmwareMetadata = MetadataUtils.FromStream(updateMetadataStrem);
            long totalSize = firmwareMetadata.TotalSize;
            var currentFirmwarePath = Path.Combine(tuyaPath, "/firmware/current");
            var backupFirmwarePath = Path.Combine(tuyaPath, "/firmware/backup");
            var transitiveFirmware = Path.Combine(tuyaPath, "/OTA/transitive");

            ResetDirectory(transitiveFirmware);

            var metadataFile = File.OpenWrite(Path.Combine(transitiveFirmware, "metadata.json"));
            updateMetadataStrem.CopyTo(metadataFile);
            metadataFile.Flush();

            int index = 1;
            foreach (var assembly in firmwareMetadata.Assemblies)
            {
                var assemblyEntry = zipArchive.GetEntry(assembly.Name);
                if (assemblyEntry == null)
                {
                    throw new FirmwareUpdateException(FirmwareUdpateError.UpdateVersion, $"Assembly {index} not found");
                }
                using var assemblyStream = zipArchive.GetInputStream(assemblyEntry);
                var key = assembly.GenerateKey(DeviceInfo.ProductId);
                HMACHelper.CheckIntegrity(key, assembly.Hmac, assemblyStream);
                var assemblyFile = File.OpenWrite(Path.Combine(transitiveFirmware, assembly.Name));
                assemblyStream.CopyTo(assemblyFile);
                assemblyFile.Flush();
                index++;
            }

            ResetDirectory(backupFirmwarePath);
            Directory.Move(currentFirmwarePath, backupFirmwarePath);

            ResetDirectory(currentFirmwarePath);
            Directory.Move(transitiveFirmware, currentFirmwarePath);

            ResetDirectory(transitiveFirmware);

            progressDelegate?.Invoke(new ProgressReportData(5, data.Channel));
            Updated(firmwareMetadata, data);

        }

        private void Updated(FirmwareMetadata firmwareMetadata, FirmwareUpdateData issueData)
        {
            //TODO: Reboot device upon the Channel type, for MCU we need a propetary bootloader in order to update the TuyaLink firmware
            //For application or modules firmware we can use a a ClrOnly reboot
            Power.RebootDevice(issueData.Channel.RebootOption);
        }


        private static void ResetDirectory(string transitiveFirmware)
        {
            if (Directory.Exists(transitiveFirmware))
            {
                Directory.Delete(transitiveFirmware, true);
                Directory.CreateDirectory(transitiveFirmware);
            }
        }

    }
}
