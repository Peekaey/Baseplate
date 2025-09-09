using Baseplate.BusinessService.Interfaces;
using Baseplate.DataService.Interfaces;
using Microsoft.Extensions.Logging;

namespace Baseplate.BusinessService.DatabaseServices;

public class AttachmentBusinessService : IAttachmentBusinessService
{
    private readonly ILogger<AttachmentBusinessService> _logger;
    private readonly IAttachmentService _attachmentService;
    
    public AttachmentBusinessService(ILogger<AttachmentBusinessService> logger, IAttachmentService attachmentService)
    {
        _logger = logger;
        _attachmentService = attachmentService;
    }
    
}