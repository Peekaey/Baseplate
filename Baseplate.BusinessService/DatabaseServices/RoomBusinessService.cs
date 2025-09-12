using Baseplate.BusinessService.Interfaces;
using Baseplate.DataService.Interfaces;
using Baseplate.Helpers;
using Baseplate.Models.Database;
using Baseplate.Models.Dtos;
using Baseplate.Models.Results;
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

    public CreateResult<RoomDto> CreateRoom()
    {
        Room room = new Room
        {
            ShareableSlug = SlugExtensions.CreateShareableSlug()
        };
        
        var result = _roomService.CreateRoom(room);

        if (result.IsSuccess == false)
        {
            return CreateResult<RoomDto>.AsError(result.ErrorMessage);
        }

        RoomDto roomDto = new RoomDto
        {
            Id = result.CreatedId,
            ShareableSlug = result.CreatedEntity.ShareableSlug,
            CreatedAtUtc = result.CreatedEntity.CreatedAtUtc,
        };
        return CreateResult<RoomDto>.AsSuccess(result.CreatedId, roomDto);
    }

    public GetResult<RoomDto> GetRoomDataBySlug(string roomSlug)
    {
        Room? room = _roomService.GetRoomDataBySlug(roomSlug);

        if (room == null)
        {
            return GetResult<RoomDto>.AsError("Room not found");
        }

        RoomDto roomDto = new RoomDto
        {
            Id = room.Id,
            ShareableSlug = room.ShareableSlug,
            CreatedAtUtc = room.CreatedAtUtc,
            Messages = room.Messages
        };
        
        return GetResult<RoomDto>.AsSuccess(roomDto);
    }
    
}