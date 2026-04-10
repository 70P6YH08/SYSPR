using Microsoft.AspNetCore.SignalR.Client;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RealTimeApp.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HubConnection conntection;

        public MainWindow()
        {
            InitializeComponent();

            conntection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5185")
                .Build();

            //  Обработчик для приёма сообщений от сервера (для WPF)

            conntection.On<string, string>("Receive", (username, message) =>
            {
                Dispatcher.Invoke(() =>
                {
                    var text = $"{username} : {message}";
                    messageList.Items.Add(text);
                });
            });
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //отправить сообщение
                await conntection.InvokeAsync("Send", usernameText.Text, messageText.Text);
            }
            catch (Exception ex)
            {
                messageList.Items.Add(ex);
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                await conntection.StartAsync(); //установить соединение
            }
            catch(Exception ex)
            {
                messageList.Items.Add(ex);
            }
        }

        private async void Window_Closed(object sender, EventArgs e)
        {
            await conntection.StopAsync();
        }
    }
}