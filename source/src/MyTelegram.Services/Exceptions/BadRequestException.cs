namespace MyTelegram.Services.Exceptions;

public class BadRequestException : RpcException
{
    public BadRequestException(
        string errorMessage) : base(400, errorMessage)
    {
    }
}