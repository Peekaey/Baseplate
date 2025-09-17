using Baseplate.Messaging.Interfaces;
using Baseplate.Models.Responses;
using Microsoft.AspNetCore.SignalR;

namespace Baseplate.Messaging;

public class MessageHub : Hub, IMessageHub
{
    private readonly IHubContext<MessageHub> _hubContext;

    public MessageHub(IHubContext<MessageHub> hubContext)
    {
        _hubContext = hubContext;
    }
    public async Task SendMessage(DateTime createdAt, string messageContent, string roomSlug)
    {
        ReceiveMessageResponse dto = new ReceiveMessageResponse
        {
            CreatedDate = createdAt,
            MessageContent = messageContent,
        };
        _hubContext.Clients.Group(roomSlug).SendAsync("ReceiveMessage", dto);

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