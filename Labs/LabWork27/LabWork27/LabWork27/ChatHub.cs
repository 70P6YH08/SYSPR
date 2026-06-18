using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace LabWork27
{
    public class ChatHub : Hub
    {
        public ConcurrentDictionary<string, string> ConnectionToRoom = new();
        public ConcurrentDictionary<string, string> ConnectionToUser = new();

        public async Task SendMessageAsync(string name, string message)
        {
            var connectionId = Context.ConnectionId;

            if(ConnectionToRoom.TryGetValue(connectionId, out var room) && ConnectionToRoom.TryGetValue(connectionId, out var user))
            {
                await Clients.Group(room).SendAsync("Receive", user, message);
            }

            //await Clients.All.SendAsync("Receive", name, message);
            //Console.WriteLine($"{name}: {message}");
        }

        public async Task JoinRoom(string roomName, string userName)
        {
            var connectionId = Context.ConnectionId;

            ConnectionToRoom[connectionId] = roomName;
            ConnectionToUser[connectionId] = userName;

            await Groups.AddToGroupAsync(connectionId, roomName);
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            await Groups.RemoveFromGroupAsync(connectionId, room);
        }
    }
}
