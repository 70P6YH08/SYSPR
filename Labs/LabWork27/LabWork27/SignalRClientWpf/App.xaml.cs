using Microsoft.Extensions.DependencyInjection;
using SignalRClientWpf.Services;
using SignalRClientWpf.ViewModels;
using SignalRClientWpf.Views;
using System.Configuration;
using System.Data;
using System.Windows;

namespace SignalRClientWpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IServiceProvider _serviceProvider;

        public App()
        {
            var services = new ServiceCollection();

            services.AddTransient<ChatViewModel>();
            services.AddTransient<MainViewModel>();
            services.AddTransient<WindowService>();

            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();

            var mainWindow = new MainWindow
            {
                DataContext = mainViewModel
            };

            mainWindow.Show();
        }
    }
}
