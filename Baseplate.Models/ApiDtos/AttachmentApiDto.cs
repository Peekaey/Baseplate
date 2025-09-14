namespace Baseplate.Models.Dtos;

public class AttachmentApiDto
{
    public string AttachmentExtension { get; set; }
    public string AttachmentName { get; set; }
    public long AttachmentSizeBytes { get; set; }
    public string AttachmentUrl { get; set; }
    public DateTime CreatedAt { get; set; }
}