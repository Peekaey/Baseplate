using System.Text.Json;
using System.Text.Json.Nodes;
using Baseplate.BusinessService.Interfaces;
using Baseplate.DataService.Interfaces;
using Baseplate.Helpers;
using Baseplate.Models.Database;
using Baseplate.Models.Dtos;
using Baseplate.Models.Results;
using Microsoft.Extensions.Logging;

namespace Baseplate.BusinessService.DatabaseServices;

public class MessageBusinessService : IMessageBusinessService
{
    private readonly ILogger<MessageBusinessService> _logger;
    private readonly IMessageService _messageService;

    public MessageBusinessService(ILogger<MessageBusinessService> logger, IMessageService messageService)
    {
        _logger = logger;
        _messageService = messageService;
    }

    public CreateResult<MessageApiDto> SaveNewMessage(string messageContent, int roomId)
    {
        Message message = new Message
        {
            MessageContent = JsonSerializer.Serialize(messageContent),
            RoomId = roomId
        };

        CreateResult<Message> saveResult = _messageService.CreateMessage(message);

        if (saveResult.IsSuccess == false)
        {
            return CreateResult<MessageApiDto>.AsError(saveResult.ErrorMessage);
        }

        MessageApiDto messageApiDto = new MessageApiDto
        {
            CreatedAt = message.CreatedAtUtc.ConvertToAest(),
            MessageContent = message.MessageContent,
        };
        
        return CreateResult<MessageApiDto>.AsSuccess(saveResult.CreatedId,messageApiDto);
    }
    
}