using System.Diagnostics;

//Process.Start("notepad");

//var startInfo = new ProcessStartInfo()
//{
//    FileName = "cmd.exe",
//    UseShellExecute = false,
//    RedirectStandardOutput = true,
//    RedirectStandardError = true,
//    RedirectStandardInput = true,
//    CreateNoWindow = true,
//};

//using (var process = Process.Start(startInfo))
//{
//    using (var writer = process.StandardInput)
//    {
//        writer.WriteLine("ping ya.ru");
//    }

//    string output = process.StandardOutput.ReadToEnd();
//    string error = process.StandardError.ReadToEnd();

//    process.WaitForExit();

//    Console.WriteLine($"Output: {output}");

//    Console.WriteLine($"Error: {error}");
//}

var processes = Process.GetProcesses();

foreach (var process in processes)
{
    Console.WriteLine($"Id: {process.Id}," +
        $"Name: {process.ProcessName}" +
        $"Handle: {process.VirtualMemorySize64 >> 30}");
    //process.Kill();

}

var prc = Process.GetProcessesByName("notepad");
