using System.Net;
using Baseplate.BusinessService.Interfaces;
using Baseplate.Models.Dtos;
using Baseplate.Models.Results;
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

    [HttpPost("create")]
    public IActionResult CreateRoom()
    {
        //TODO Do Security Validations Here
        CreateResult<RoomDto> saveResult = _roomBusinessService.CreateRoom();

        if (saveResult.IsSuccess == false)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        
        return Ok(saveResult.CreatedEntity);
    }

    [HttpGet("{roomId}", Name = "join")]
    public IActionResult JoinRoom([FromBody] string roomSlug)
    {
        if (ModelState.IsValid == false)
        {
            return BadRequest(ModelState);
        }
        
        GetResult<RoomDto> getResult = _roomBusinessService.GetRoomDataBySlug(roomSlug);

        if (getResult.IsSuccess == false)
        {
            return StatusCode(StatusCodes.Status404NotFound);
        }
        
        return Ok();
    }

    
    
}