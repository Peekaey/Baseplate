using Baseplate.BusinessService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Baseplate.WebApis.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class RoomController : ControllerBase
{
    private readonly ILogger<RoomController> _logger;
    private readonly IRoomBusinessService _roomBusinessService;
    
    
    
    public RoomController(ILogger<RoomController> logger, IRoomBusinessService roomBusinessService)
    {
        _logger = logger;
        _roomBusinessService = roomBusinessService;
    }

    [HttpPost(Name = "create")]
    public IActionResult CreateRoom()
    {
        return Ok();
    }

    [HttpGet("{roomId}", Name = "join")]
    public IActionResult JoinRoom(int roomId)
    {
        return Ok();
    }

    
    
}