using System.Globalization;

namespace MyTelegram.MessengerServer.Services.Serialization.SpanJson;

public class MySpanJsonEventJsonSerializer : IEventJsonSerializer
{
    private readonly IJsonSerializer _jsonSerializer;
    private readonly IEventDefinitionService _eventDefinitionService;
    private readonly IDomainEventFactory _domainEventFactory;
    public MySpanJsonEventJsonSerializer(
        IJsonSerializer jsonSerializer,
        IEventDefinitionService eventDefinitionService,
        IDomainEventFactory domainEventFactory)
    {
        _jsonSerializer = jsonSerializer;
        _eventDefinitionService = eventDefinitionService;
        _domainEventFactory = domainEventFactory;
    }

    public SerializedEvent Serialize(
        IDomainEvent domainEvent)
    {
        return Serialize(domainEvent.GetAggregateEvent(), domainEvent.Metadata);
    }

    public SerializedEvent Serialize(IAggregateEvent aggregateEvent, IEnumerable<KeyValuePair<string, string>> metadatas)
    {
        var eventDefinition = _eventDefinitionService.GetDefinition(aggregateEvent.GetType());

        var metadata = new Metadata(metadatas
            .Where(kv => kv.Key != MetadataKeys.EventName && kv.Key != MetadataKeys.EventVersion)
            .Concat(new[]
            {
                new KeyValuePair<string, string>(MetadataKeys.EventName, eventDefinition.Name),
                new KeyValuePair<string, string>(MetadataKeys.EventVersion, eventDefinition.Version.ToString(CultureInfo.InvariantCulture)),
            }));
        //var dataJson = _jsonSerializer.Serialize(aggregateEvent, aggregateEvent.GetType(), MyJsonContext.Default);
        var dataJson = _jsonSerializer.Serialize(aggregateEvent);

        var metaJson = _jsonSerializer.Serialize(metadata);

        return new SerializedEvent(
            metaJson,
            dataJson,
            metadata.AggregateSequenceNumber,
            metadata);
    }

    public IDomainEvent Deserialize(string eventJson, string metadataJson)
    {
        var metadata = _jsonSerializer.Deserialize<Metadata>(metadataJson);
        return Deserialize(eventJson, metadata);
    }

    public IDomainEvent Deserialize(string json, IMetadata metadata)
    {
        return Deserialize(metadata.AggregateId, json, metadata);
    }

    public IDomainEvent Deserialize(ICommittedDomainEvent committedDomainEvent)
    {
        var metadata = _jsonSerializer.Deserialize<Metadata>(committedDomainEvent.Metadata);
        return Deserialize(committedDomainEvent.AggregateId, committedDomainEvent.Data, metadata);
    }

    public IDomainEvent<TAggregate, TIdentity> Deserialize<TAggregate, TIdentity>(
        TIdentity id,
        ICommittedDomainEvent committedDomainEvent)
        where TAggregate : IAggregateRoot<TIdentity>
        where TIdentity : IIdentity
    {
        return (IDomainEvent<TAggregate, TIdentity>)Deserialize(committedDomainEvent);
    }

    private IDomainEvent Deserialize(string aggregateId, string json, IMetadata metadata)
    {
        var eventDefinition = _eventDefinitionService.GetDefinition(
            metadata.EventName,
            metadata.EventVersion);

        //var aggregateEvent = (IAggregateEvent)_jsonSerializer.Deserialize(json, eventDefinition.Type, MyJsonContext.Default);
        var aggregateEvent = (IAggregateEvent)_jsonSerializer.Deserialize(json, eventDefinition.Type);

        var domainEvent = _domainEventFactory.Create(
            aggregateEvent,
            metadata,
            aggregateId,
            metadata.AggregateSequenceNumber);

        return domainEvent;
    }
}