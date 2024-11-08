using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;

namespace TuyaLink.Firmware
{
    public class FirmwareLoader
    {
        public static bool IsLoading { get; private set; }

        public static bool IsLoaded { get; private set; }

        public static IAssemblyEntryPoint[] Runners { get; private set; }

        internal static ManualResetEvent _updateWaitHandler = new(false);

        private static ITargetDevice _targetDevice;

        internal static void SignalUpdate()
        {
            _updateWaitHandler.Set();
        }

        public static void WaitForUpdate()
        {
            _updateWaitHandler.WaitOne();
        }

        public static FirmwareMetadata LoadFirmware()
        {
            try
            {
                IsLoading = true;
                var metadata = LoadAssemblies();
                var runners = new IAssemblyEntryPoint[metadata.RunnersCount];
                int index = 0;
                foreach (var assembly in metadata.Assemblies)
                {
                    if (!assembly.Runable)
                    {
                        continue;
                    }
                    var type = Assembly.Load(assembly.Name).GetType(assembly.EntryPoint);
                    if (type == null)
                    {
                        throw new FirmwareLoadException($"Entry point {assembly.EntryPoint} not found in assembly {assembly.Name}");
                    }
                    if (!type.IsSubclassOf(typeof(IAssemblyEntryPoint)))
                    {
                        throw new FirmwareLoadException($"Entry point {assembly.EntryPoint} in assembly {assembly.Name} does not implement IAssemblyEntryPoint");
                    }


                    ConstructorInfo defaultConstructor = type.GetConstructor([]) ?? throw new FirmwareLoadException($"No public default constructor found in assembly {assembly.Name}");
                    defaultConstructor.Invoke(null, []);

                    var entryPoint = (IAssemblyEntryPoint)defaultConstructor.Invoke([]);

                    entryPoint.Run(assembly.LaunchArgs);
                    runners[index] = entryPoint;
                    index++;
                }

                Runners = runners;

                IsLoaded = true;
                return metadata;
            }
            finally
            {
                IsLoading = false;
            }
        }

        public static void StopFirmware()
        {
            if (!IsLoaded)
            {
                return;
            }
            foreach (var runner in Runners)
            {
                try
                {
                    runner.Stop();
                }
                catch (System.Exception ex)
                {
                    Debug.WriteLine($"Error while stopping runner {runner}");
                    Debug.WriteLine(ex.ToString());
                }
            }
            Runners = null;
            IsLoaded = false;
        }

        public static void Run(ITargetDevice targetDevice)
        {
            if (targetDevice is null)
            {
                throw new ArgumentNullException(nameof(targetDevice));
            }
            _targetDevice = targetDevice;
            _targetDevice.InternetConnect();
            while (true)
            {
                try
                {
                    LoadFirmware();
                }
                catch (FirmwareLoadException ex)
                {
                    Debug.WriteLine($"Error while loading firmware: {ex}");
                    Thread.Sleep(1000);
                }
            }
        }

        public static void DeepSleep()
        {

        }

        private static FirmwareMetadata LoadAssemblies()
        {
            var metadataFilePath = Path.Combine(FirmwarePaths.Current, FirmwareConsts.MetadataFileName);
            if (!File.Exists(metadataFilePath))
            {
                RestoreFromBackup();
            }
            using var metadataStream = File.OpenRead(metadataFilePath);
            var metadata = MetadataUtils.FromStream(metadataStream);

            foreach (var assembly in metadata.Assemblies)
            {
                var assemblyPath = Path.Combine(FirmwarePaths.Current, assembly.Name);
                if (!File.Exists(assemblyPath))
                {
                    RestoreFromBackup();
                }
            }

            foreach (var assembly in metadata.Assemblies)
            {
                var assemblyPath = Path.Combine(FirmwarePaths.Current, assembly.Name);
                Assembly.Load(assemblyPath);
            }
            return metadata;
        }

        private static void RestoreFromBackup()
        {
            var metadataFilePath = Path.Combine(FirmwarePaths.Backup, FirmwareConsts.MetadataFileName);
            if (!File.Exists(metadataFilePath))
            {
                throw new FirmwareLoadException("No backup found");
            }
            using var metadataStream = File.OpenRead(metadataFilePath);
            var metadata = MetadataUtils.FromStream(metadataStream);
            foreach (var assembly in metadata.Assemblies)
            {
                var assemblyPath = Path.Combine(FirmwarePaths.Backup, assembly.Name);
                if (!File.Exists(assemblyPath))
                {
                    throw new FirmwareLoadException($"Backup assembly {assembly.Name} not found for version {metadata.Version}");
                }
            }
        }
    }
}
