namespace MyTelegram.Core;

public partial record ClientDisconnectedEvent(string ConnectionId,
    long AuthKeyId,
    long SessionId);