
namespace MyTelegram.ReadModel.MongoDB;

public static class MyTelegramServerReadModelMongoDbExtensions
{
    public static IEventFlowOptions AddPushUpdatesMongoDbReadModel(this IEventFlowOptions options)
    {
        options.UseMongoDbReadModel<PtsReadModel, IPtsReadModelLocator>();
        options.UseMongoDbReadModel<PushUpdatesReadModel>();
        options.UseMongoDbReadModel<PtsForAuthKeyIdReadModel>();
        return options;
    }

    public static IEventFlowOptions AddMessengerMongoDbReadModel(this IEventFlowOptions options)
    {
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
            ;
    }
}