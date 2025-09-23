namespace Baseplate.Models.Results;

public class ExecuteResult : Result
{
    public static ExecuteResult AsSuccess()
    {
        return new ExecuteResult { IsSuccess = true };
    }

    public new static ExecuteResult AsError(string errorMessage, Exception? e)
    {
        return new ExecuteResult { ErrorMessage = errorMessage, IsSuccess = false, Exception = e };
    }
}