namespace Baseplate.Models.Results;

public class GetResult<T> : Result 
{
    public T? Entity { get; set; }

    public static GetResult<T> AsSuccess(T entity)
    {
        return new GetResult<T> {IsSuccess = true, Entity = entity, ErrorMessage = string.Empty};
    }

    public static GetResult<T> AsError(string errorMessage, Exception? exception = null)
    {
        return new GetResult<T> { IsSuccess = false, ErrorMessage = errorMessage, Exception = exception };
    }
    
}