namespace MyTelegram;

public class UserFriendlyException : Exception
{
    public int ErrorCode { get; }

    public UserFriendlyException()
    {

    }

    public UserFriendlyException(string message, int errorCode = 400) : base(message)
    {
        ErrorCode = errorCode;
    }

    public UserFriendlyException(string message, Exception inner) : base(message, inner)
    {

    }
}