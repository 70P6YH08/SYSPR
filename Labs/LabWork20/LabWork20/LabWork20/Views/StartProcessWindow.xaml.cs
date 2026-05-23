using LabWork20.ViewModels;
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

namespace LabWork20.Views
{
    /// <summary>
    /// Логика взаимодействия для StartProcessWindow.xaml
    /// </summary>
    public partial class StartProcessWindow : Window
    {
        private StartProcessViewModel _viewModel = new();
        public StartProcessWindow()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
