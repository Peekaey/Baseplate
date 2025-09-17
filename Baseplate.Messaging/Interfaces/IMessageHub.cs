namespace Baseplate.Messaging.Interfaces;

public interface IMessageHub
{
    Task SendMessage(DateTime createdAt, string messageContent, string roomSlug);
}