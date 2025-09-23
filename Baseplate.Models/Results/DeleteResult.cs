using Baseplate.Models.Database;

namespace Baseplate.Models.Results;

public sealed class DeleteResult : Result
{
    public static DeleteResult AsSuccess()
    {
        return new DeleteResult {IsSuccess = true};
    }

    public new static DeleteResult AsError(string errorMessage, Exception? e = null)
    {
        return new DeleteResult {ErrorMessage = errorMessage, IsSuccess = false, Exception = e};
    }
}