// ReSharper disable once CheckNamespace
namespace MyTelegram;

public class UserFriendlyException : Exception
{
    public int ErrorCode { get; }

    public UserFriendlyException(string message, int errorCode = ErrorCodes.BadRequest) : base(message)
    {
        ErrorCode = errorCode;
    }
}