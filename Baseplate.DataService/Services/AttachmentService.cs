using Baseplate.DataService.Interfaces;
using Microsoft.Extensions.Logging;

namespace Baseplate.DataService.Services;

public class AttachmentService : IAttachmentService
{
    private readonly ILogger<AttachmentService> _logger;
    private readonly DataContext _dataContext;

    public AttachmentService(ILogger<AttachmentService> logger, DataContext dataContext)
    {
        _logger = logger;
        _dataContext = dataContext;
        
    }
    
}