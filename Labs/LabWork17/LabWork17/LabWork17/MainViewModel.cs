using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace LabWork17
{
    public partial class MainViewModel : ViewModelBase
    {
        [ObservableProperty] //должно быть развёрнуто в свойство, автоматическая кодогенерация
        public string searchString = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        [ObservableProperty]
        public List<FileInfo> files = new List<FileInfo>();

        public ICommand SearchCommand { get; set; }

        public MainViewModel()
        {
            SearchCommand = new RelayCommand<string>(Search, (x) => !String.IsNullOrWhiteSpace(x));
        }

        public void Search(string searchString)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(searchString);

            if (directoryInfo.Exists)
            {
                try
                {
                    Files = directoryInfo.GetFiles("", SearchOption.TopDirectoryOnly).ToList();

                }
                catch (UnauthorizedAccessException ex)
                {
                    MessageBox.Show($"Нет доступа к папке: {ex.Message}",
                                   "Ошибка доступа",
                                   MessageBoxButton.OK,
                                   MessageBoxImage.Warning);
                    Files = new List<FileInfo>();
                }
            }

        }
    }
}
