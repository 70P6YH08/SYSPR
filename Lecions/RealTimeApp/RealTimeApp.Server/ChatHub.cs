using Microsoft.AspNetCore.SignalR;

namespace RealTimeApp.Server
{
    public class ChatHub : Hub
    {
        public async Task Send(string username, string message, string groupName)
        {
            //this.Clients.All; //сообщение всем
            //this.Clients.Caller; //сообщение отправителю
            //this.Clients.Others; //сообщение всем кроме отправителя
            await this.Clients.Group(groupName).SendAsync("Receive", username, message);
        }

        public async Task Enter(string username, string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }
    }
}
