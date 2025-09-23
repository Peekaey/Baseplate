using System.Text.Json;
using Baseplate.BusinessService.Interfaces;
using Baseplate.DataService.Interfaces;
using Baseplate.Helpers;
using Baseplate.Models.Database;
using Baseplate.Models.Dtos;
using Baseplate.Models.Responses;
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

    public CreateResult<string> CreateRoom()
    {
        Room room = new Room
        {
            ShareableSlug = SlugExtensions.CreateShareableSlug()
        };
        
        var result = _roomService.CreateRoom(room);

        if (result.IsSuccess == false)
        {
            return CreateResult<string>.AsError(result.ErrorMessage);
        }

        RoomDto roomDto = new RoomDto
        {
            Id = result.CreatedId,
            ShareableSlug = result.CreatedEntity.ShareableSlug,
            CreatedAtUtc = result.CreatedEntity.CreatedAtUtc,
        };
        return CreateResult<string>.AsSuccess(result.CreatedId, result.CreatedEntity.ShareableSlug);
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
            CreatedAtUtc = room.CreatedAtUtc.ConvertToAest(),
            Messages = room.Messages
        };
        
        return GetResult<RoomDto>.AsSuccess(roomDto);
    }

    public GetResult<GetRoomResponse> GetRoomDataBySlugResponse(string roomSlug)
    {
        Room? room = _roomService.GetRoomDataBySlug(roomSlug);

        if (room == null)
        {
            return GetResult<GetRoomResponse>.AsNotFound();
        }

        try
        {

            //TODO Optimise this monstrosity
            GetRoomResponse roomResponse = new GetRoomResponse
            {
                Slug = room.ShareableSlug,
                CreatedAt = room.CreatedAtUtc.ConvertToAest(),
                Messages = room.Messages.Select(message => new MessageApiDto
                {
                    CreatedAt = message.CreatedAtUtc.ConvertToAest(),
                    MessageContent = JsonSerializer.Deserialize<string>(message.MessageContent),
                    Attachments = message.Attachments.Select(attachment => new AttachmentApiDto
                    {
                        AttachmentName = attachment.AttachmentName,
                        AttachmentExtension = attachment.AttachmentExtension,
                        AttachmentSizeBytes = attachment.AttachmentSizeBytes,
                        AttachmentUrl = attachment.StorageKey,
                        CreatedAt = attachment.CreatedAtUtc.ConvertToAest(),
                    }).ToList()
                }).ToList()
            };

            return GetResult<GetRoomResponse>.AsSuccess(roomResponse);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return GetResult<GetRoomResponse>.AsError(e.Message);
        }
    }

    public GetResult<int> GetRoomIdBySlug(string roomSlug)
    {
        int? roomId = _roomService.GetRoomIdBySlug(roomSlug);

        if (roomId == null)
        {
            return GetResult<int>.AsNotFound();
        }
        
        return GetResult<int>.AsSuccess(roomId.Value);
        
    }

    public async Task DeleteStaleChatroomsBackgroundJob(int previousDaysCount)
    {
        _roomService.DeleteStaleRoomsBackgroundJob(previousDaysCount);
    }
    
}