using Baseplate.Models.Database;
using Baseplate.Models.Results;

namespace Baseplate.DataService.Interfaces;

public interface IRoomService
{
    CreateResult<Room> CreateRoom(Room room);
    Room? GetRoomDataBySlug(string roomSlug);
    int? GetRoomIdBySlug(string roomSlug);
    Task DeleteStaleRoomsBackgroundJob(int previousDaysCount);

}