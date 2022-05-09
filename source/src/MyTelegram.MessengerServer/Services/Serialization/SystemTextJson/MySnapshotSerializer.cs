namespace MyTelegram.MessengerServer.Services.Serialization.SystemTextJson;

public class MySnapshotSerializer : ISnapshotSerilizer// ISnapshotSerializer
{
    private readonly ISystemTextJsonSerializer _jsonSerializer;
    private readonly ILogger<MySnapshotSerializer> _logger;
    private readonly ISnapshotDefinitionService _snapshotDefinitionService;
    private readonly ISnapshotUpgradeService _snapshotUpgradeService;

    public MySnapshotSerializer(
        ILogger<MySnapshotSerializer> logger,
        ISystemTextJsonSerializer jsonSerializer,
        ISnapshotUpgradeService snapshotUpgradeService,
        ISnapshotDefinitionService snapshotDefinitionService)
    {
        _logger = logger;
        _jsonSerializer = jsonSerializer;
        _snapshotUpgradeService = snapshotUpgradeService;
        _snapshotDefinitionService = snapshotDefinitionService;
    }

    public async Task<SnapshotContainer> DeserializeAsync<TAggregate, TIdentity, TSnapshot>(
        CommittedSnapshot committedSnapshot,
        CancellationToken cancellationToken)
        where TAggregate : ISnapshotAggregateRoot<TIdentity, TSnapshot>
        where TIdentity : IIdentity
        where TSnapshot : ISnapshot
    {
        if (committedSnapshot == null)
        {
            throw new ArgumentNullException(nameof(committedSnapshot));
        }

        var metadata = _jsonSerializer.Deserialize(committedSnapshot.SerializedMetadata, MyJsonContext.Default.SnapshotMetadata);

        if (metadata == null)
        {
            throw new InvalidOperationException($"Deserialized metadata is null,metadata={committedSnapshot.SerializedMetadata}");
        }

        var snapshotDefinition =
            _snapshotDefinitionService.GetDefinition(metadata.SnapshotName, metadata.SnapshotVersion);

        _logger.LogTrace(
            "Deserializing snapshot named {SnapshotName} v{SnapshotVersion} for '{AggregateType}' v{AggregateVersion}",
            snapshotDefinition.Name,
            snapshotDefinition.Version,
            typeof(TAggregate).PrettyPrint(),
            metadata.AggregateSequenceNumber);

        var snapshot =
            (ISnapshot)_jsonSerializer.Deserialize(committedSnapshot.SerializedData, snapshotDefinition.Type);
        var upgradedSnapshot =
            await _snapshotUpgradeService.UpgradeAsync(snapshot, cancellationToken).ConfigureAwait(false);

        return new SnapshotContainer(upgradedSnapshot, metadata);
    }

    public Task<SerializedSnapshot> SerializeAsync<TAggregate, TIdentity, TSnapshot>(
        SnapshotContainer snapshotContainer,
        CancellationToken cancellationToken)
        where TAggregate : ISnapshotAggregateRoot<TIdentity, TSnapshot>
        where TIdentity : IIdentity
        where TSnapshot : ISnapshot
    {
        var snapshotDefinition = _snapshotDefinitionService.GetDefinition(typeof(TSnapshot));

        _logger.LogTrace(
            "Building snapshot {SnapshotName} v{SnapshotVersion} for {AggregateType}",
            snapshotDefinition.Name,
            snapshotDefinition.Version,
            typeof(TAggregate).PrettyPrint());

        var updatedSnapshotMetadata = new SnapshotMetadata(snapshotContainer.Metadata.Concat(
            new Dictionary<string, string> {
                { SnapshotMetadataKeys.SnapshotName, snapshotDefinition.Name },
                { SnapshotMetadataKeys.SnapshotVersion, snapshotDefinition.Version.ToString() }
            }));

        var serializedMetadata =
            _jsonSerializer.Serialize(updatedSnapshotMetadata, MyJsonContext.Default.SnapshotMetadata);
        var serializedData = _jsonSerializer.Serialize(snapshotContainer.Snapshot);

        return Task.FromResult(new SerializedSnapshot(
            serializedMetadata,
            serializedData,
            updatedSnapshotMetadata));
    }
}