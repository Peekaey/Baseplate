using System.Net;
using Baseplate.BusinessService.Interfaces;
using Baseplate.Messaging;
using Baseplate.Models.Dtos;
using Baseplate.Models.Responses;
using Baseplate.Models.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Baseplate.WebApis.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class RoomController : ControllerBase
{
    private readonly ILogger<RoomController> _logger;
    private readonly IRoomBusinessService _roomBusinessService;
    private readonly IHubContext<MessageHub>  _hubContext;
    
    public RoomController(ILogger<RoomController> logger, IRoomBusinessService roomBusinessService, IHubContext<MessageHub> hubContext)
    {
        _logger = logger;
        _roomBusinessService = roomBusinessService;
        _hubContext = hubContext;
    }

    [HttpPost("create")]
    public IActionResult CreateRoom()
    {
        //TODO Do Security Validations Here
        CreateResult<string> saveResult = _roomBusinessService.CreateRoom();

        if (saveResult.IsSuccess == false)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        CreateRoomResponse response = new CreateRoomResponse
        {
            Slug = saveResult.CreatedEntity
        };
        return Ok(response);
    }

    [HttpGet("get/{roomId}", Name = "get")]
    public IActionResult GetRoom(string roomId)
    {
        if (ModelState.IsValid == false)
        {
            return BadRequest(ModelState);
        }

        GetResult<GetRoomResponse> getResult = _roomBusinessService.GetRoomDataBySlugResponse(roomId);

        if (getResult.NotFound == true)
        {
            return NotFound();
        }

        if (getResult.IsSuccess == false)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return Ok(getResult.Entity);
    }

    [HttpGet("validate/{roomId}", Name = "validate")]
    public IActionResult ValidateRoom(string roomId)
    {
        if (ModelState.IsValid == false)
        {
            return BadRequest(ModelState);
        }
        
        bool isValid = _roomBusinessService.ValidateRoomExists(roomId);

        if (isValid == false)
        {
            return NotFound();
        }
        
        return Ok();
    }
    

}