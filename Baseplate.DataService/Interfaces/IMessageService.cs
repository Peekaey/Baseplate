using Baseplate.Models.Database;
using Baseplate.Models.Results;

namespace Baseplate.DataService.Interfaces;

public interface IMessageService
{
    CreateResult<Message> CreateMessage(Message message);

}