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

namespace LabWork21
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();

            MainViewModel mainViewModel = new MainViewModel();
            var listProcessorProps = mainViewModel.GetProcessorProperties();
            var listVideoControllerProps = mainViewModel.GetVideoControllerProperties();
            var listMotherBoardProps = mainViewModel.GetMotherBoardDeviceProperties();
            var listOperatingSystemProps = mainViewModel.GetOperatingSystemProperties();

            processorListView.ItemsSource = listProcessorProps;
            videoControllerListView.ItemsSource = listVideoControllerProps;
            motherboardDeviceListView.ItemsSource = listMotherBoardProps;
            operatingSystemListView.ItemsSource = listOperatingSystemProps;

        }
    }
}

