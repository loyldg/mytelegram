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
            ;
        await CreateIndexAsync<MongoDbSnapshotDataModel>(p => p.AggregateName, snapShotCollectionName)
            ;
        await CreateIndexAsync<MongoDbSnapshotDataModel>(p => p.AggregateSequenceNumber, snapShotCollectionName)
            ;

        await CreateAllIndexesCoreAsync();
    }

    protected abstract Task CreateAllIndexesCoreAsync();

    protected async Task CreateIndexAsync<TReadModel>(Expression<Func<TReadModel, object>> field)
        where TReadModel : IMongoDbReadModel
    {
        var indexDefine = Builders<TReadModel>.IndexKeys.Ascending(field);
        var collectionName = _descriptionProvider.GetReadModelDescription<TReadModel>().RootCollectionName;
        await _database.GetCollection<TReadModel>(collectionName.Value).Indexes
            .CreateOneAsync(new CreateIndexModel<TReadModel>(indexDefine));
    }

    protected async Task CreateIndexAsync<TSnapshot>(Expression<Func<TSnapshot, object>> field,
        string collectionName)
    {
        var indexDefine = Builders<TSnapshot>.IndexKeys.Ascending(field);
        await _database.GetCollection<TSnapshot>(collectionName).Indexes
            .CreateOneAsync(new CreateIndexModel<TSnapshot>(indexDefine));
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
        await CreateIndexAsync<DialogReadModel>(p => p.OwnerId);
        await CreateIndexAsync<DialogReadModel>(p => p.Pinned);

        await CreateIndexAsync<MessageReadModel>(p => p.MessageId);
        await CreateIndexAsync<MessageReadModel>(p => p.OwnerPeerId);
        await CreateIndexAsync<MessageReadModel>(p => p.MessageType);
        await CreateIndexAsync<MessageReadModel>(p => p.Pinned);
        await CreateIndexAsync<MessageReadModel>(p => p.Pts);
        await CreateIndexAsync<MessageReadModel>(p => p.ToPeerType);
        await CreateIndexAsync<MessageReadModel>(p => p.SendMessageType);
        //await CreateIndexAsync<MessageReadModel>(p => p.ReplyToMsgId);

        await CreateIndexAsync<UserReadModel>(p => p.UserId);
        await CreateIndexAsync<UserReadModel>(p => p.FirstName);
        await CreateIndexAsync<ChannelReadModel>(p => p.ChannelId);
        await CreateIndexAsync<ChannelFullReadModel>(p => p.ChannelId);
        await CreateIndexAsync<ChannelMemberReadModel>(p => p.ChannelId);
        await CreateIndexAsync<ChannelMemberReadModel>(p => p.UserId);
        await CreateIndexAsync<ChannelMemberReadModel>(p => p.Kicked);
        await CreateIndexAsync<ChannelMemberReadModel>(p => p.IsBot);
        //await CreateIndexAsync<AuthKeyReadModel>(p => p.TempAuthKeyId);

        await CreateIndexAsync<DeviceReadModel>(p => p.PermAuthKeyId);
        await CreateIndexAsync<DeviceReadModel>(p => p.UserId);
        await CreateIndexAsync<DeviceReadModel>(p => p.IsActive);


        //await CreateIndexAsync<FileReadModel>(p => p.UserId);
        //await CreateIndexAsync<FileReadModel>(p => p.FileId);
        //await CreateIndexAsync<FileReadModel>(p => p.ServerFileId);
        //await CreateIndexAsync<FileReadModel>(p => p.FileReference);

        await CreateIndexAsync<UserNameReadModel>(p => p.UserName);
        await CreateIndexAsync<UserNameReadModel>(p => p.PeerId);

        await CreateIndexAsync<PushUpdatesReadModel>(p => p.PeerId);
        await CreateIndexAsync<PushUpdatesReadModel>(p => p.Pts);
        await CreateIndexAsync<PushUpdatesReadModel>(p => p.PtsType);
        await CreateIndexAsync<PushUpdatesReadModel>(p => p.SeqNo);

        await CreateIndexAsync<ReadingHistoryReadModel>(p => p.MessageId);
        await CreateIndexAsync<ReadingHistoryReadModel>(p => p.TargetPeerId);

        await CreateIndexAsync<PtsReadModel>(p => p.PeerId);
        await CreateIndexAsync<PtsForAuthKeyIdReadModel>(p => p.PeerId);
        await CreateIndexAsync<PtsForAuthKeyIdReadModel>(p => p.PermAuthKeyId);
        await CreateIndexAsync<PtsForAuthKeyIdReadModel>(p => p.GlobalSeqNo);
        await CreateIndexAsync<PtsForAuthKeyIdReadModel>(p => p.Pts);

        await CreateIndexAsync<RpcResultReadModel>(p => p.SourceId);
        await CreateIndexAsync<RpcResultReadModel>(p => p.PeerId);

        await CreateIndexAsync<ReplyReadModel>(p => p.SavedFromPeerId);
        await CreateIndexAsync<ReplyReadModel>(p => p.SavedFromMsgId);

        await CreateIndexAsync<DialogFilterReadModel>(p => p.OwnerUserId);
        await CreateIndexAsync<PollReadModel>(p => p.ToPeerId);
        await CreateIndexAsync<PollReadModel>(p => p.PollId);
        await CreateIndexAsync<PollAnswerVoterReadModel>(p => p.PollId);
        await CreateIndexAsync<PollAnswerVoterReadModel>(p => p.Option);
        var snapShotCollectionName = "snapShots";
        await CreateIndexAsync<MongoDbSnapshotDataModel>(p => p.AggregateId, snapShotCollectionName)
            ;
        await CreateIndexAsync<MongoDbSnapshotDataModel>(p => p.AggregateName, snapShotCollectionName)
            ;
        await CreateIndexAsync<MongoDbSnapshotDataModel>(p => p.AggregateSequenceNumber, snapShotCollectionName)
            ;
    }
}