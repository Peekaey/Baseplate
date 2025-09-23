using System.Transactions;
using Baseplate.DataService.Interfaces;
using Baseplate.Models.Database;
using Baseplate.Models.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Baseplate.DataService.Services;

public class RoomService : IRoomService
{
    private readonly ILogger<RoomService> _logger;
    private readonly DataContext _dataContext;

    public RoomService(ILogger<RoomService> logger, DataContext dataContext)
    {
        _logger = logger;
        _dataContext = dataContext;
    }

    public CreateResult<Room> CreateRoom(Room room)
    {
        try
        {
            using (var transaction = new TransactionScope())
            {
                _dataContext.Rooms.Add(room);
                _dataContext.SaveChanges();
                transaction.Complete();
                return CreateResult<Room>.AsSuccess(room.Id, room);
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return CreateResult<Room>.AsError("Error saving new room to database", e);
        }
    }

    public Room? GetRoomDataBySlug(string roomSlug)
    {
        return _dataContext.Rooms.Where(r => r.ShareableSlug == roomSlug)
            .Include(r => r.Messages)
            .ThenInclude(r => r.Attachments)
            .FirstOrDefault();
    }

    public int? GetRoomIdBySlug(string roomSlug)
    {
        return _dataContext.Rooms.Where(r => r.ShareableSlug == roomSlug)
            .Select(r => r.Id).FirstOrDefault();
    }

    public async Task DeleteStaleRoomsBackgroundJob(int previousDaysCount)
    {
        DateTime cutOff = DateTime.Now.AddDays(-previousDaysCount);
        
            using (var transaction = new TransactionScope()) {
                List<int> roomIds = _dataContext.Rooms.Where(r => r.CreatedAtUtc <= cutOff)
                    .Select(r => r.Id).ToList();

                if (roomIds.Count != 0)
                {
                    var roomsDeleted = _dataContext.Rooms.Where(r => roomIds.Contains(r.Id))
                        .ExecuteDelete();
                    transaction.Complete();
                    _logger.LogInformation($"Deleted {roomIds.Count} rooms from database");
                }
                else
                {
                    _logger.LogInformation($"0 Rooms required to be deleted from database");
                }
                
            }
        
    }
}