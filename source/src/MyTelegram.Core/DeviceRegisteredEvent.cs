namespace MyTelegram.Core;

public partial record DeviceRegisteredEvent(long AuthKeyId,long PermAuthKeyId,long SessionId);