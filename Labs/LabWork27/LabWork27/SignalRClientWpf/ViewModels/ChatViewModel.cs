using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;

namespace SignalRClientWpf.ViewModels
{
    public partial class ChatViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private string _message;

        [ObservableProperty]
        private ObservableCollection<string> _chat = new() {"====== УСПЕШНОЕ ПОДКЛЮЧЕНИЕ ======"};

        private readonly HubConnection _connection;
        public ChatViewModel(string name, HubConnection connection)
        {
            Name = name;
            _connection = connection;

            _connection.On<string>("ReceiveMessage", (message) =>
            {
                Chat.Add(message);
            });
        }

        [RelayCommand]
        public async Task SendMessageAsync()
        {
            try
            {

                if (String.IsNullOrWhiteSpace(Message))
                {
                    MessageBox.Show("Нельзя оправить пустое сообщение!",
                        "Внимание",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                    return;
                }

                await _connection.InvokeAsync("SendMessageAsync", Name, Message);
                Chat.Add($"Вы: {Message}");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand]
        public async Task DisconnectAsync()
        {
            MessageBoxResult messageBoxResult = MessageBox.Show(
                "Вы уверены, что хотите отключиться?",
                "Предупреждение",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning
                );

            if (messageBoxResult == MessageBoxResult.No)
                return;

            await _connection.StopAsync();
        }
    }
}
