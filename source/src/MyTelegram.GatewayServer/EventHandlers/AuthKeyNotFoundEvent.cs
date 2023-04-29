namespace MyTelegram.GatewayServer.EventHandlers;

[EventName("MyTelegram.Core.AuthKeyNotFoundEvent")]
public record AuthKeyNotFoundEvent(long AuthKeyId,
    string ConnectionId);
