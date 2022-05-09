namespace MyTelegram.ReadModel.InMemory;

public static class MyTelegramReadModelInMemoryExtensions
{
    public static IEventFlowOptions UseMyInMemoryReadStoreFor<TReadModel>(this IEventFlowOptions options) where TReadModel : class, IReadModel
    {
        options.UseInMemoryReadStoreFor<TReadModel>();

        options.ServiceCollection.AddSingleton<IMyInMemoryReadStore<TReadModel>, MyInMemoryReadStore<TReadModel>>();
        options.ServiceCollection.AddSingleton<IInMemoryReadStore<TReadModel>>(r=>r.GetRequiredService<IMyInMemoryReadStore<TReadModel>>());
        options.ServiceCollection.AddTransient<IReadModelStore<TReadModel>>(r => r.GetRequiredService<IInMemoryReadStore<TReadModel>>());

        return options;
    }

    public static IEventFlowOptions UseMyInMemoryReadStoreFor<TReadModel,TReadModelLocator>(this IEventFlowOptions options) where TReadModel : class, IReadModel where TReadModelLocator : IReadModelLocator
    {
        options.UseInMemoryReadStoreFor<TReadModel, TReadModelLocator>();
        options.ServiceCollection.AddSingleton<IMyInMemoryReadStore<TReadModel>, MyInMemoryReadStore<TReadModel>>();
        options.ServiceCollection.AddSingleton<IInMemoryReadStore<TReadModel>>(r => r.GetRequiredService<IMyInMemoryReadStore<TReadModel>>());
        options.ServiceCollection.AddTransient<IReadModelStore<TReadModel>>(r => r.GetRequiredService<IInMemoryReadStore<TReadModel>>());


        return options;
    }

    public static IEventFlowOptions AddInMemoryReadModel(this IEventFlowOptions options)
    {
        options.ServiceCollection
            .AddTransient<IDialogReadModelLocator, DialogReadModelLocator>()
            .AddTransient<IDialogReadModelLocator, DialogReadModelLocator>()
            .AddTransient<IPtsReadModelLocator, PtsReadModelLocator>()
            .AddTransient<IMessageIdLocator, MessageIdLocator>()
            .AddTransient<IUserReadModelLocator, UserReadModelLocator>()
            .AddTransient<IChannelReadModelLocator, ChannelReadModelLocator>()
            .AddTransient<IChannelFullReadModelLocator, ChannelFullReadModelLocator>()
            ;

        options
            .UseMyInMemoryReadStoreFor<AppCodeReadModel>()
            .UseMyInMemoryReadStoreFor<DialogReadModel, IDialogReadModelLocator>()
            .UseMyInMemoryReadStoreFor<MessageReadModel, IMessageIdLocator>()
            .UseMyInMemoryReadStoreFor<PeerNotifySettingsReadModel>()
            .UseMyInMemoryReadStoreFor<PtsReadModel, IPtsReadModelLocator>()
            .UseMyInMemoryReadStoreFor<UserReadModel, IUserReadModelLocator>()
            .UseMyInMemoryReadStoreFor<ChatReadModel>()
            .UseMyInMemoryReadStoreFor<ChannelReadModel, IChannelReadModelLocator>()
            .UseMyInMemoryReadStoreFor<ChannelFullReadModel, IChannelFullReadModelLocator>()
            .UseMyInMemoryReadStoreFor<ChannelMemberReadModel>()
            .UseMyInMemoryReadStoreFor<UserNameReadModel>()
            .UseMyInMemoryReadStoreFor<DeviceReadModel>()
            .UseMyInMemoryReadStoreFor<PushDeviceReadModel>()
            .UseMyInMemoryReadStoreFor<DraftReadModel>()
            .UseMyInMemoryReadStoreFor<ChatInviteReadModel>()
            .UseMyInMemoryReadStoreFor<RpcResultReadModel>()
            .UseMyInMemoryReadStoreFor<UserNameReadModel>()

            .UseMyInMemoryReadStoreFor<PushUpdatesReadModel>()
            .UseMyInMemoryReadStoreFor<PtsForAuthKeyIdReadModel>()
            ;


        return options;
    }
}