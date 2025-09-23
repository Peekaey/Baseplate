using Baseplate.BusinessService.Interfaces;
using Baseplate.DataService;
using Baseplate.Models.Results;
using Basesplate.BackgroundService.Interfaces;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Basesplate.BackgroundService.Jobs;

public class DeleteStaleChatrooms : IJob
{
    private readonly ILogger<DeleteStaleChatrooms> _logger;
    private readonly IRoomBusinessService _roomBusinessService;
    private readonly int previousDaysCount = 7;
    
    public DeleteStaleChatrooms(ILogger<DeleteStaleChatrooms> logger, IRoomBusinessService roomBusinessService)
    {
        _logger = logger;
        _roomBusinessService = roomBusinessService;
    }
    
    public async Task Execute(IJobExecutionContext context)
    {
        await ExecuteDeleteStaleChatrooms();
    }
    
    public async Task ExecuteDeleteStaleChatrooms()
    {
        _logger.LogInformation($"ExecuteDeleteStaleChatrooms started at {DateTime.UtcNow}");

        try
        {
             await _roomBusinessService.DeleteStaleChatroomsBackgroundJob(previousDaysCount);
            _logger.LogInformation($"ExecuteDeleteStaleChatrooms finished successfully at {DateTime.UtcNow}");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "ExecuteDeleteStaleChatrooms failed at {DateTime.UtcNow} with error: {ErrorMessage}", e.Message);
            throw;
        }
    }
}