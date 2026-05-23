using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;

namespace LabWork20.ViewModels
{
    public partial class StartProcessViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string _processName;

        [RelayCommand]
        public void StartProcess()
        {
            try
            {
                if(!String.IsNullOrWhiteSpace(ProcessName))
                {
                    Process.Start(ProcessName);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand]
        public void SelectProcess()
        {
            OpenFileDialog dialog = new()
            {
                Title = "Выберите исполнямый файл",
                Filter = "Исполняемые файлы (*.exe)|*.exe|All files (*.*)|*.*",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
            };
            if (dialog.ShowDialog() is true)
            {
                var fileName = dialog.FileName;
                ProcessName = fileName;
                StartProcess();
            };
        }
    }
}
