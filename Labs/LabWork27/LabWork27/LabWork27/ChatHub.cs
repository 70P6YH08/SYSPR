using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace LabWork27
{
    public class ChatHub : Hub
    {
        public static ConcurrentDictionary<string, string> ConnectionToRoom = new();
        public static ConcurrentDictionary<string, string> ConnectionToUser = new();

        public async Task SendMessageAsync(string message)
        {
            var connectionId = Context.ConnectionId;

            if(ConnectionToRoom.TryGetValue(connectionId, out var room) && ConnectionToUser.TryGetValue(connectionId, out var user))
                await Clients.Group(room).SendAsync("ReceiveMessage", user, message);

            //await Clients.All.SendAsync("Receive", name, message);
        }

        public async Task JoinRoom(string userName, string roomName)
        {
            var connectionId = Context.ConnectionId;

            ConnectionToRoom[connectionId] = roomName;
            ConnectionToUser[connectionId] = userName;

            await Groups.AddToGroupAsync(connectionId, roomName);
            await Clients.Group(roomName).SendAsync("ReceiveMessage", "Подключение", $"{userName} ПРИСОЕДИНИЛСЯ к комнате {roomName}.");
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var connectionId = Context.ConnectionId;
            if(ConnectionToRoom.TryRemove(connectionId, out var room))
                await Groups.RemoveFromGroupAsync(connectionId, room);
            //ConnectionToUser.TryRemove(connectionId, out var user);
        }
    }
}
