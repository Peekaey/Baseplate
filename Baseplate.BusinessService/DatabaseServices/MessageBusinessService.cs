using Baseplate.BusinessService.Interfaces;
using Baseplate.DataService.Interfaces;
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
    
}