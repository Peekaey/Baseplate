using Baseplate.Models.Dtos;

namespace Baseplate.Models.Responses;

public class GetRoomResponse
{
    public string Slug { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<MessageApiDto> Messages { get; set; } = new List<MessageApiDto>();
}