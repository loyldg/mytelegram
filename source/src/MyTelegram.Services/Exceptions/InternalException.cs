namespace MyTelegram.Services.Exceptions;

public class InternalException : RpcException
{
    public InternalException(
        string errorMessage) : base(500, errorMessage)
    {
    }
}