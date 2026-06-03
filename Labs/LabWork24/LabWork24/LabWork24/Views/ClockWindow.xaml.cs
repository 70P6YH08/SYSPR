using LabWork24.ViewModels;
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
    /// Логика взаимодействия для ClockWindow.xaml
    /// </summary>
    public partial class ClockWindow : Window
    {
        private bool _isMove = false;

        private int stepX = 2;
        private int stepY = 2;

        ClockViewModel clockViewModel = new();

        public ClockWindow()
        {
            InitializeComponent();
            DataContext = clockViewModel;
            DispatcherTimer dispatcherTimer = new();
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(1000);
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Start();

            DispatcherTimer movementTimer = new();
            movementTimer.Interval = TimeSpan.FromMilliseconds(10);
            movementTimer.Tick += MovementTimer_Tick;
            movementTimer.Start();
        }

        private void MovementTimer_Tick(object? sender, EventArgs e)
        {
            double x = Canvas.GetLeft(ClockLabel);
            double y = Canvas.GetTop(ClockLabel);

            double widthLabel = ClockLabel.ActualWidth;
            double heightLabel = ClockLabel.ActualHeight;

            if (double.IsNaN(x))
                x = 0;
            if (double.IsNaN(y))
                y = 0;


            if (widthLabel + x >= ClocksWindow.ActualWidth / 2)
                stepX = -stepX;

            if(heightLabel + y >= ClocksWindow.ActualHeight / 2)
                stepY = -stepY;

            if (x <= -ClocksWindow.ActualWidth / 2)
                stepX = -stepX;

            if (y <= -ClocksWindow.ActualHeight / 2)
                stepY = -stepY;


            double newX = x + stepX;
            double newY = y + stepY;

            Canvas.SetLeft(ClockLabel, newX);
            Canvas.SetTop(ClockLabel, newY);
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
