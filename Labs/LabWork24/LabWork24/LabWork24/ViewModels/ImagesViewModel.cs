using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace LabWork24.ViewModels
{
    public partial class ImagesViewModel : ObservableObject
    {
        private DispatcherTimer _updatePicture;

        [ObservableProperty]
        private BitmapImage _picture;

        private Random _random = new Random();
        private string _directoryPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

        public ImagesViewModel()
        {

            _updatePicture = new DispatcherTimer();
            _updatePicture.Interval = TimeSpan.FromSeconds(2);
            _updatePicture.Tick += UpdatePicture_Tick;
            _updatePicture.Start();

            UpdatePictureFunc();
        }

        private void UpdatePicture_Tick(object? sender, EventArgs e)
        {
            UpdatePictureFunc();
        }

        private void UpdatePictureFunc()
        {

            try
            {
                var dir = new DirectoryInfo(_directoryPath);
                if (!dir.Exists)
                    return;

                var files = dir.EnumerateFiles("*.*", SearchOption.AllDirectories);
                var pictures = files.Where(f => IsPicture(f.Extension)).ToArray();

                if (pictures.Length == 0)
                    return;

                int randomFileNumber = _random.Next(0, pictures.Length);
                var randomFile = pictures[randomFileNumber];


                string? randomFileName = randomFile.FullName;
                Picture = new BitmapImage(new Uri(randomFileName, UriKind.Absolute));
            }
            catch (Exception ex) { }
        }
        private bool IsPicture(string extension)
        {
            List<string> exts = new() { ".jpg", ".jpeg", ".png", ".bmp", ".gif" };
            if (exts.Contains(extension.ToLower()))
                return true;
            else
                return false;
        }
    }
}
