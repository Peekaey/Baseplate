namespace Baseplate.Models.Results;

public sealed class CreateResult<T> : Result
{
    public int CreatedId { get; set; }
    public T? CreatedEntity { get; set; }
    public static CreateResult<T> AsSuccess(int createdId, T createdEntity)
    {
        return new CreateResult<T> {IsSuccess = true, CreatedId = createdId, CreatedEntity = createdEntity, ErrorMessage = string.Empty};
    }
    
    public new static CreateResult<T> AsError(string errorMessage, Exception? exception = null)
    {
        return new CreateResult<T> { IsSuccess = false, ErrorMessage = errorMessage, Exception = exception };
    }
    
}