namespace MyTelegram.Core;

public partial record UserLoggedOutEvent(long UserId,long TempAuthKeyId,long PermAuthKeyId);