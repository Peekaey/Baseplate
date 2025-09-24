using Baseplate.Models.Dtos;
using Baseplate.Models.Responses;
using Baseplate.Models.Results;

namespace Baseplate.BusinessService.Interfaces;

public interface IRoomBusinessService
{
    CreateResult<string> CreateRoom();
    GetResult<RoomDto> GetRoomDataBySlug(string roomSlug);
    GetResult<GetRoomResponse> GetRoomDataBySlugResponse(string roomSlug);
    GetResult<int> GetRoomIdBySlug(string roomSlug);
    Task DeleteStaleChatroomsBackgroundJob(int previousDaysCount);
    bool ValidateRoomExists(string roomSlug);
}