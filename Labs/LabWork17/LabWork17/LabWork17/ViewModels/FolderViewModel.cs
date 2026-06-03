using CommunityToolkit.Mvvm.ComponentModel;
using LabWork17.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using static FileDirInfo;

namespace LabWork17.ViewModels
{
    public partial class FolderViewModel : ViewModelBase
    {

        [ObservableProperty]
        public MainViewModel viewModel;

        [ObservableProperty]
        public FileDirInfo selectedFolder;

        [ObservableProperty]
        public string folderPath = Directory.GetCurrentDirectory();

        public FolderViewModel(MainViewModel mainViewModel, FileDirInfo fileDirInfo)
        {
            selectedFolder = fileDirInfo;
            viewModel = mainViewModel;
            selectedFolder = MainFunc(SelectedFolder.FullDirPath);
        }

        public FileDirInfo MainFunc(string dirPath)
        {
            DirectoryInfo directoryInfo = new(dirPath);
            var rootFolder = directoryInfo.Root.Name;
            var disk = new DriveInfo(rootFolder);

            var totalSize = disk.TotalSize;
            var folderSizeBytes = GetFilesSize(directoryInfo);

            var folderSizeKey = MainViewModel.ConvertWeight(folderSizeBytes).Keys.First();
            var folderSizeValue = MainViewModel.ConvertWeight(folderSizeBytes).Values.First();

            var standardUnitWeight = TypeUnitWeight.B;

            Dictionary<long, string> topFilesDict = new();

            GetTopFiveFiles(directoryInfo, topFilesDict);

            var sortedList = topFilesDict.OrderByDescending(n => n.Key).ToDictionary();
            var topListBytes = sortedList.Take(5).ToDictionary();

            var resultTopList = GetResultList(topListBytes);

            FileDirInfo folder = new()
            {
                Name = directoryInfo.Name,
                CountDirs = GetDirs(directoryInfo),
                Type = FileType.Folder,
                CountFiles = GetFilesCount(directoryInfo),
                FolderSize = $"{folderSizeKey} {folderSizeValue} ({folderSizeBytes} {standardUnitWeight})",
                FolderPercent = (double)folderSizeBytes / totalSize * 100,
                TopFiles = resultTopList
            };
            return folder;
        }
        public int GetDirs(DirectoryInfo directoryInfo)
        {
            int countDirs = 0;

            try
            {
                foreach (var dir in directoryInfo.GetDirectories())
                {
                    countDirs++;
                    countDirs += GetDirs(dir);
                }
            }
            catch (UnauthorizedAccessException ex) { }

            return countDirs;
        }
        public int GetFilesCount(DirectoryInfo directoryInfo)
        {
            int filesCount = 0;

            //var filePaths = Directory.EnumerateFiles(directoryInfo.FullName, "*.*", new EnumerationOptions
            //{
            //    IgnoreInaccessible = true,
            //    RecurseSubdirectories = true
            //});

            //filesCount = filePaths.Count();
            try
            {
                filesCount = directoryInfo.GetFiles().Length;

                foreach (var dir in directoryInfo.GetDirectories())
                    filesCount += GetFilesCount(dir);
            }
            catch (UnauthorizedAccessException ex) { }

            return filesCount;
        }

        public long GetFilesSize(DirectoryInfo directoryInfo)
        {
            long filesSize = 0;

            try
            {
                foreach (var file in directoryInfo.GetFiles())
                    filesSize += file.Length;

                foreach (var dir in directoryInfo.GetDirectories())
                    filesSize += GetFilesSize(dir);
            }
            catch (UnauthorizedAccessException ex) { }

            return filesSize;
        }

        public void GetTopFiveFiles(DirectoryInfo directoryInfo, Dictionary<long, string> topFilesDict)
        {
            try
            {
                foreach (var file in directoryInfo.GetFiles())
                    topFilesDict[file.Length] = file.FullName;

                foreach (var dir in directoryInfo.GetDirectories())
                    GetTopFiveFiles(dir, topFilesDict);
            }
            catch (UnauthorizedAccessException ex) { }
        }

        public Dictionary<string, string> GetResultList(Dictionary<long, string> topListBytes)
        {
            Dictionary<string, string> resultList = new();
            foreach (var fileSizeBytes in topListBytes)
            {
                var fileSize = MainViewModel.ConvertWeight(fileSizeBytes.Key);
                string resultSize = $"{fileSize.Keys.First()} {fileSize.Values.First()}";
                resultList[resultSize] = fileSizeBytes.Value;
            }
            return resultList;
        }
    }
}
