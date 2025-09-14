using System.Net;
using Baseplate.BusinessService.Interfaces;
using Baseplate.Models.Dtos;
using Baseplate.Models.Responses;
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

    [HttpGet("join/{roomId}", Name = "join")]
    public IActionResult JoinRoom(string roomId)
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

}