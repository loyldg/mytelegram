// ReSharper disable once CheckNamespace

namespace MyTelegram;

public class ThrowHelper
{
    public static void ThrowUserFriendlyException(string message,
        int errorCode = ErrorCodes.BadRequest)
    {
        throw new UserFriendlyException(message, errorCode);
    }

    public static void ThrowUserFriendlyException(UserFriendlyException exception)
    {
        throw exception;
    }
}
