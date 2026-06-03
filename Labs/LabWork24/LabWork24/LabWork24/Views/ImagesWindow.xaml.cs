using LabWork24.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using Path = System.IO.Path;

namespace LabWork24.Views
{
    /// <summary>
    /// Логика взаимодействия для ImagesWindow.xaml
    /// </summary>
    public partial class ImagesWindow : Window
    {
        bool isMove = false;

        public ImagesWindow()
        {
            InitializeComponent();
            DataContext = new ImagesViewModel();

            DispatcherTimer dispatcherTimer = new();
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(100);
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Start();

        }
        private void DispatcherTimer_Tick(object? sender, EventArgs e)
        {
            isMove = true;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.X)
                Close();
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMove)
                Close();
        }
    }
}
