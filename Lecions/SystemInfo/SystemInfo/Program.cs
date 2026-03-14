using System.Management;
using System.Text;
using System.Diagnostics;


Console.WriteLine(HardwareInfo.GetCpuInfo());
Console.WriteLine(HardwareInfo.GetRamInfo());
Console.WriteLine(HardwareInfo.GetGpuInfo());
Console.WriteLine(HardwareInfo.GetDiskInfo());
Console.WriteLine(HardwareInfo.GetNetworkInfo());
HardwareInfo.GetCurrentPerfomance();
HardwareInfo.GetCurrentPerfomance();
HardwareInfo.GetCurrentPerfomance();

public class HardwareInfo
{
    public static string GetCpuInfo()
    {
        StringBuilder sb = new();
        using ManagementObjectSearcher searcher = new("SELECT * FROM Win32_Processor");
    
        foreach(ManagementObject obj in searcher.Get())
        {
            sb.AppendLine($"Процессор: {obj["Name"]}" +
                $" Ядер: {obj["NumberOfCores"]}" +
                $" Потоков: {obj["NumberOfLogicalProcessors"]}" +
                $" Частота: {obj["MaxClockSpeed"]} МГц"); 
        }

        return sb.ToString();
    }

    public static string GetRamInfo()
    {
        StringBuilder sb = new();
        using ManagementObjectSearcher searcher = new("SELECT * FROM Win32_ComputerSystem");

        foreach (ManagementObject obj in searcher.Get())
        {
            double ram = Convert.ToDouble(obj["TotalPhysicalMemory"]) / (1 << 30);
            // 30 потому сначала конвертируем из байт в килобайты / 2^10, потом альтернативно из КБ в МБ и из МБ в ГБ

            sb.AppendLine($"Оперативная память: {ram:F2} ГБ");
        }

        return sb.ToString();
    }

    public static string GetGpuInfo()
    {
        StringBuilder sb = new();
        using ManagementObjectSearcher searcher = new("SELECT * FROM Win32_VideoController");

        foreach (ManagementObject obj in searcher.Get())
        {
            double ram = Convert.ToDouble(obj["AdapterRAM"]) / (1 << 30);
            // 30 потому сначала конвертируем из байт в килобайты / 2^10, потом альтернативно из КБ в МБ и из МБ в ГБ

            sb.AppendLine($"Видеокарта: {obj["Name"]}" +
                $" Видеопамять: {ram:F2} ГБ");
        }

        return sb.ToString();
    }

    public static string GetDiskInfo()
    {
        StringBuilder sb = new();
        using ManagementObjectSearcher searcher = new("SELECT * FROM Win32_DiskDrive");

        foreach (ManagementObject obj in searcher.Get())
        {
            double size = Convert.ToDouble(obj["Size"]) / (1 << 30);
            // 30 потому сначала конвертируем из байт в килобайты / 2^10, потом альтернативно из КБ в МБ и из МБ в ГБ

            sb.AppendLine($"Диск: {obj["Model"]}" +
                $" Серийный номер: {obj["SerialNumber"]}" +
                $" Размер: {size:F2} ГБ");
        }

        return sb.ToString();
    }

    public static string GetNetworkInfo()
    {
        StringBuilder sb = new();
        using ManagementObjectSearcher searcher = new("SELECT * FROM Win32_NetworkAdapter WHERE NetEnabled = true");

        foreach (ManagementObject obj in searcher.Get())
        {
            double speed = Convert.ToDouble(obj["Speed"]) / (1000 * 1000);
            double maxSpeed = Convert.ToDouble(obj["MaxSpeed"]) / (1000 * 1000);
            // 30 потому сначала конвертируем из байт в килобайты / 2^10, потом альтернативно из КБ в МБ и из МБ в ГБ

            sb.AppendLine($"Адаптер: {obj["Name"]}" +
                $" Серийный номер: {obj["MACAddress"]}" +
                $" Скорость: {speed:F2} МБит/с" +
                $"Максимальная скорость: {maxSpeed:F2} МБит/с");
        }

        return sb.ToString();
    }

    private static PerformanceCounter cpuCounter =
        new("Processor", "% Processor Time", "_Total");
    private static PerformanceCounter ramCounter =
       new("Memory", "Available MBytes");


    public static void GetCurrentPerfomance()
    {
        cpuCounter.NextValue();

        Thread.Sleep(500);

        Console.WriteLine(cpuCounter.NextValue());
        Console.WriteLine(ramCounter.NextValue());
    }
}