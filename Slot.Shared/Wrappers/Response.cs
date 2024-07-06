namespace Slot.Shared.Configuration.Wrappers;

public class Response<T>
{
    public Response(T? data, string? message = null)
        : this(true, message, default, data)
    {
    }

    public Response(string message)
        : this(false, message, default, default)
    {
    }

    public Response(int? errorCode = 400, string? message = null)
        : this(false, message, errorCode, default) { }

    public Response(bool succeeded, string? message, int? errorCode, T? data)
    {
        Succeeded = succeeded;
        ErrorCode = errorCode;
        Message = message;
        Data = data;
    }

    public bool Succeeded { get; private set; }
    public string? Message { get; private set; }
    public int? ErrorCode { get; private set; } = null;
    public T? Data { get; private set; } = default;

    public void AddSuccessResult(T? data, string? message = null)
    {
        Succeeded = true;
        Data = data;
        Message = message;
    }
}