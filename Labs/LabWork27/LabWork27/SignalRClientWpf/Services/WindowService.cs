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

        public async Task OpenChatWindow(string name, HubConnection connection)
        {
            var viewModel = new ChatViewModel(name, connection);
            var window = new ChatWindow()
            {
                DataContext = viewModel
            };
            window.Show();
        }
        //public void OpenWindow<TViewModel>() where TViewModel : class
        //{
        //    var viewModel = _serviceProvider.GetService<TViewModel>();

        //    if (viewModel == null)
        //        return;

        //    var currentWindow = GetWindowByViewModel(viewModel);

        //}
        //private Window GetWindowByViewModel(object viewModel)
        //{
        //    Window window = viewModel switch
        //    {
        //        MainViewModel => new MainWindow(),
        //        ChatViewModel => new ChatWindow(),
        //        _ => throw new ArgumentException($"Неизвестный viewModel: {viewModel.GetType().Name}")
        //    };
        //    window.DataContext = viewModel;

        //    return window;
        //}
    }
}
