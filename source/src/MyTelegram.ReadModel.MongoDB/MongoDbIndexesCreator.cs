namespace MyTelegram.ReadModel.MongoDB;

public class MongoDbIndexesCreator(
    IMongoDatabase database,
    IReadModelDescriptionProvider descriptionProvider,
    IMongoDbEventPersistenceInitializer eventPersistenceInitializer)
    : MongoDbIndexesCreatorBase(database,
        descriptionProvider,
        eventPersistenceInitializer), ITransientDependency
{
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

        await CreateIndexAsync<UserReadModel>(p => p.UserId);
        await CreateIndexAsync<UserReadModel>(p => p.PhoneNumber);
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

        await CreateIndexAsync<ContactReadModel>(p => p.SelfUserId);
        await CreateIndexAsync<ContactReadModel>(p => p.TargetUserId);
        //await CreateIndexAsync<FileReadModel>(p => p.UserId);
        //await CreateIndexAsync<FileReadModel>(p => p.FileId);
        //await CreateIndexAsync<FileReadModel>(p => p.ServerFileId);
        //await CreateIndexAsync<FileReadModel>(p => p.FileReference);

        await CreateIndexAsync<UserNameReadModel>(p => p.UserName);
        await CreateIndexAsync<UserNameReadModel>(p => p.PeerId);

        //await CreateIndexAsync<PushUpdatesReadModel>(p => p.PeerId);
        //await CreateIndexAsync<PushUpdatesReadModel>(p => p.Pts);
        //await CreateIndexAsync<PushUpdatesReadModel>(p => p.SeqNo);

        await CreateIndexAsync<ReadingHistoryReadModel>(p => p.MessageId);
        await CreateIndexAsync<ReadingHistoryReadModel>(p => p.TargetPeerId);

        await CreateIndexAsync<PtsReadModel>(p => p.PeerId);
        await CreateIndexAsync<PtsForAuthKeyIdReadModel>(p => p.PeerId);
        await CreateIndexAsync<PtsForAuthKeyIdReadModel>(p => p.PermAuthKeyId);
        await CreateIndexAsync<PtsForAuthKeyIdReadModel>(p => p.GlobalSeqNo);
        await CreateIndexAsync<PtsForAuthKeyIdReadModel>(p => p.Pts);

        await CreateIndexAsync<RpcResultReadModel>(p => p.ReqMsgId);

        await CreateIndexAsync<ReplyReadModel>(p => p.ChannelId);
        await CreateIndexAsync<ReplyReadModel>(p => p.MessageId);

        await CreateIndexAsync<DialogFilterReadModel>(p => p.OwnerUserId);
        await CreateIndexAsync<PollReadModel>(p => p.ToPeerId);
        await CreateIndexAsync<PollReadModel>(p => p.PollId);
        await CreateIndexAsync<PollAnswerVoterReadModel>(p => p.PollId);
        await CreateIndexAsync<PollAnswerVoterReadModel>(p => p.Option);

        await CreateIndexAsync<LanguageReadModel>(p => p.LanguageCode);
        await CreateIndexAsync<LanguageTextReadModel>(p => p.LanguageCode);
        await CreateIndexAsync<LanguageTextReadModel>(p => p.Platform);

		await CreateIndexAsync<UserConfigReadModel>(p => p.UserId);
        await CreateIndexAsync<UserConfigReadModel>(p => p.Key);

        await CreateIndexAsync<MessageTokenReadModel>(p => p.OwnerPeerId);
        await CreateIndexAsync<MessageTokenReadModel>(p => p.ToPeerId);
        await CreateIndexAsync<MessageTokenReadModel>(p => p.MessageId);
        await CreateIndexAsync<MessageTokenReadModel>(p => p.Tokens);

        var snapShotCollectionName = "snapShots";
        await CreateIndexAsync<MongoDbSnapshotDataModel>(p => p.AggregateId, snapShotCollectionName);
        await CreateIndexAsync<MongoDbSnapshotDataModel>(p => p.AggregateName, snapShotCollectionName);
        await CreateIndexAsync<MongoDbSnapshotDataModel>(p => p.AggregateSequenceNumber, snapShotCollectionName);
    }
}
