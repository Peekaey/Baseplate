using Baseplate.DataService.Interfaces;
using Microsoft.Extensions.Logging;

namespace Baseplate.DataService.Services;

public class RoomService : IRoomService
{
    private readonly ILogger<RoomService> _logger;
    private readonly DataContext _dataContext;

    public RoomService(ILogger<RoomService> logger, DataContext dataContext)
    {
        _logger = logger;
        _dataContext = dataContext;
    }
}