using System.ComponentModel.DataAnnotations;

namespace Baseplate.Models.Database;

public class Room
{
    public int Id { get; set; }
    [Required]
    public  DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    [Required]
    public required string ShareableSlug { get; set; }
    public ICollection<Message> Messages { get; set; }
}