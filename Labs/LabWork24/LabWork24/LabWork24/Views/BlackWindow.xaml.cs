using System;
using System.Collections.Generic;
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

namespace LabWork24.Views
{
    /// <summary>
    /// Логика взаимодействия для BlackWindow.xaml
    /// </summary>
    public partial class BlackWindow : Window
    {
        private bool _isMove = false;
        public BlackWindow()
        {
            InitializeComponent();
            DispatcherTimer dispatcherTimer = new();
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(100);
            dispatcherTimer.Tick += DispatcherTimer_Tick; ;
            dispatcherTimer.Start();
        }

        private void DispatcherTimer_Tick(object? sender, EventArgs e)
        {
            _isMove = true;
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isMove)
                Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.X)
                Close();
        }
    }
}
