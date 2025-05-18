namespace VowelBonus.Shared.Common.Models;

public class Response<T>
{
    public Response()
    {

    }

    public Response(bool succeeded, string message, T Result, int totalRecord)
    {
        Succeeded = succeeded;
        Message = message;
        TotalRecord = totalRecord;
        this.Result = Result;
    }

    public bool Succeeded { get; init; }
    public string? Message { get; init; }
    public T? Result { get; init; } = default;
    public int TotalRecord { get; init; } = default;

    public  Response<T> Success(T result, string message)
    {
        return new Response<T>(true, message, result, default);
    }

    public Response<T> Success(T result, string message, int totalRecord)
    {
        return new Response<T>(true, message, result, totalRecord);
    }

    public  Response<T> Failure(string message)
    {
        return new Response<T>(false, message, default, default);
    }
}
