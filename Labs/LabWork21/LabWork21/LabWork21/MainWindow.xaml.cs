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

namespace LabWork21
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private DispatcherTimer dispatcherTimer;

        private MainViewModel viewModel = new();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = viewModel;

            //dispatcherTimer = new DispatcherTimer();
            //dispatcherTimer.Interval = TimeSpan.FromSeconds(2);
            //dispatcherTimer.Tick += DispatcherTimer_Tick;
            //dispatcherTimer.Start();

            var listProcessorProps = viewModel.GetProcessorProperties();
            var listVideoControllerProps = viewModel.GetVideoControllerProperties();
            var listMotherBoardProps = viewModel.GetMotherBoardDeviceProperties();
            var listOperatingSystemProps = viewModel.GetOperatingSystemProperties();

            var listDisk = viewModel.GetDiskDriveProperties();

            var listNetworkInformation = viewModel.GetNetworkInformation();

            var installApplications = viewModel.GetInstalledApplicationName();

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

        //private void DispatcherTimer_Tick(object? sender, EventArgs e)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
