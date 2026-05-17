using CommunityToolkit.Mvvm.ComponentModel;
using LabWork21.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Management;
using System.Net.NetworkInformation;
using System.Text;

namespace LabWork21
{
    partial class MainViewModel : ViewModelBase
    {
        private ManagementObjectSearcher processorSearcher = new("SELECT * FROM Win32_Processor");
        private ManagementObjectSearcher videoControllerSearcher = new("SELECT * FROM Win32_VideoController");
        private ManagementObjectSearcher motherboardDeviceSearcher = new("SELECT * FROM Win32_MotherboardDevice");
        private ManagementObjectSearcher diskDriveSearcher = new("SELECT * FROM Win32_DiskDrive");
        private ManagementObjectSearcher operatingSystemSearcher = new("SELECT * FROM Win32_OperatingSystem");

        [ObservableProperty]
        public Dictionary<string, string> processorListProperties = new();

        [ObservableProperty]
        public Dictionary<string, string> videoControllerListProperties = new();

        [ObservableProperty]
        public Dictionary<string, string> motherboardDeviceListProperties = new();

        [ObservableProperty]
        public Dictionary<string, Dictionary<string, string>> diskDriveListProperties = new();

        [ObservableProperty]
        public Dictionary<string, string> operatingSystemListProperties = new();

        [ObservableProperty]
        public Dictionary<string, Dictionary<string, string>> networkInformationList = new();

        [ObservableProperty]
        public List<string> installedApplicationsList = new();

        public MainViewModel()
        {
            GetProcessorProperties();
            GetVideoControllerProperties();
            GetMotherBoardDeviceProperties();
            GetDiskDriveProperties();
            GetOperatingSystemProperties();
            GetNetworkInformation();
            GetInstalledApplicationName();
        }

        public Dictionary<string, string> GetProcessorProperties()
        {
            var processors = processorSearcher.Get();
            var processor = processors.Cast<ManagementBaseObject>().FirstOrDefault();

            foreach (var property in processor.Properties)
            {
                string propName = property.Name;
                object value = processor[propName];
                if (processor[propName] != null)
                    processorListProperties[propName] = value.ToString();
            }
            return processorListProperties;
        }

        public Dictionary<string, string> GetVideoControllerProperties()
        {
            var videoControllers = videoControllerSearcher.Get();
            var videoController = videoControllers.Cast<ManagementBaseObject>().FirstOrDefault();

            foreach (var property in videoController.Properties)
            {
                string propName = property.Name;
                object value = videoController[propName];

                if (value != null)
                    videoControllerListProperties[propName] = value.ToString();
            }
            return videoControllerListProperties;
        }

        public Dictionary<string, string> GetMotherBoardDeviceProperties()
        {
            var motherboardDevices = motherboardDeviceSearcher.Get();
            var motherbroad = motherboardDevices.Cast<ManagementBaseObject>().FirstOrDefault();

            foreach (var property in motherbroad.Properties)
            {
                string propName = property.Name;
                object value = motherbroad[propName];
                if (value != null)
                    motherboardDeviceListProperties[propName] = value.ToString();
            }
            return motherboardDeviceListProperties;
        }

        public Dictionary<string, Dictionary<string, string>> GetDiskDriveProperties()
        {
            var diskDrive = diskDriveSearcher.Get();

            foreach (var disk in diskDrive)
            {
                string diskName = disk["Name"]?.ToString();
                Dictionary<string, string> diskProperties = new();

                foreach (var property in disk.Properties)
                {
                    string propName = property.Name;
                    object value = disk[propName];

                    if (value != null)
                        diskProperties.Add(property.Name, value.ToString());
                }
                diskDriveListProperties[diskName] = diskProperties;
            }
            return diskDriveListProperties;
        }

        public Dictionary<string, string> GetOperatingSystemProperties()
        {
            var operatingSystem = operatingSystemSearcher.Get().Cast<ManagementBaseObject>().FirstOrDefault();

            foreach (var property in operatingSystem.Properties)
            {
                string propName = property.Name;
                object value = operatingSystem[propName];

                if (value != null)
                    operatingSystemListProperties[propName] = value.ToString();
            }
            return operatingSystemListProperties;
        }

        public Dictionary<string, Dictionary<string, string>> GetNetworkInformation()
        {
            var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

            foreach (var networkInterface in networkInterfaces)
            {
                string networkInterfaceId = networkInterface.Id;
                Dictionary<string, string> networkInterfaceProperties = new();

                networkInterfaceProperties["Id"] = networkInterface.Id;
                networkInterfaceProperties["Name"] = networkInterface.Name;
                networkInterfaceProperties["Description"] = networkInterface.Description;
                networkInterfaceProperties["NetworkInterfaceType"] = networkInterface.NetworkInterfaceType.ToString();
                networkInterfaceProperties["OperationalStatus"] = networkInterface.OperationalStatus.ToString();
                networkInterfaceProperties["Speed"] = networkInterface.Speed.ToString();
                networkInterfaceProperties["Speed"] = networkInterface.Speed.ToString();
                networkInterfaceProperties["IsReceiveOnly"] = networkInterface.IsReceiveOnly.ToString();
                networkInterfaceProperties["SupportsMulticast"] = networkInterface.SupportsMulticast.ToString();
                networkInterfaceProperties["PhysicalAddress"] = networkInterface.GetPhysicalAddress().ToString();

                networkInformationList[networkInterfaceId] = networkInterfaceProperties;
            }
            return networkInformationList;
        }

        public List<string> GetInstalledApplicationName()
        {
            string keyUserApplications = "Software\\Microsoft\\Windows\\CurrentVersion\\Uninstall";
            string keyMachineApplications = "Software\\Microsoft\\Windows\\CurrentVersion\\Uninstall";

            RegistryKey key = Registry.CurrentUser;
            var openUserKey = key.OpenSubKey(keyUserApplications);

            foreach (var item in openUserKey.GetSubKeyNames())
            {
                using (RegistryKey? subKey = openUserKey.OpenSubKey(item))
                {
                    if (subKey?.GetValue("DisplayName") != null)
                        installedApplicationsList.Add(subKey.GetValue("DisplayName").ToString());
                }
            }

            key = Registry.LocalMachine;
            var openMachineKey = key.OpenSubKey(keyMachineApplications);

            foreach (var item in openMachineKey.GetSubKeyNames())
            {
                using (RegistryKey? subKey = openMachineKey.OpenSubKey(item))
                {
                    if (subKey?.GetValue("DisplayName") != null)
                        installedApplicationsList.Add(subKey.GetValue("DisplayName").ToString());
                }
            }

            return installedApplicationsList;
        }
    }
}
