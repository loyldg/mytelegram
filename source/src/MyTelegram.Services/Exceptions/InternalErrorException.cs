namespace MyTelegram.Services.Exceptions;

public class InternalErrorException : RpcException
{
    public InternalErrorException(
        string errorMessage) : base(500, errorMessage)
    {
    }
}