using System.Diagnostics;

List<Process> list = new List<Process>(Process.GetProcesses());

foreach (Process process in list)
{
    Console.WriteLine(process.MainWindowTitle);
}

