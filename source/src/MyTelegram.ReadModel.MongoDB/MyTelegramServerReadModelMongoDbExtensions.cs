using EventFlow.ReadStores;
using MyTelegram.EventFlow.MongoDB;
using MyTelegram.Domain.Aggregates.PeerSettings;
using MyTelegram.Domain.Aggregates.Photo;
using MongoDB.Bson.Serialization.Conventions;

namespace MyTelegram.ReadModel.MongoDB;

public static class MyTelegramServerReadModelMongoDbExtensions
{
    //public static IEventFlowOptions AddMyMongoDbReadModel(/*this IServiceCollection services*/ this IEventFlowOptions options)
    //{
    //    // default readmodel dbcontext
    //    options.ServiceCollection.AddSingleton<DefaultReadModelMongoDbContext>();
    //    options.ServiceCollection.AddSingleton<IMongoDbContext, DefaultReadModelMongoDbContext>();
    //    options.ServiceCollection.AddSingleton(typeof(IMongoDbContextFactory<>),
    //        typeof(DefaultMongoDbContextFactory<>));

    //    //options.ServiceCollection.AddTransient(typeof(IMongoDbReadModelStore<>), typeof(MyMongoDbReadModelStore<>));
    //    //options.ServiceCollection.AddTransient(typeof(IReadModelStore<>), typeof(IMongoDbReadModelStore<>));

    //    //options.ServiceCollection.AddTransient(typeof(IMongoDbReadModelStore<>), typeof(MyMongoDbReadModelStore<>));
    //    //options.ServiceCollection.AddTransient(typeof(IMyMongoDbReadModelStore<>), typeof(MyMongoDbReadModelStore<>));

    //    return options;
    //}

    public static IEventFlowOptions AddPushUpdatesMongoDbReadModel(this IEventFlowOptions options)
    {
        //options.UseMongoDbReadModel<PtsReadModel, IPtsReadModelLocator>();
        //options.UseMongoDbReadModel<PushUpdatesAggregate, PushUpdatesId, PushUpdatesReadModel>();
        //options.UseMongoDbReadModel<PtsAggregate, PtsId, PtsForAuthKeyIdReadModel>();
        //options.UseMongoDbReadModel<EncryptedChatAggregate, EncryptedChatId, EncryptedPushUpdatesReadModel>();
        return options;
    }

    public static IEventFlowOptions AddMessengerMongoDbReadModel(this IEventFlowOptions options)
    {
        //options.ServiceCollection.AddSingleton<IMongoDbIndexesCreator, MongoDbIndexesCreator>();
        var pack = new ConventionPack
        {
            new IgnoreExtraElementsConvention(true)
        };
        ConventionRegistry.Register("IgnoreExtraElements", pack, _ => true);
        options.AddMyMongoDbReadModel();

        options.ServiceCollection
            .AddTransient<IDialogReadModelLocator, DialogReadModelLocator>()
            .AddTransient<IMessageIdLocator, MessageIdLocator>()
            .AddTransient<IPtsReadModelLocator, PtsReadModelLocator>()
            .AddTransient<IReplyReadModelLocator, ReplyReadModelLocator>()
            .AddTransient<IUserReadModelLocator, UserReadModelLocator>()
            //.AddTransient<IDeviceReadModelLocator, DeviceReadModelLocator>()
            .AddTransient<IChannelReadModelLocator, ChannelReadModelLocator>()
            .AddTransient<IChannelFullReadModelLocator, ChannelFullReadModelLocator>()
            .AddTransient<IPollAnswerVoterReadModelLocator, PollAnswerVoterReadModelLocator>()
            .AddTransient<IAccessHashReadModelLocator, AccessHashReadModelLocator>()
            .AddTransient<IChatAdminReadModelLocator, ChatAdminReadModelLocator>()
            .AddTransient<IChatInviteImporterReadModelLocator, ChatInviteImporterReadModelLocator>()
            ;


        return options.AddDefaults(typeof(MyTelegramServerReadModelMongoDbExtensions).Assembly)
            .UseMongoDbReadModel<AppCodeAggregate, AppCodeId, AppCodeReadModel>()
            .UseMongoDbReadModel<DialogReadModel, IDialogReadModelLocator>()
            .UseMongoDbReadModel<MessageReadModel, IMessageIdLocator>()
            .UseMongoDbReadModel<PeerNotifySettingsAggregate, PeerNotifySettingsId, PeerNotifySettingsReadModel>()
            //.UseMongoDbReadModel<PtsReadModel, IPtsReadModelLocator>()
            .UseMongoDbReadModel<UserReadModel, IUserReadModelLocator>()
            //.UseMongoDbReadModel<BotAggregate, BotId, BotReadModel>()
            .UseMongoDbReadModel<ChatAggregate, ChatId, ChatReadModel>()
            .UseMongoDbReadModel<ChannelReadModel, IChannelReadModelLocator>()
            .UseMongoDbReadModel<ChannelFullReadModel, IChannelFullReadModelLocator>()
            .UseMongoDbReadModel<ChannelMemberAggregate, ChannelMemberId, ChannelMemberReadModel>()
            .UseMongoDbReadModel<UserNameAggregate, UserNameId, UserNameReadModel>()
            .UseMongoDbReadModel<DeviceAggregate, DeviceId, DeviceReadModel>()
            .UseMongoDbReadModel<PushDeviceAggregate, PushDeviceId, PushDeviceReadModel>()
            .UseMongoDbReadModel<DialogAggregate, DialogId, DraftReadModel>()            
            .UseMongoDbReadModel<ReadingHistoryAggregate, ReadingHistoryId, ReadingHistoryReadModel>()
            //.UseMongoDbReadModel<RpcResultAggregate, RpcResultId, RpcResultReadModel>()
            .UseMongoDbReadModel<ReplyReadModel, IReplyReadModelLocator>()

            .UseMongoDbReadModel<DialogFilterAggregate, DialogFilterId, DialogFilterReadModel>()
            .UseMongoDbReadModel<PollAggregate, PollId, PollReadModel>()
            .UseMongoDbReadModel<PollAnswerVoterReadModel, IPollAnswerVoterReadModelLocator>()
            .UseMongoDbReadModel<AccessHashReadModel, IAccessHashReadModelLocator>()
            .UseMongoDbReadModel<PeerSettingsAggregate, PeerSettingsId, PeerSettingsReadModel>()
            .UseMongoDbReadModel<ChatAdminReadModel, IChatAdminReadModelLocator>()
            .UseMongoDbReadModel<ChatInviteImporterReadModel, IChatInviteImporterReadModelLocator>()

            .UseMongoDbReadModel<ChatInviteAggregate, ChatInviteId, ChatInviteReadModel>()
            // photo created by file server
            .UseMongoDbReadModel<PhotoAggregate, PhotoId, PhotoReadModel>()

            .UseMongoDbReadModel<ContactAggregate, ContactId, ContactReadModel>()
            .UseMongoDbReadModel<ImportedContactAggregate, ImportedContactId, ImportedContactReadModel>()

            ;
    }
}
