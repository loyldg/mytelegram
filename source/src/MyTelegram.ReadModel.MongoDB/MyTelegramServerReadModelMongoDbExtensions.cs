using MongoDB.Bson.Serialization.Conventions;
using MyTelegram.EventFlow.MongoDB;
using MyTelegram.ReadModel.Impl;

namespace MyTelegram.ReadModel.MongoDB;

public static class MyTelegramServerReadModelMongoDbExtensions
{
    public static IEventFlowOptions AddMessengerMongoDbReadModel(this IEventFlowOptions options)
    {
        var pack = new ConventionPack
        {
            new IgnoreExtraElementsConvention(true)
        };
        ConventionRegistry.Register("IgnoreExtraElements", pack, _ => true);

        options.ServiceCollection
            .AddTransient<IDialogReadModelLocator, DialogReadModelLocator>()
            .AddTransient<IDialogReadModelLocator, DialogReadModelLocator>()
            .AddTransient<IMessageIdLocator, MessageIdLocator>()
            .AddTransient<IPtsReadModelLocator, PtsReadModelLocator>()
            .AddTransient<IReplyReadModelLocator, ReplyReadModelLocator>()
            .AddTransient<IUserReadModelLocator, UserReadModelLocator>()
            .AddTransient<IChannelReadModelLocator, ChannelReadModelLocator>()
            .AddTransient<IChannelFullReadModelLocator, ChannelFullReadModelLocator>()
            .AddTransient<IPollAnswerVoterReadModelLocator, PollAnswerVoterReadModelLocator>()
            .AddTransient<IAccessHashReadModelLocator, AccessHashReadModelLocator>()
            .AddTransient<IChatAdminReadModelLocator, ChatAdminReadModelLocator>()
            ;

        return options.AddDefaults(typeof(MyTelegramServerReadModelMongoDbExtensions).Assembly)
                .UseMongoDbReadModel<AppCodeReadModel>()
                .UseMongoDbReadModel<DialogReadModel, IDialogReadModelLocator>()
                .UseMongoDbReadModel<MessageReadModel, IMessageIdLocator>()
                .UseMongoDbReadModel<PeerNotifySettingsReadModel>()
                .UseMongoDbReadModel<PtsReadModel, IPtsReadModelLocator>()
                .UseMongoDbReadModel<UserReadModel, IUserReadModelLocator>()
                .UseMongoDbReadModel<ChatReadModel>()
                .UseMongoDbReadModel<ChannelReadModel, IChannelReadModelLocator>()
                .UseMongoDbReadModel<ChannelFullReadModel, IChannelFullReadModelLocator>()
                .UseMongoDbReadModel<ChannelMemberReadModel>()
                .UseMongoDbReadModel<UserNameReadModel>()
                .UseMongoDbReadModel<DeviceReadModel>()
                .UseMongoDbReadModel<PushDeviceReadModel>()
                .UseMongoDbReadModel<DraftReadModel>()
                .UseMongoDbReadModel<ChatInviteReadModel>()
                //.UseMongoDbReadModel<FileReadModel>()
                .UseMongoDbReadModel<ReadingHistoryReadModel>()
                .UseMongoDbReadModel<RpcResultReadModel>()
                .UseMongoDbReadModel<ReplyReadModel, IReplyReadModelLocator>()
                .UseMongoDbReadModel<DialogFilterReadModel>()
                .UseMongoDbReadModel<PollAggregate, PollId, PollReadModel>()
                .UseMongoDbReadModel<PollAnswerVoterReadModel, IPollAnswerVoterReadModelLocator>()
                .UseMongoDbReadModel<AccessHashReadModel, IAccessHashReadModelLocator>()
                .UseMongoDbReadModel<PeerSettingsAggregate, PeerSettingsId, PeerSettingsReadModel>()
                .UseMongoDbReadModel<ChatAdminReadModel, IChatAdminReadModelLocator>()
                .UseMongoDbReadModel<PhotoAggregate, PhotoId, PhotoReadModel>()
            ;
    }

    public static IEventFlowOptions AddPushUpdatesMongoDbReadModel(this IEventFlowOptions options)
    {
        options.UseMongoDbReadModel<PtsReadModel, IPtsReadModelLocator>();
        options.UseMongoDbReadModel<PushUpdatesReadModel>();
        options.UseMongoDbReadModel<PtsForAuthKeyIdReadModel>();
        return options;
    }
}
