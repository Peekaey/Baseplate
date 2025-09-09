using Baseplate.DataService.Interfaces;
using Microsoft.Extensions.Logging;

namespace Baseplate.DataService.Services;

public class MessageService : IMessageService
{
    private readonly ILogger<MessageService> _logger;
    private readonly DataContext _dataContext;

    public MessageService(ILogger<MessageService> logger, DataContext dataContext)
    {
        _logger = logger;
        _dataContext = dataContext;
    }
}