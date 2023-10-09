namespace MyTelegram.Messenger.Services.Interfaces;

public class DomainEventMessageFactory : IDomainEventMessageFactory
{
    private readonly IEventJsonSerializer _eventJsonSerializer;

    public DomainEventMessageFactory(IEventJsonSerializer eventJsonSerializer)
    {
        _eventJsonSerializer = eventJsonSerializer;
    }

    public DomainEventMessage CreateDomainEventMessage(IDomainEvent domainEvent)
    {
        var serializedEvent = _eventJsonSerializer.Serialize(
            domainEvent.GetAggregateEvent(),
            domainEvent.Metadata);

        return new DomainEventMessage(domainEvent.Metadata.EventId.Value, serializedEvent.SerializedData,
            domainEvent.Metadata);
    }
}