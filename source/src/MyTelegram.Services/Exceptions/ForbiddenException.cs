namespace MyTelegram.Services.Exceptions;

public class ForbiddenException : RpcException
{
    public ForbiddenException(
        string errorMessage) : base(403, errorMessage)
    {
    }
}