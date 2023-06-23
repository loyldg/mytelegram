using System.Globalization;

namespace MyTelegram.MessengerServer.Services.Serialization.SystemTextJson;

public class MyEventJsonSerializer : IEventJsonSerializer
{
    private readonly IDomainEventFactory _domainEventFactory;
    private readonly IEventDefinitionService _eventDefinitionService;
    private readonly ISystemTextJsonSerializer _jsonSerializer;

    public MyEventJsonSerializer(
        ISystemTextJsonSerializer jsonSerializer,
        IEventDefinitionService eventDefinitionService,
        IDomainEventFactory domainEventFactory)
    {
        _jsonSerializer = jsonSerializer;
        _eventDefinitionService = eventDefinitionService;
        _domainEventFactory = domainEventFactory;
    }

    public IDomainEvent Deserialize(string eventJson,
        string metadataJson)
    {
        var metadata = _jsonSerializer.Deserialize(metadataJson, MyJsonContext.Default.Metadata);
        if (metadata == null)
            throw new InvalidOperationException($"Deserialized metadata is null,metadata={metadataJson}");

        return Deserialize(eventJson, metadata);
    }

    public IDomainEvent Deserialize(string json,
        IMetadata metadata)
    {
        return Deserialize(metadata.AggregateId, json, metadata);
    }

    public IDomainEvent Deserialize(ICommittedDomainEvent committedDomainEvent)
    {
        var metadata = _jsonSerializer.Deserialize(committedDomainEvent.Metadata, MyJsonContext.Default.Metadata);
        if (metadata == null)
            throw new InvalidOperationException(
                $"Deserialized metadata is null,metadata={committedDomainEvent.Metadata}");

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

    public SerializedEvent Serialize(
        IDomainEvent domainEvent)
    {
        return Serialize(domainEvent.GetAggregateEvent(), domainEvent.Metadata);
    }

    public SerializedEvent Serialize(IAggregateEvent aggregateEvent,
        IEnumerable<KeyValuePair<string, string>> metadatas)
    {
        var eventDefinition = _eventDefinitionService.GetDefinition(aggregateEvent.GetType());

        var metadata = new Metadata(metadatas
            .Where(kv => kv.Key != MetadataKeys.EventName && kv.Key != MetadataKeys.EventVersion)
            .Concat(new[]
            {
                new KeyValuePair<string, string>(MetadataKeys.EventName, eventDefinition.Name),
                new KeyValuePair<string, string>(MetadataKeys.EventVersion,
                    eventDefinition.Version.ToString(CultureInfo.InvariantCulture))
            }));
        var dataJson = _jsonSerializer.Serialize(aggregateEvent);
        //var dataJson = _jsonSerializer.Serialize(aggregateEvent, aggregateEvent.GetType(), _jsonContextProvider.GetSerializerContext());

        var metaJson = _jsonSerializer.Serialize(metadata, MyJsonContext.Default.Metadata);

        return new SerializedEvent(
            metaJson,
            dataJson,
            metadata.AggregateSequenceNumber,
            metadata);
    }

    private IDomainEvent Deserialize(string aggregateId,
        string json,
        IMetadata metadata)
    {
        var eventDefinition = _eventDefinitionService.GetDefinition(
            metadata.EventName,
            metadata.EventVersion);

        var aggregateEvent = (IAggregateEvent)_jsonSerializer.Deserialize(json, eventDefinition.Type);
        //var aggregateEvent = (IAggregateEvent)_jsonSerializer.Deserialize(json, eventDefinition.Type, _jsonContextProvider.GetSerializerContext());

        var domainEvent = _domainEventFactory.Create(
            aggregateEvent,
            metadata,
            aggregateId,
            metadata.AggregateSequenceNumber);

        return domainEvent;
    }
}