// ReSharper disable once CheckNamespace
namespace MyTelegram;

public static class RpcErrorMessages
{
    public const string PhoneCodeEmpty = "PHONE_CODE_EMPTY";
    public const string PhoneCodeInvalid = "PHONE_CODE_INVALID";
    public const string PhoneCodeExpired = "PHONE_CODE_EXPIRED";

    public const string PhotoInvalid = "PHOTO_INVALID";
    public const string ChannelInvalid = "CHANNEL_INVALID";

    public const string InvalidOperation = "Invalid_Operation";

    public const string ChatAdminRequired = "CHAT_ADMIN_REQUIRED";
    public const string ChatAboutTooLong = "CHAT_ABOUT_TOO_LONG";
    public const string AdminsTooMuch = "ADMINS_TOO_MUCH";
    public const string ChatNotModified = "CHAT_NOT_MODIFIED";
    public const string BroadcastIdInvalid = "BROADCAST_ID_INVALID";
    public const string ChatWriteForbidden = "CHAT_WRITE_FORBIDDEN";
    public const string SlowModeWait = "SLOWMODE_WAIT_{0}";

    public const string ChannelPrivate = "CHANNEL_PRIVATE";
    public const string UserAlreadyParticipant = "USER_ALREADY_PARTICIPANT";
    public const string UsersTooMuch = "USERS_TOO_MUCH";

    public const string AuthTokenInvalid = "AUTH_TOKEN_INVALID";
    public const string AuthTokenExpired = "AUTH_TOKEN_EXPIRED";
    public const string AuthTokenAlreadyAccepted = "AUTH_TOKEN_ALREADY_ACCEPTED";

    public const string UserNameInvalid = "USERNAME_INVALID";
    public const string UserNameOccupied = "USERNAME_OCCUPIED";
    public const string MessageEditTimeExpired = "MESSAGE_EDIT_TIME_EXPIRED";
    public const string MessageAuthorRequired = "MESSAGE_AUTHOR_REQUIRED";
    public const string SessionPasswordNeeded = "SESSION_PASSWORD_NEEDED";
}