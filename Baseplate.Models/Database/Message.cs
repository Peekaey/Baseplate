using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Baseplate.Models.Database;

public class Message
{
    public int Id { get; set; }
    [StringLength(5000)]
    public required string MessageContent { get; set; }
    [Required] public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    
    [Required]
    public int RoomId { get; set; }

    public Room Room { get; set; }

    public ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();
}