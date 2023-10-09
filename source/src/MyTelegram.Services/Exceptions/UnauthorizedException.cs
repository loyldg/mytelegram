namespace MyTelegram.Services.Exceptions;

public class UnauthorizedException : RpcException
{
    public UnauthorizedException(
        string errorMessage) : base(401, errorMessage)
    {
    }
}