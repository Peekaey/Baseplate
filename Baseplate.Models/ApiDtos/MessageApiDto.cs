namespace Baseplate.Models.Dtos;

public class MessageApiDto
{
    public DateTime CreatedAt { get; set; }
    public string MessageContent { get; set; }
    public ICollection<AttachmentApiDto> Attachments { get; set; } = new List<AttachmentApiDto>();
}