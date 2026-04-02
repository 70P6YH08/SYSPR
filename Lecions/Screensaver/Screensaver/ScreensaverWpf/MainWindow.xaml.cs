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

namespace ScreensaverWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer;

        int speedX = 1000;
        int speedY = 1;

        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromMilliseconds(16);
            timer.Start();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            var x = Canvas.GetLeft(JupmingText);
            var y = Canvas.GetTop(JupmingText);

            if(JupmingText.ActualWidth + x > ActualWidth || x < 0)
            {
                speedX = -speedX;
            }
            if (JupmingText.ActualHeight + y > ActualHeight || y < 0)
            {
                speedY = -speedY;
            }


            Canvas.SetLeft(JupmingText, x + speedX);
            Canvas.SetTop(JupmingText, y + speedY);
        }
    }
}