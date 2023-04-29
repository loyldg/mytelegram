namespace MyTelegram.MessengerServer.Exceptions;

public class UnauthorizedException : RpcException
{
    public UnauthorizedException(
        string errorMessage) : base(ErrorCodes.Unauthorized, errorMessage)
    {
    }
}
