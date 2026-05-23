using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LabWork20.Models;
using LabWork20.ViewModels;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace LabWork20
{
    public partial class MainViewModel : ViewModelBase
    {
        [ObservableProperty]
        private ObservableCollection<ProcessInfo> _processes = new();

        [ObservableProperty]
        private int _countProcess;

        [ObservableProperty]
        private ObservableCollection<ApplicationInfo> _applications = new();

        private DispatcherTimer _dispatcherTimer;

        public MainViewModel()
        {
            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Interval = TimeSpan.FromMilliseconds(2000);
            _dispatcherTimer.Tick += DispatcherTimer_Tick;
            _dispatcherTimer.Start();
        }

        private void DispatcherTimer_Tick(object? sender, EventArgs e)
        {
            GetProcessCount();
            GetProcessList();
            //GetApplicationListWithMWT();
        }

        public void GetProcessList()
        {
            Processes.Clear();
            foreach (Process process in Process.GetProcesses())
            {
                ProcessInfo processInfo = new()
                {
                    Name = process.ProcessName,
                    Id = process.Id,
                    Memory = GetWorkingProcessMemory(process.WorkingSet64)
                };
                Processes.Add(processInfo);
            }
        }

        public void GetProcessCount() =>
            CountProcess = Process.GetProcesses().Length;

        [RelayCommand]
        public void GetApplicationListWithMWT()
        {
            Applications.Clear();
            foreach (Process process in Process.GetProcesses().Where(p => !String.IsNullOrWhiteSpace(p.MainWindowTitle)))
            {
                ApplicationInfo applicationInfo = new()
                {
                    Title = process.MainWindowTitle,
                    StartTime = process.StartTime
                };
                Applications.Add(applicationInfo);
            }
        }

        public string GetWorkingProcessMemory(long longProcessMemory)
        {
            double intProcessMemory = (double)longProcessMemory;
            int counter = 0;
            List<string> memoryUnits = new() {"Байт","Кбайт", "Мбайт","Гбайт" };

            while(intProcessMemory >= 1024)
            {
                intProcessMemory /= 1024;
                counter++;
            }
            string memory = $"{Math.Round(intProcessMemory, 2)} {memoryUnits[counter]}";
            return memory;
        }

        [RelayCommand]
        public void KillProcessById(ProcessInfo processInfo)
        {
            try
            {
                if(processInfo is not null)
                {
                    var applicationId = processInfo.Id;
                    var process = Process.GetProcessById(applicationId);
                    process.Kill();
                    process.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Ошибка",MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}