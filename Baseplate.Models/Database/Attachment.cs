using System.ComponentModel.DataAnnotations;

namespace Baseplate.Models.Database;

public class Attachment
{
    public int Id { get; set; }
    [Required]
    public required string AttachmentExtension { get; set; }
    [Required]
    public required string AttachmentName { get; set; }
    [Required]
    public long AttachmentSizeBytes { get; set; }
    [Required]
    public required string MimeType {get; set; }
    
    [Required]
    public required string StorageKey { get; set; }

    [Required] public required DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    
    public string MessageId { get; set; }
    public Message Message { get; set; }
    
}