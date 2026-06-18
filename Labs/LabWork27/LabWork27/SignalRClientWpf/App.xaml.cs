using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using SignalRClientWpf.Services;
using SignalRClientWpf.ViewModels;
using SignalRClientWpf.Views;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Automation.Provider;

namespace SignalRClientWpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IServiceProvider? _serviceProvider;
        public App() { }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var services = new ServiceCollection();

            services.AddTransient<HubConnection>(h => new HubConnectionBuilder()
                    .WithUrl("https://localhost:7058/chat")
                    .WithAutomaticReconnect()
                    .Build());

            services.AddTransient<ChatViewModel>();
            services.AddTransient<MainViewModel>();
            services.AddTransient<WindowService>();

            _serviceProvider = services.BuildServiceProvider();

            var windowService = _serviceProvider.GetRequiredService<WindowService>();

            windowService.OpenMainWindow();
        }
    }
}
