using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.SignalR.Client;
using SignalRClientWpf.Models;
using SignalRClientWpf.Services;
using SignalRClientWpf.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;

namespace SignalRClientWpf.ViewModels
{
    public partial class ChatViewModel : ViewModelBase
    {
        private readonly HubConnection _connection;
        public Window Window { get; set; }

        [ObservableProperty]
        private string _user;

        [ObservableProperty]
        private string _message;

        [ObservableProperty]
        private string _roomName;

        [ObservableProperty]
        private ObservableCollection<Message> _chat = new();

        public ChatViewModel(string user, string roomName, HubConnection connection)
        {
            User = user;
            RoomName = roomName;
            _connection = connection;

            _connection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Chat.Add(new Message
                    {
                        User = user,
                        UserMessage = message
                    });
                });
            });
        }

        [RelayCommand]
        public async Task SendMessageAsync()
        {
            if (String.IsNullOrWhiteSpace(Message))
            {
                MessageBox.Show("Нельзя оправить пустое сообщение!",
                    "Внимание",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            try
            {
                await _connection.InvokeAsync("SendMessageAsync", Message);
                Chat.Add(new Message
                {
                    User = User,
                    UserMessage = Message
                });
                Message = string.Empty;
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
            Application.Current.Dispatcher.Invoke(() =>
            {
                Window?.Close();
            });
        }
    }
}
