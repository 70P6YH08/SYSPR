using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace LabWork20
{
    public partial class MainViewModel : ViewModelBase
    {
        [ObservableProperty]
        public ObservableCollection<Process> processes = new(Process.GetProcesses().OrderBy(p => p.ProcessName));

        [ObservableProperty]
        public int countProcess = Process.GetProcesses().Length;

        [ObservableProperty]
        public ObservableCollection<Process> applications = new();

        public void GetListProcessWithMWT()
        {
            applications = new(processes.Where(a => !String.IsNullOrEmpty(a.MainWindowTitle)));
        }
    }
}