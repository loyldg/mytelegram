namespace MyTelegram.MessengerServer.Exceptions;

public class BadRequestException : RpcException
{
    public BadRequestException(
        string errorMessage) : base(ErrorCodes.BadRequest, errorMessage)
    {
    }
}
