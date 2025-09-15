using Baseplate.DataService.Interfaces;
using Baseplate.Models.Database;
using Baseplate.Models.Results;
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

    public CreateResult<Message> CreateMessage(Message message)
    {
        using (var transaction = _dataContext.Database.BeginTransaction())
        {
            try
            {
                _dataContext.Messages.Add(message);
                _dataContext.SaveChanges();
                transaction.Commit();
                return CreateResult<Message>.AsSuccess(message.Id, message);
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
                return CreateResult<Message>.AsError("Error saving new message to database", e);
            }
        }
    }
}