namespace MyTelegram.Core;

public partial record AuthKeyNotFoundEvent(long AuthKeyId,
    string ConnectionId);


public partial record ClientDisconnectedEvent(string ConnectionId,
    long AuthKeyId,
    long SessionId);