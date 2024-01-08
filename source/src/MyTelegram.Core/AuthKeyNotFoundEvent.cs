namespace MyTelegram.Core;

public partial record AuthKeyNotFoundEvent(long AuthKeyId,
    string ConnectionId);