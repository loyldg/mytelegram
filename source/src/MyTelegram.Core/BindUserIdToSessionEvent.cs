namespace MyTelegram.Core;
public record BindUserIdToSessionEvent(
    long UserId,
    long AuthKeyId,
    long PermAuthKeyId,
    long AccessHashKeyId
    );