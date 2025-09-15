using Microsoft.AspNetCore.SignalR;

namespace Baseplate.Messaging;

public class MessageHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
    
    public async Task JoinRoom(string roomSlug)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, roomSlug);
        await Clients.Group(roomSlug).SendAsync("UserJoined", Context.ConnectionId);
    }

    public async Task LeaveRoom(string roomSlug)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomSlug);
        await Clients.Group(roomSlug).SendAsync("UserLeft", Context.ConnectionId);
    }
}