using Microsoft.AspNetCore.Mvc;

namespace Baseplate.WebApis.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class RoomController : ControllerBase
{
    private readonly ILogger<RoomController> _logger;
    
    public RoomController(ILogger<RoomController> logger)
    {
        _logger = logger;
    }

    [HttpPost(Name = "Create")]
    public IActionResult CreateRoom()
    {
        return Ok();
    }

    [HttpGet("{roomId}", Name = "JoinRoom")]
    public IActionResult JoinRoom(int roomId)
    {
        return Ok();
    }

    
    
}