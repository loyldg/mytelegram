namespace MyTelegram.MessengerServer.Exceptions;

public class ForbiddenException : RpcException
{
    public ForbiddenException(
        string errorMessage) : base(ErrorCodes.Forbidden, errorMessage)
    {
    }
}