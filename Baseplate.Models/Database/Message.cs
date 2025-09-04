using System.ComponentModel.DataAnnotations;

namespace Baseplate.Models.Database;

public class Messages
{
    public int Id { get; set; }
    public required string MessageContent { get; set; }
    [Required]
    public DateTime CreatedAtUtc { get; set; }
    [Required]
    public bool HasAttachments { get; set; }
    public 
}