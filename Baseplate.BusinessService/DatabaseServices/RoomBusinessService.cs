using Baseplate.BusinessService.Interfaces;
using Baseplate.DataService.Interfaces;
using Microsoft.Extensions.Logging;

namespace Baseplate.BusinessService.DatabaseServices;

public class RoomBusinessService : IRoomBusinessService
{
    private readonly ILogger<RoomBusinessService> _logger;
    private readonly IRoomService _roomService;
    
    public RoomBusinessService(ILogger<RoomBusinessService> logger, IRoomService roomService)
    {
        _logger = logger;
        _roomService = roomService;
    }
    
}