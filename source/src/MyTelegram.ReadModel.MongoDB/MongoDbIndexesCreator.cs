namespace MyTelegram.ReadModel.MongoDB;

public interface IMongoDbIndexesCreator
{
    Task CreateAllIndexesAsync();
}

public abstract class MongoDbIndexesCreatorBase : IMongoDbIndexesCreator
{
    private readonly IMongoDatabase _database;
    private readonly IReadModelDescriptionProvider _descriptionProvider;
    private readonly IMongoDbEventPersistenceInitializer _eventPersistenceInitializer;

    protected MongoDbIndexesCreatorBase(IMongoDatabase database,
        IReadModelDescriptionProvider descriptionProvider,
        IMongoDbEventPersistenceInitializer eventPersistenceInitializer)
    {
        _database = database;
        _descriptionProvider = descriptionProvider;
        _eventPersistenceInitializer = eventPersistenceInitializer;
    }

    public async Task CreateAllIndexesAsync()
    {
        _eventPersistenceInitializer.Initialize();
        var snapShotCollectionName = "snapShots";
        await CreateIndexAsync<MongoDbSnapshotDataModel>(p => p.AggregateId, snapShotCollectionName)
            .ConfigureAwait(false);
        await CreateIndexAsync<MongoDbSnapshotDataModel>(p => p.AggregateName, snapShotCollectionName)
            .ConfigureAwait(false);
        await CreateIndexAsync<MongoDbSnapshotDataModel>(p => p.AggregateSequenceNumber, snapShotCollectionName)
            .ConfigureAwait(false);

        await CreateAllIndexesCoreAsync().ConfigureAwait(false);
    }

    protected abstract Task CreateAllIndexesCoreAsync();

    protected async Task CreateIndexAsync<TReadModel>(Expression<Func<TReadModel, object>> field)
        where TReadModel : IMongoDbReadModel
    {
        var indexDefine = Builders<TReadModel>.IndexKeys.Ascending(field);
        var collectionName = _descriptionProvider.GetReadModelDescription<TReadModel>().RootCollectionName;
        await _database.GetCollection<TReadModel>(collectionName.Value).Indexes
            .CreateOneAsync(new CreateIndexModel<TReadModel>(indexDefine)).ConfigureAwait(false);
    }

    protected async Task CreateIndexAsync<TSnapshot>(Expression<Func<TSnapshot, object>> field,
        string collectionName)
    {
        var indexDefine = Builders<TSnapshot>.IndexKeys.Ascending(field);
        await _database.GetCollection<TSnapshot>(collectionName).Indexes
            .CreateOneAsync(new CreateIndexModel<TSnapshot>(indexDefine)).ConfigureAwait(false);
    }
}

public class MongoDbIndexesCreator : MongoDbIndexesCreatorBase
{
    public MongoDbIndexesCreator(IMongoDatabase database,
        IReadModelDescriptionProvider descriptionProvider,
        IMongoDbEventPersistenceInitializer eventPersistenceInitializer) : base(database,
        descriptionProvider,
        eventPersistenceInitializer)
    {
    }

    protected override async Task CreateAllIndexesCoreAsync()
    {
        await CreateIndexAsync<DialogReadModel>(p => p.OwnerId).ConfigureAwait(false);
        await CreateIndexAsync<DialogReadModel>(p => p.Pinned).ConfigureAwait(false);

        await CreateIndexAsync<MessageReadModel>(p => p.MessageId).ConfigureAwait(false);
        await CreateIndexAsync<MessageReadModel>(p => p.OwnerPeerId).ConfigureAwait(false);
        await CreateIndexAsync<MessageReadModel>(p => p.MessageType).ConfigureAwait(false);
        await CreateIndexAsync<MessageReadModel>(p => p.Pinned).ConfigureAwait(false);
        await CreateIndexAsync<MessageReadModel>(p => p.Pts).ConfigureAwait(false);
        await CreateIndexAsync<MessageReadModel>(p => p.ToPeerType).ConfigureAwait(false);
        await CreateIndexAsync<MessageReadModel>(p => p.SendMessageType).ConfigureAwait(false);
        await CreateIndexAsync<MessageReadModel>(p => p.ReplyToMsgId).ConfigureAwait(false);

        await CreateIndexAsync<UserReadModel>(p => p.UserId).ConfigureAwait(false);
        await CreateIndexAsync<UserReadModel>(p => p.FirstName).ConfigureAwait(false);
        await CreateIndexAsync<ChannelReadModel>(p => p.ChannelId).ConfigureAwait(false);
        await CreateIndexAsync<ChannelFullReadModel>(p => p.ChannelId).ConfigureAwait(false);
        await CreateIndexAsync<ChannelMemberReadModel>(p => p.ChannelId).ConfigureAwait(false);
        await CreateIndexAsync<ChannelMemberReadModel>(p => p.UserId).ConfigureAwait(false);
        await CreateIndexAsync<ChannelMemberReadModel>(p => p.Kicked).ConfigureAwait(false);
        await CreateIndexAsync<ChannelMemberReadModel>(p => p.IsBot).ConfigureAwait(false);
        //await CreateIndexAsync<AuthKeyReadModel>(p => p.TempAuthKeyId);

        await CreateIndexAsync<DeviceReadModel>(p => p.PermAuthKeyId).ConfigureAwait(false);
        await CreateIndexAsync<DeviceReadModel>(p => p.UserId).ConfigureAwait(false);
        await CreateIndexAsync<DeviceReadModel>(p => p.IsActive).ConfigureAwait(false);


        //await CreateIndexAsync<FileReadModel>(p => p.UserId).ConfigureAwait(false);
        //await CreateIndexAsync<FileReadModel>(p => p.FileId).ConfigureAwait(false);
        //await CreateIndexAsync<FileReadModel>(p => p.ServerFileId).ConfigureAwait(false);
        //await CreateIndexAsync<FileReadModel>(p => p.FileReference).ConfigureAwait(false);

        await CreateIndexAsync<UserNameReadModel>(p => p.UserName).ConfigureAwait(false);
        await CreateIndexAsync<UserNameReadModel>(p => p.PeerId).ConfigureAwait(false);

        await CreateIndexAsync<PushUpdatesReadModel>(p => p.PeerId).ConfigureAwait(false);
        await CreateIndexAsync<PushUpdatesReadModel>(p => p.Pts).ConfigureAwait(false);
        await CreateIndexAsync<PushUpdatesReadModel>(p => p.PtsType).ConfigureAwait(false);
        await CreateIndexAsync<PushUpdatesReadModel>(p => p.SeqNo).ConfigureAwait(false);

        await CreateIndexAsync<ReadingHistoryReadModel>(p => p.MessageId).ConfigureAwait(false);
        await CreateIndexAsync<ReadingHistoryReadModel>(p => p.TargetPeerId).ConfigureAwait(false);

        await CreateIndexAsync<PtsReadModel>(p => p.PeerId).ConfigureAwait(false);
        await CreateIndexAsync<PtsForAuthKeyIdReadModel>(p => p.PeerId).ConfigureAwait(false);
        await CreateIndexAsync<PtsForAuthKeyIdReadModel>(p => p.PermAuthKeyId).ConfigureAwait(false);
        await CreateIndexAsync<PtsForAuthKeyIdReadModel>(p => p.GlobalSeqNo).ConfigureAwait(false);
        await CreateIndexAsync<PtsForAuthKeyIdReadModel>(p => p.Pts).ConfigureAwait(false);

        await CreateIndexAsync<RpcResultReadModel>(p => p.SourceId).ConfigureAwait(false);
        await CreateIndexAsync<RpcResultReadModel>(p => p.PeerId).ConfigureAwait(false);

        await CreateIndexAsync<ReplyReadModel>(p => p.SavedFromPeerId).ConfigureAwait(false);
        await CreateIndexAsync<ReplyReadModel>(p => p.SavedFromMsgId).ConfigureAwait(false);

        var snapShotCollectionName = "snapShots";
        await CreateIndexAsync<MongoDbSnapshotDataModel>(p => p.AggregateId, snapShotCollectionName)
            .ConfigureAwait(false);
        await CreateIndexAsync<MongoDbSnapshotDataModel>(p => p.AggregateName, snapShotCollectionName)
            .ConfigureAwait(false);
        await CreateIndexAsync<MongoDbSnapshotDataModel>(p => p.AggregateSequenceNumber, snapShotCollectionName)
            .ConfigureAwait(false);
    }
}