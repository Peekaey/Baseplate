using Baseplate.Models.Dtos;
using Baseplate.Models.Results;

namespace Baseplate.BusinessService.Interfaces;

public interface IRoomBusinessService
{
    CreateResult<RoomDto> CreateRoom();
    GetResult<RoomDto> GetRoomDataBySlug(string roomSlug);
}