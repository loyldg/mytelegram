using System.Collections.ObjectModel;
using EventFlow.MongoDB.EventStore;
using EventFlow.MongoDB.SnapshotStores;
using EventFlow.MongoDB.ValueObjects;
using EventFlow.Sagas;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver.Core.Operations;

//using MyTelegram.Domain.Aggregates.Bot;
using DialogReadModel = MyTelegram.ReadModel.MongoDB.DialogReadModel;


namespace MyTelegram.Messenger.NativeAot;

public static class NativeAotExtensions
{

    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(ILayeredService<IAuthorizationConverter>))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(ILayeredService<IChatConverter>))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(ILayeredService<IConfigConverter>))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(ILayeredService<IDialogConverter>))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(ILayeredService<IDifferenceConverter>))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(ILayeredService<IMessageConverter>))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(ILayeredService<IPeerConverter>))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(ILayeredService<IPeerNotifySettingsConverter>))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(ILayeredService<IPhotoConverter>))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(ILayeredService<IPollConverter>))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(ILayeredService<IPremiumPromoConverter>))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(ILayeredService<ISendAsPeerConverter>))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(ILayeredService<ITranslationTextConverter>))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(ILayeredService<IUpdatesConverter>))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(ILayeredService<IUserConverter>))]

    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(LayeredService<IAuthorizationConverter>))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(LayeredService<IChatConverter>))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(LayeredService<IConfigConverter>))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(LayeredService<IDialogConverter>))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(LayeredService<IDifferenceConverter>))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(LayeredService<IMessageConverter>))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(LayeredService<IPeerConverter>))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(LayeredService<IPeerNotifySettingsConverter>))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(LayeredService<IPhotoConverter>))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(LayeredService<IPollConverter>))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(LayeredService<IPremiumPromoConverter>))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(LayeredService<ISendAsPeerConverter>))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(LayeredService<ITranslationTextConverter>))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(LayeredService<IUpdatesConverter>))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(LayeredService<IUserConverter>))]


    //[DynamicDependency(DynamicallyAccessedMemberTypes.All,typeof(MyTelegram.Caching.Redis.CacheManager<CachedFutureSalt>))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All,typeof(MyTelegram.Caching.Redis.CacheManager<string>))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All,typeof(MyTelegram.Caching.Redis.CacheManager<UserCacheItem>))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All,typeof(MyTelegram.Caching.Redis.CacheManager<GlobalPrivacySettingsCacheItem>))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(MongoDbEventPersistence))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(MongoDbSnapshotPersistence))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(MongoDbEventDataModel))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(MyTelegramMessengerServerOptions))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(MongoDB.Bson.Serialization.Serializers.Int64Serializer))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(MongoDB.Bson.Serialization.Serializers.StringSerializer))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(MongoDB.Bson.Serialization.Serializers.Int32Serializer))]

    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(MongoDB.Bson.Serialization.Serializers.GuidSerializer))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(MongoDB.Bson.Serialization.Serializers.BooleanSerializer))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(MongoDB.Bson.Serialization.Serializers.BitArraySerializer))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(MongoDB.Bson.Serialization.Serializers.DoubleSerializer))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(MongoDB.Bson.Serialization.Serializers.ObjectSerializer))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(MongoDB.Bson.Serialization.Serializers.ExpandoObjectSerializer))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(MongoDB.Bson.Serialization.Serializers.NullableSerializer<long>))]
    public static IServiceCollection AddMyNativeAot(this IServiceCollection services)
    { 
        services.AddTransient<ISagaStore, MySagaAggregateStore>();
        //services.AddTransient<IRabbitMqSerializer, NativeAotUtf8JsonRabbitMqSerializer>();
        FixMyTelegramServices(services);
        FixEventFlowServices();
        FixMongodbServices();

        Fix<InMemoryRepository<FileItem, long>>();

        return services;
    }



    private static void FixMyTelegramServices(IServiceCollection services)
    {
        services.AddSingleton<IInMemoryRepository<UserStatus, long>, InMemoryRepository<UserStatus, long>>();
        services.AddSingleton<IInMemoryRepository<CacheLoginToken, long>, InMemoryRepository<CacheLoginToken, long>>();

        Fix(new DialogReadModel());
        Fix(new ReadModel.Impl.DialogReadModel());
        Fix(new Draft(string.Empty, false, null, 0));
    }

    //[DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(GuidSerializer))]
    //[DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(BooleanSerializer))]
    //[DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(StringSerializer))]
    //[DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Int32Serializer))]
    //[DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Int64Serializer))]
    //[DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(ExpandoObjectSerializer))]
    //[DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(ByteSerializer))]
    //[DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(ByteArraySerializer))]

    //#region ReadModels
    //[DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(AppCodeReadModel))]
    //[DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(AuthKeyReadModel))]
    //[DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(BlockedReadModel))]
    //[DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(BotReadModel))]
    //[DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(ChannelFullReadModel))]
    //[DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(ChannelReadModel))]
    //[DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(ChannelMemberReadModel))]
    //[DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(ChatInviteReadModel))]
    //[DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(ChatReadModel))]
    //[DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(ContactReadModel))]
    //[DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(DeviceReadModel))]
    //[DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(DialogFilterReadModel))]
    //[DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(DialogReadModel))]
    //[DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(DraftReadModel))]
    //[DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(EncryptedChatReadModel))]
    //[DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(EncryptedMessageBoxReadModel))]
    //[DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(EncryptedPushUpdatesReadModel))]
    //[DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(FileReadModel))]
    //[DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(ImportedContactReadModel))]
    //[DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(MessageReadModel))]
    //[DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(PeerNotifySettingsReadModel))]
    //[DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(PhoneCallConfigReadModel))]
    //[DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(PrivacyReadModel))]
    //[DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(PtsForAuthKeyIdReadModel))]
    //[DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(PtsReadModel))]
    //[DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(PushDeviceReadModel))]
    //[DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(PushUpdatesReadModel))]
    //[DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(ReadingHistoryReadModel))]
    //[DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(ReplyReadModel))]
    //[DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(RpcResultReadModel))]
    //[DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(UserNameReadModel))]
    //[DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(UserPasswordReadModel))]
    //[DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(UserReadModel))]
    //[DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(PollReadModel))]
    //[DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(PollAnswerVoterReadModel))]
    //[DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(LoginLogReadModel))]
    //[DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(UserReactionReadModel))]
    //[DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(ForumTopicReadModel))]
    //[DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(AccessHashReadModel))]
    //#endregion

    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(MessageView))]

    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(MongoDB.Bson.Serialization.Serializers.Int64Serializer))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(MongoDB.Bson.Serialization.Serializers.StringSerializer))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(MongoDB.Bson.Serialization.Serializers.Int32Serializer))]

    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(MongoDB.Bson.Serialization.Serializers.GuidSerializer))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(MongoDB.Bson.Serialization.Serializers.BooleanSerializer))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(MongoDB.Bson.Serialization.Serializers.BitArraySerializer))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(MongoDB.Bson.Serialization.Serializers.DoubleSerializer))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(MongoDB.Bson.Serialization.Serializers.ObjectSerializer))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(MongoDB.Bson.Serialization.Serializers.ExpandoObjectSerializer))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(MongoDB.Bson.Serialization.Serializers.NullableSerializer<long>))]

    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(NullableSerializer<Guid>))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(NullableSerializer<int>))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(NullableSerializer<long>))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(NullableSerializer<bool>))]

    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(EnumSerializer<>))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(ReadOnlyCollection<>))]

    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(List<int>))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(List<long>))]

    [DynamicDependency(DynamicallyAccessedMemberTypes.All, "EnumUnderlyingTypeSerializer", "MongoDB.Driver")]
    private static void FixMongodbServices()
    {
        //Fix<NullableSerializer<int>>();
        //Fix<NullableSerializer<long>>();
        //Fix<NullableSerializer<bool>>();
        //Fix<NullableSerializer<Guid>>();
        Fix<bool?>();
        Fix<DateTime?>();
        Fix<IEnumerable<bool>>();
        Fix<Func<bool?, bool?, int?, string?, PeerNotifySettings>>();
        Fix<Func<string, bool, int?, int, byte[]?, Draft>>();
        Fix<Func<Peer, string?, int, string, int, Peer?, int, MessageFwdHeader>>();
        Fix<Func<int, int, int, ChatMember>>();
        Fix<Func<int, bool, int, ChatAdminRights, string, ChatAdmin>>();
        Fix<Func<bool, bool, bool, bool, bool, bool, bool, bool, bool, bool, bool, ChatAdminRights>>();
        Fix<Func<bool, bool, bool, bool, bool, bool, bool, bool, bool, bool, bool, bool, int, ChatBannedRights>>();
        //Fix<Func<string, string, BotCommand>>();
        //Fix<EnumerableInterfaceImplementerSerializer<List<int>>>();
        //Fix<EnumerableInterfaceImplementerSerializer<List<int>, int>>();
        //Fix<ImpliedImplementationInterfaceSerializer<IEnumerable<int>, List<int>>>();
        Fix<EnumerableInterfaceImplementerSerializer<List<int>, int>>();
        Fix<EnumerableInterfaceImplementerSerializer<List<long>, long>>();
        Fix<EnumerableInterfaceImplementerSerializer<List<int>>>();
        Fix<EnumerableInterfaceImplementerSerializer<List<long>>>();
        Fix<EnumerableInterfaceImplementerSerializer<List<int>, int>>();
        Fix<ImpliedImplementationInterfaceSerializer<IEnumerable<int>, List<int>>>();
        //Fix(new EnumerableInterfaceImplementerSerializer<List<int>, int>(serializerRegistry: new BsonSerializerRegistry()));

        // count=19
        Fix<EnumSerializer<AccessHashType>>();
        Fix<EnumSerializer<DeviceType>>();
        Fix<EnumSerializer<IdType>>();
        Fix<EnumSerializer<LoadType>>();
        Fix<EnumSerializer<MemberStateChangeType>>();
        Fix<EnumSerializer<MessageActionType>>();
        Fix<EnumSerializer<MessageSubType>>();
        Fix<EnumSerializer<MessageType>>();
        Fix<EnumSerializer<PasswordState>>();
        Fix<EnumSerializer<PeerType>>();
        Fix<EnumSerializer<PhoneCallDiscardReason>>();
        Fix<EnumSerializer<PrivacyType>>();
        Fix<EnumSerializer<PrivacyValueType>>();
        Fix<EnumSerializer<PtsChangeReason>>();
        Fix<EnumSerializer<UpdatesType>>();
        Fix<EnumSerializer<ReactionType>>();
        Fix<EnumSerializer<SendMessageType>>();
        Fix<EnumSerializer<UpdateType>>();
    }

    private static void FixEventFlowServices()
    {
    }

    public static void Fix<T>()
    {
        var item = typeof(T);
        //Console.WriteLine($"Fix native aot issue for type:{item.FullName}");
        item.Fix();
    }

    public static void Fix<T>(T type)
    {
        type?.GetType().Fix();
    }

    public static void Fix(this Type type)
    {
        FixInternal(type);
    }

    private static void FixInternal(Type type)
    {
        if (type.Name == "OnlyForFixNativeIssues")
        {
            Console.WriteLine(type.FullName);
        }

        //Console.WriteLine($"<Type Name=\"{type.FullName}\" Dynamic=\"Required All\" />");
    }
}