namespace Baseplate.Models.Results;

public sealed class UpdateResult : Result
{
    public static UpdateResult AsSuccess()
    {
        return new UpdateResult { IsSuccess = true };
    }
    
    public new static UpdateResult AsError(string errorMessage, Exception? exception = null)
    {
        return new UpdateResult { IsSuccess = false, ErrorMessage = errorMessage, Exception = exception };
    }
    
}