using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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

namespace LabWork17
{
    public partial class MainViewModel : ViewModelBase
    {
        [ObservableProperty]
        public string searchString = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        [ObservableProperty]
        public ObservableCollection<FileDirInfo> files = new();

        public MainViewModel()
        {
            Files = Search(searchString);
        }

        public ObservableCollection<FileDirInfo> Search(string searchString)
        {
            if (String.IsNullOrEmpty(searchString)){

                var disks = DriveInfo.GetDrives();
                foreach (var disk in disks)
                {
                    FileDirInfo diskInfo = new()
                    {
                        Name = disk.Name,
                        Type = FileType.HardDrive
                    };
                    Files.Add(diskInfo);
                }
                return Files;
            }
            else
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(searchString);

                Files.Clear();

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
                                    Type = FileType.Folder
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
                                    Weight = ConvertWeight(file.Length),
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
                {
                    MessageBox.Show("Папка не найдена!");
                }
                return Files;
            }
        }

        public string ConvertWeight(long fileWeight)
        {
            List<string> typeWeight = ["Байт", "КБайт", "МБайт", "ГБайт"];
            int counter = 0;
            while (fileWeight >= 1024)
            {
                fileWeight /= 1024;
                counter++;
            }
            return $"{fileWeight} {typeWeight[counter]}";
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
