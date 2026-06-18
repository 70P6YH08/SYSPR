using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using SignalRClientWpf.ViewModels;
using SignalRClientWpf.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace SignalRClientWpf.Services
{
    public class WindowService
    {
        private readonly IServiceProvider _serviceProvider;

        public WindowService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void OpenChatWindow(string user, string roomName, HubConnection connection)
        {
            var viewModel = new ChatViewModel(user, roomName, connection);
            var window = new ChatWindow()
            {
                DataContext = viewModel
            };

            viewModel.Window = window;

            window.Show();
        }

        public void OpenMainWindow()
        {
            var viewModel = _serviceProvider.GetService<MainViewModel>();
            var mainWindow = new MainWindow
            {
                DataContext = viewModel
            };

            if(viewModel != null)
                viewModel.Window = mainWindow;

            mainWindow.Show();
        }
    }
}
