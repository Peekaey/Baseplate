using Baseplate.Models.Database;

namespace Baseplate.Models.Dtos;

public class RoomDto
{
    public int Id { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public string ShareableSlug { get; set; }
    public ICollection<Message> Messages { get; set; }
}