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

            var listDisk = mainViewModel.GetDiskDriveProperties();

            var listNetworkInformation = mainViewModel.GetNetworkInformation();

            var installApplications = mainViewModel.GetInstalledApplicationName();

            processorListView.ItemsSource = listProcessorProps;
            videoControllerListView.ItemsSource = listVideoControllerProps;
            motherboardDeviceListView.ItemsSource = listMotherBoardProps;
            operatingSystemListView.ItemsSource = listOperatingSystemProps;

            var listDiskProps = listDisk
                .SelectMany(disk => disk.Value
                .Select(prop => new {
                    DiskName = disk.Key,
                    PropName = prop.Key,
                    PropValue = prop.Value
                })).ToList();

            diskDriveListView.ItemsSource = listDiskProps;

            var listNetworkInformationProps = listNetworkInformation
                .SelectMany(networkInterface => networkInterface.Value
                .Select(prop => new {
                    InterfaceName = networkInterface.Key,
                    PropName = prop.Key,
                    PropValue = prop.Value
                })).ToList();

            networkListView.ItemsSource = listNetworkInformationProps;

            installedApplicationListView.ItemsSource = installApplications;
        }
    }
}
