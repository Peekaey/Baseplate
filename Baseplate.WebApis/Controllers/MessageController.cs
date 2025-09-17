using Baseplate.BusinessService.Interfaces;
using Baseplate.Messaging;
using Baseplate.Messaging.Interfaces;
using Baseplate.Models.Dtos;
using Baseplate.Models.Requests;
using Baseplate.Models.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Baseplate.WebApis.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class MessageController : ControllerBase
{
    private readonly ILogger<MessageController> _logger;
    private readonly IMessageBusinessService  _messageBusinessService;
    private readonly IRoomBusinessService _roomBusinessService;
    private readonly IHubContext<MessageHub>  _hubContext;
    private readonly IMessageHub  _messageHub;

    public MessageController(ILogger<MessageController> logger, IMessageBusinessService messageBusinessService,
        IRoomBusinessService roomBusinessService, IHubContext<MessageHub> hubContext, IMessageHub messageHub)
    {
        _logger = logger;
        _messageBusinessService = messageBusinessService;
        _roomBusinessService = roomBusinessService;
        _hubContext = hubContext;
        _messageHub = messageHub;
    }

    [HttpPost("create", Name = "create")]
    public IActionResult CreateMessage([FromBody] CreateMessageRequest request)
    {
        if (ModelState.IsValid == false)
        {
            return BadRequest(ModelState);
        }

        GetResult<int> getRoomResult = _roomBusinessService.GetRoomIdBySlug(request.RoomId);

        if (getRoomResult.NotFound == true)
        {
            return NotFound();
        }

        CreateResult<MessageApiDto> saveMessageResult = _messageBusinessService.SaveNewMessage(request.Message, getRoomResult.Entity);

        if (saveMessageResult.IsSuccess == false)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        
        _messageHub.SendMessage(saveMessageResult.CreatedEntity.CreatedAt,request.Message, request.RoomId);
        return Ok(saveMessageResult.CreatedEntity);
    }
    
    
    
    
}