namespace VowelBonus.Shared.Common.Models;

public class Response<T>
{
    public Response()
    {

    }

    public Response(bool succeeded, string message, T Result)
    {
        Succeeded = succeeded;
        Message = message;
        this.Result = Result;
    }

    public bool Succeeded { get; init; }
    public string? Message { get; init; }
    public T? Result { get; init; } = default;

    public  Response<T> Success(T result, string message)
    {
        return new Response<T>(true, message, result);
    }

    public  Response<T> Failure(string message)
    {
        return new Response<T>(false, message, default);
    }
}
