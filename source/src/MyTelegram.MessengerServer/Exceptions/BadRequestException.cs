namespace MyTelegram.MessengerServer.Exceptions;

public class BadRequestException : RpcException
{
    public BadRequestException(
        string errorMessage) : base(400, errorMessage)
    {
    }
}