using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LabWork17.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static FileDirInfo;

namespace LabWork17.Views
{
    public partial class MainViewModel : ViewModelBase
    {
        [ObservableProperty]
        public string searchString = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);

        [ObservableProperty]
        public ObservableCollection<FileDirInfo> files = new();

        [ObservableProperty]
        public ObservableCollection<FileDirInfo> disks = new();

        [ObservableProperty]
        public FileDirInfo selectedItem;

        [ObservableProperty]
        public Visibility isVisibleFiles = Visibility.Collapsed;

        [ObservableProperty]
        public Visibility isVisibleDisks = Visibility.Visible;

        [ObservableProperty]
        public Visibility isProperiesMenuVisible = Visibility.Visible;

        [RelayCommand]
        public void OpenDiskProperties(object selectedItem)
        {
            var selected = selectedItem as FileDirInfo;
            if (selected != null)
            {
                if(selected.Type == FileType.Folder)
                {
                    FolderViewModel folderViewModel = new FolderViewModel(this, selected);
                    FolderWindow folderWindow = new();
                    folderWindow.DataContext = folderViewModel;
                    folderWindow.Show();
                }
                else if(selected.Type == FileType.HardDrive)
                {
                    DiskViewModel diskViewModel = new DiskViewModel(selected);
                    DiskWindow diskWindow = new();
                    diskWindow.DataContext = diskViewModel;
                    diskWindow.Show();
                }
            }
            else
                MessageBox.Show("Такого диска не существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public MainViewModel()
        {
            Files = Search(searchString);
        }

        public ObservableCollection<FileDirInfo> Search(string searchString)
        {
            if (String.IsNullOrEmpty(searchString) || searchString == Environment.GetFolderPath(Environment.SpecialFolder.MyComputer))
            {
                Disks.Clear();
                var disks = DriveInfo.GetDrives();
                foreach (var disk in disks)
                {
                    var diskTotalSizeBytes = disk.TotalSize;
                    var diskAvailableSizeBytes = disk.AvailableFreeSpace;

                    var diskTotalSize = ConvertWeight(diskTotalSizeBytes);
                    var diskAvailzbleSize = ConvertWeight(diskAvailableSizeBytes);

                    var diskTotalSizeKey = diskTotalSize.Keys.First();
                    var diskTotalSizeValue = ConvertWeight(diskTotalSizeBytes).Values.First();
                    var diskAvailbaleSizeKey = diskAvailzbleSize.Keys.First();
                    var diskAvailbaleSizeValue = diskAvailzbleSize.Values.First();

                    var diskOccupiedSize = ConvertWeight(diskTotalSizeBytes - diskAvailableSizeBytes);
                    var diskOccupiedSizeKey = diskOccupiedSize.Keys.First();
                    var diskOccupiedSizeValue = diskOccupiedSize.Values.First();

                    var standardUnitWeight = TypeUnitWeight.B;

                    FileDirInfo diskInfo = new()
                    {
                        Type = FileType.HardDrive,
                        DiskLetter = disk.VolumeLabel,
                        Name = disk.Name,

                        DiskTotalSizeTextBytes = $"{diskTotalSizeBytes} {standardUnitWeight}",
                        DiskTotalSizeText = $"{diskTotalSizeKey} {diskTotalSizeValue}",

                        DiskAvailableSizeBytes = $"{diskAvailableSizeBytes} {standardUnitWeight}",
                        DiskAvailableSize = $"{diskAvailbaleSizeKey} {diskAvailbaleSizeValue}",

                        DiskOccupiedSizeBytes = $"{diskTotalSizeBytes - diskAvailableSizeBytes} {standardUnitWeight}",
                        DiskOccupiedSizeText = $"{diskOccupiedSizeKey} {diskOccupiedSizeValue}",

                        DiskTotalSize = diskTotalSizeKey,
                        DiskOccupiedSize = CompareValues(diskTotalSize, diskAvailzbleSize),

                        DiskType = $"{disk.DriveType}",
                        DiskFileSystem = disk.DriveFormat
                    };
                    Disks.Add(diskInfo);
                }
                return Disks;
            }
            else
            {
                Files.Clear();
                DirectoryInfo directoryInfo = new DirectoryInfo(searchString);

                FileDirInfo parentFolder = new() { Name = "..", Type = FileType.ParentFolder };
                Files.Add(parentFolder);

                if (directoryInfo.Exists)
                {
                    try
                    {
                        var directories = directoryInfo.GetDirectories();
                        if (directories.Length > 0)
                        {
                            foreach (var folder in directories)
                            {
                                FileDirInfo fileDirInfo = new()
                                {
                                    Name = folder.Name,
                                    Type = FileType.Folder,
                                    FullDirPath = folder.FullName,
                                };
                                Files.Add(fileDirInfo);
                            }
                        }
                        var files = directoryInfo.GetFiles();
                        if (files.Length > 0)
                        {
                            foreach (var file in files)
                            {
                                FileDirInfo fileDirInfo = new()
                                {
                                    Name = file.Name,
                                    ChangedTime = file.LastWriteTimeUtc,
                                    Weight = $"{ConvertWeight(file.Length).Keys.FirstOrDefault()} {ConvertWeight(file.Length).Values.FirstOrDefault()}",
                                    Type = SetIcon(file.Extension)
                                };
                                Files.Add(fileDirInfo);
                            }
                        }
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        MessageBox.Show($"Нет доступа к папке: {ex.Message}",
                                       "Ошибка доступа",
                                       MessageBoxButton.OK,
                                       MessageBoxImage.Warning);
                    }
                }
                else
                    MessageBox.Show("Папка не найдена!");
                return Files;
            }
        }

        public static Dictionary<double, TypeUnitWeight> ConvertWeight(long fileWeight)
        {
            List<TypeUnitWeight> typeWeight = [TypeUnitWeight.B, TypeUnitWeight.KB, TypeUnitWeight.MB, TypeUnitWeight.GB, TypeUnitWeight.TB];
            Dictionary<double, TypeUnitWeight> keyValuePairs = new();
            int unitWeight = (int)TypeUnitWeight.B;
            double weight = (double)fileWeight;
            while (weight >= 1024)
            {
                weight /= 1024;
                unitWeight++;
            }
            keyValuePairs[Math.Round(weight, 2)] = typeWeight[unitWeight];
            return keyValuePairs;
        }

        public double CompareValues(Dictionary<double, TypeUnitWeight> diskTotalSize, Dictionary<double, TypeUnitWeight> diskAvailableSize)
        {
            var totalSizeValue = (int)diskTotalSize.Values.First();
            var availzbleSizeValue = (int)diskAvailableSize.Values.First();

            var availableSizeKey = diskAvailableSize.Keys.First();
            var totalSizeKey = diskTotalSize.Keys.First();

            while(totalSizeValue > availzbleSizeValue)
            {
                availableSizeKey /= 1024;
            }
            double occupiedSize = totalSizeKey - availableSizeKey;
            return occupiedSize;
        }

        public FileType SetIcon(string fileExtension)
        {
            FileType fileIconPath;
            switch (fileExtension)
            {
                case ".jpg" or ".png" or ".jpeg" or ".gif" or ".webp" or ".bmp" or ".psd":
                    fileIconPath = FileType.Image;
                    break;
                case ".mp4" or ".avi" or ".mov" or ".webm" or ".flv" or ".MP4":
                    fileIconPath = FileType.Video;
                    break;
                case ".zip" or ".rar" or ".7z" or ".tar":
                    fileIconPath = FileType.Archive;
                    break;
                case ".aac" or ".mp3" or ".ape" or ".alac" or ".aiff" or ".wav":
                    fileIconPath = FileType.Audio;
                    break;
                case ".txt" or ".doc" or ".docx" or ".rtf" or ".csv" or ".odt":
                    fileIconPath = FileType.Text;
                    break;
                default:
                    fileIconPath = FileType.Unknown;
                    break;
            }
            return fileIconPath;
        }
    }
}
