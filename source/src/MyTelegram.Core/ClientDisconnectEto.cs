namespace MyTelegram.Core;

public record ClientDisconnectEto(string ConnectionId,
    long AuthKeyId,
    long SessionId);