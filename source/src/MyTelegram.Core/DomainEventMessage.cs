namespace MyTelegram.Core;

public record DomainEventMessage(string EventId, string Message, IReadOnlyDictionary<string, string> Headers);