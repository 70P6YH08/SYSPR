using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.SignalR.Client;
using SignalRClientWpf.Services;
using SignalRClientWpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Threading;

namespace SignalRClientWpf
{
    public partial class MainViewModel : ViewModelBase
    {
        private WindowService _windowService;

        [ObservableProperty]
        private string? _name;


        public MainViewModel(WindowService windowService)
        {
            _windowService = windowService;
        }

        [RelayCommand]
        public async Task ServerConnectAsync()
        {
            if (String.IsNullOrWhiteSpace(Name))
            {
                MessageBox.Show("Логин не может быть пустым!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                var connection = new HubConnectionBuilder()
                    .WithUrl("https://localhost:7058/chat")
                    .Build();
                await connection.StartAsync();
                await _windowService.OpenChatWindow(Name, connection);
                MessageBox.Show("Подключение произошло успешно!", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                Name = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
