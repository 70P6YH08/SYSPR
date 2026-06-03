using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Threading;

namespace LabWork24.ViewModels
{
    public partial class ClockViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _clockTime = DateTime.Now.ToString("T");

        public ClockViewModel()
        {
            DispatcherTimer updateTimer = new();
            updateTimer.Interval = TimeSpan.FromMilliseconds(500);
            updateTimer.Tick += UpdateTimer_Tick;
            updateTimer.Start();
        }

        private void UpdateTimer_Tick(object? sender, EventArgs e)
        {
            ClockTime = DateTime.Now.ToString("T");
        }
    }
}
