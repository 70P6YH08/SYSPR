using CommunityToolkit.Mvvm.ComponentModel;
using LabWork21.Models;
using System;
using System.Collections.Generic;
using System.Management;
using System.Text;

namespace LabWork21
{
    partial class MainViewModel : ViewModelBase
    {
        private ManagementObjectSearcher processorSearcher = new("SELECT * FROM Win32_Processor");
        private ManagementObjectSearcher videoControllerSearcher = new ("SELECT * FROM Win32_VideoController");
        private ManagementObjectSearcher motherboardDeviceSearcher = new("SELECT * FROM Win32_MotherboardDevice");
        private ManagementObjectSearcher diskDriveSearcher = new("SELECT * FROM Win32_DiskDrive");
        private ManagementObjectSearcher operatingSystemSearcher = new("SELECT * FROM Win32_OperatingSystem");

        [ObservableProperty]
        public Dictionary<string, string> processorListPropertiesName = new();

        [ObservableProperty]
        public Dictionary<string, string> videoControllerListPropertiesName = new();

        [ObservableProperty]
        public Dictionary<string, string> motherboardDeviceListPropertiesName = new();

        [ObservableProperty]
        public Dictionary<string, Dictionary<string, string>> diskDriveListPropertiesName = new();

        [ObservableProperty]
        public Dictionary<string, string> operatingSystemListPropertiesName = new();

        public MainViewModel()
        {
            GetProcessorProperties();
            GetVideoControllerProperties();
            GetMotherBoardDeviceProperties();
            //GetDiskDriveProperties();
            GetOperatingSystemProperties();
        }


        public Dictionary<string, string> GetProcessorProperties()
        {
            var processor = processorSearcher.Get();

            foreach (var obj in processor)
            {
                foreach (var property in obj.Properties)
                {
                    if (obj[$"{property.Name}"] != null && !processorListPropertiesName.ContainsKey(property.Name))
                        processorListPropertiesName.Add($"{property.Name}", $"{obj[$"{property.Name}"]}");
                }
            }
            return processorListPropertiesName;
        }

        public Dictionary<string, string> GetVideoControllerProperties()
        {
            var videoController = videoControllerSearcher.Get();

            foreach (var obj in videoController)
            {
                foreach (var property in obj.Properties)
                {
                    if (obj[$"{property.Name}"] != null && !videoControllerListPropertiesName.ContainsKey(property.Name))
                        videoControllerListPropertiesName.Add($"{property.Name}", $"{obj[$"{property.Name}"]}");
                }
            }
            return videoControllerListPropertiesName;
        }

        public Dictionary<string, string> GetMotherBoardDeviceProperties()
        {
            var motherboardDevice = motherboardDeviceSearcher.Get();

            foreach (var obj in motherboardDevice)
            {
                foreach (var property in obj.Properties)
                {
                    if (obj[$"{property.Name}"] != null && !motherboardDeviceListPropertiesName.ContainsKey(property.Name))
                        motherboardDeviceListPropertiesName.Add($"{property.Name}", $"{obj[$"{property.Name}"]}");
                }
            }
            return motherboardDeviceListPropertiesName;
        }

        //public Dictionary<string, Dictionary<string, string>> GetDiskDriveProperties()
        //{
        //    var diskDrive = diskDriveSearcher.Get();

        //    Dictionary<string, string> diskProperties = new();

        //    foreach (var obj in diskDrive)
        //    {
        //        foreach (var property in obj.Properties)
        //        {
        //            if (obj[$"{property.Name}"] != null)
        //            {
        //                diskProperties.Add($"{property.Name}", $"{obj[$"{property.Name}"]}");
        //                diskDriveListPropertiesName.Add(obj.GetText(TextFormat.Mof), diskProperties);
        //            }
        //        }
        //    }
        //    return diskDriveListPropertiesName;
        //}

        public Dictionary<string, string> GetOperatingSystemProperties()
        {
            var operatingSystem = operatingSystemSearcher.Get();

            foreach (var obj in operatingSystem)
            {
                foreach (var property in obj.Properties)
                {
                    if (obj[$"{property.Name}"] != null && !operatingSystemListPropertiesName.ContainsKey(property.Name))
                        operatingSystemListPropertiesName.Add($"{property.Name}", $"{obj[$"{property.Name}"]}");
                }
            }
            return operatingSystemListPropertiesName;
        }
    }
}
