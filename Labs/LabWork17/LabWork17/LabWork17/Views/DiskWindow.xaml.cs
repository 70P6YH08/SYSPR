using LabWork17.ViewModels;
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

namespace LabWork17.Views
{
    /// <summary>
    /// Логика взаимодействия для DiskWindow.xaml
    /// </summary>
    public partial class DiskWindow : Window
    {
        public DiskWindow()
        {
            InitializeComponent();
            DataContext = new DiskViewModel();
        }
    }
}
