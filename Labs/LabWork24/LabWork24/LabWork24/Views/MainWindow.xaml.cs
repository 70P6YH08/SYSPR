using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace LabWork24.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BlackWindowButton_Click(object sender, RoutedEventArgs e)
        {
            BlackWindow blackWindow = new();
            blackWindow.Show();
        }

        private void ImagesWindowButton_Click(object sender, RoutedEventArgs e)
        {
            ImagesWindow imagesWindow = new();
            imagesWindow.Show();
        }

        private void ClockWindowButton_Click(object sender, RoutedEventArgs e)
        {
            ClockWindow clockWindow = new();
            clockWindow.Show();
        }
    }
}