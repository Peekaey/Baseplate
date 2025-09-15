using Baseplate.Models.Dtos;
using Baseplate.Models.Results;

namespace Baseplate.BusinessService.Interfaces;

public interface IMessageBusinessService
{
    CreateResult<MessageApiDto> SaveNewMessage(string messageContent, int roomId);
}