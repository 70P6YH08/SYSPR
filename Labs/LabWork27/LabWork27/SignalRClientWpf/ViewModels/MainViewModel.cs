using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.SignalR.Client;
using SignalRClientWpf.Services;
using SignalRClientWpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Text;
using System.Windows;
using System.Windows.Threading;

namespace SignalRClientWpf
{
    public partial class MainViewModel : ViewModelBase
    {
        private readonly HubConnection _connection;

        private WindowService _windowService;
        public Window Window { get; set; }

        [ObservableProperty]
        private string? _userName;

        [ObservableProperty]
        private string? _roomName;


        public MainViewModel(WindowService windowService, HubConnection connection)
        {
            _windowService = windowService;
            _connection = connection;
        }

        [RelayCommand]
        public async Task ServerConnectAsync()
        {
            if (String.IsNullOrWhiteSpace(UserName))
            {
                MessageBox.Show("Логин не может быть пустым!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (String.IsNullOrWhiteSpace(RoomName))
            {
                MessageBox.Show("Введите название комнаты!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                //if(_connection.State != HubConnectionState.Disconnected)
                //{
                    await _connection.StopAsync();
                //}

                await _connection.StartAsync();

                await _connection.InvokeAsync("JoinRoom", UserName, RoomName);

                _windowService.OpenChatWindow(UserName, RoomName, _connection);

                Application.Current.Dispatcher.Invoke(() =>
                {
                    Window?.Close();
                });

                MessageBox.Show("Подключение произошло успешно!", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
