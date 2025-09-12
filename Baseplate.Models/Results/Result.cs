namespace Baseplate.Models.Results;

public class Result
{
    public bool IsSuccess { get; set; }
    public string ErrorMessage { get; set; }
    public Exception? Exception { get; set; }

    public static Result AsSuccess()
    {
        return new Result { IsSuccess = true };
    }
    
    public static Result AsError(string errorMessage, Exception? exception = null)
    {
        return new Result { IsSuccess = false, ErrorMessage = errorMessage, Exception = exception };
    }
}