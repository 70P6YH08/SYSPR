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

namespace WpfDI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FilesServices filesServices;
        public MainWindow(FilesServices filesServices)
        {
            InitializeComponent();
            this.filesServices = filesServices;

            FilesBox.ItemsSource = filesServices.GetFiles("C:\\Users\\-221");
        }
    }
}