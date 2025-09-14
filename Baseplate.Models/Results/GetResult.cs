namespace Baseplate.Models.Results;

public class GetResult<T> : Result 
{
    public T? Entity { get; set; }
    
    public bool NotFound{ get; set; }

    public static GetResult<T> AsSuccess(T entity)
    {
        return new GetResult<T> {IsSuccess = true, Entity = entity, ErrorMessage = string.Empty};
    }

    public static GetResult<T> AsError(string errorMessage, Exception? exception = null)
    {
        return new GetResult<T> { IsSuccess = false, ErrorMessage = errorMessage, Exception = exception };
    }

    public static GetResult<T> AsNotFound()
    {
        return new GetResult<T> { IsSuccess = false, NotFound = true, ErrorMessage = string.Empty };
    }
    
}