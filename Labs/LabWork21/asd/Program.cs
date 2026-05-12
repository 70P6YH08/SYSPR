using System.Management;


ManagementObjectSearcher diskDriveSearcher = new("SELECT * FROM Win32_DiskDrive");
ManagementObjectSearcher processorSearcher = new("SELECT * FROM Win32_Processor");

Dictionary<string, string> processorListPropertiesName = new();

List<string> diskDriveListPropertiesName = new();
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

List<string> GetDiskDriveProperties()
{
    var diskDrive = diskDriveSearcher.Get();

    Dictionary<string, string> diskProperties = new();

    foreach (var obj in diskDrive)
    {
        foreach (var property in obj.Properties)
        {
            if (obj[$"{property.Name}"] != null && !diskDriveListPropertiesName.Contains(obj.GetText(TextFormat.Mof)))
            {
                diskDriveListPropertiesName.Add(obj.GetText(TextFormat.Mof));
            }
        }
    }
    return diskDriveListPropertiesName;
}
foreach (var disks in GetDiskDriveProperties())
{
    Console.WriteLine(disks);
}

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

Dictionary<string, string> GetProcessorProperties()
{
    var processor = processorSearcher.Get();

    foreach (var obj in processor)
    {
        foreach (var property in obj.Properties)
        {
            if (obj[$"{property.Name}"] != null)
                processorListPropertiesName.Add($"{property.Name}", $"{obj[$"{property.Name}"]}");
        }
    }
    return processorListPropertiesName;
}

foreach (var item in GetProcessorProperties())
{
    Console.WriteLine($"{item.Key}: {item.Value}");
}