using Baseplate.Models.Results;

namespace Basesplate.BackgroundService.Interfaces;

public interface IDeleteStaleChatrooms
{
    Task ExecuteDeleteStaleChatrooms();
}