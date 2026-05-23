using Microsoft.Win32;
using System.Management;
using System.Net.NetworkInformation;


//ManagementObjectSearcher diskDriveSearcher = new("SELECT * FROM Win32_DiskDrive");
//ManagementObjectSearcher processorSearcher = new("SELECT * FROM Win32_Processor");

//Dictionary<string, string> processorListPropertiesName = new();

//List<string> diskDriveListPropertiesName = new();
//Dictionary<string, string> diskDriveListPropertiesName = new();

//Dictionary<string, string> processorListPropertiesName = new();

//List<string> processorListPropertiesValue = new();

//Dictionary<string, string> GetProperties()
//{
//    var processor = managementObjectSearcher.Get();

//    foreach (var obj in processor)
//    {
//        foreach (var property in obj.Properties)
//        {
//            if (obj[$"{property.Name}"] != null)
//                processorListPropertiesName.Add($"{property.Name}", $"{obj[$"{property.Name}"]}");
//        }
//    }
//    return processorListPropertiesName;
//}


//foreach (var propertyName in GetProperties())
//{
//    Console.WriteLine($"{propertyName.Key}: {propertyName.Value}");
//}

//List<string> GetDiskDriveProperties()
//{
//    var diskDrive = diskDriveSearcher.Get();

//    foreach (var obj in diskDrive)
//    {
//        foreach (var property in obj.Properties)
//        {
//            Console.WriteLine($"{property.Name} : {obj[property.Name]} ");
//        }
//    }
//    return diskDriveListPropertiesName;
//}
//foreach (var disks in GetDiskDriveProperties())
//{
//    Console.WriteLine(disks);
//}

//Dictionary<string, string> GetDiskDriveProperties()
//{
//    var diskDrive = diskDriveSearcher.Get();

//    Dictionary<string, string> diskProperties = new();

//    foreach (var obj in diskDrive)
//    {
//        foreach (var property in obj.Properties)
//        {
//            if (obj[$"{property.Name}"] != null)
//            {
//                diskDriveListPropertiesName.Add($"{property.Name}", $"{obj[$"{property.Name}"]}");
//            }
//        }
//    }
//    return diskDriveListPropertiesName;
//}
//GetDiskDriveProperties();

//Dictionary<string, string> GetProcessorProperties()
//{
//    var processor = processorSearcher.Get();

//    foreach (var obj in processor)
//    {
//        foreach (var property in obj.Properties)
//        {
//            if (obj[$"{property.Name}"] != null)
//                processorListPropertiesName.Add($"{property.Name}", $"{obj[$"{property.Name}"]}");
//        }
//    }
//    return processorListPropertiesName;
//}

//foreach (var item in GetProcessorProperties())
//{
//    Console.WriteLine($"{item.Key}: {item.Value}");
//}

//var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

//foreach (var item in networkInterfaces)
//{
//    Console.WriteLine(item.Id);
//}
List<string> installedApplication = new();

string keyUserApplications = "Software\\Microsoft\\Windows\\CurrentVersion\\Uninstall";
string keyMachineApplications = "Software\\Microsoft\\Windows\\CurrentVersion\\Uninstall";

RegistryKey key = Registry.CurrentUser;
var openUserKey = key.OpenSubKey(keyUserApplications);

foreach (var item in openUserKey.GetSubKeyNames())
{
    using (RegistryKey subKey = openUserKey.OpenSubKey(item))
    {
        if (subKey.GetValue("DisplayName") != null)
            installedApplication.Add(subKey.GetValue("DisplayName").ToString());
    }
}

key = Registry.LocalMachine;
var openMachineKey = key.OpenSubKey(keyMachineApplications);

foreach (var item in openMachineKey.GetSubKeyNames())
{
    using (RegistryKey subKey = openMachineKey.OpenSubKey(item))
    {
        if (subKey.GetValue("DisplayName") != null)
            installedApplication.Add(subKey.GetValue("DisplayName").ToString());
    }
}