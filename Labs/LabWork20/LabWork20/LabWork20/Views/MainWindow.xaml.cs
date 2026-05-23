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

namespace LabWork20.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel mainViewModel = new();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = mainViewModel;
        }

        private void NewWindowMenuItem_Click(object sender, RoutedEventArgs e)
        {
            StartProcessWindow startProcessWindow = new();
            startProcessWindow.Show();
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}