using MyTelegram.MessengerServer.Services.IdGenerator;

namespace MyTelegram.MessengerServer.Extensions;

public static class MyTelegramHandlerExtensions
{
    public static IServiceCollection AddMyTelegramHandlerServices(this IServiceCollection services)
    {
        services.AddSingleton<IAckCacheService, AckCacheService>();
        services.AddTransient<IGZipHelper, GZipHelper>();
        services.AddSingleton<IMessageIdGenerator, MessageIdGenerator>();
        services.AddSingleton<IScheduleAppService, ScheduleAppService>();
        //services.AddTransient<IObjectMessageSender, ObjectMessageSender>();
        services.AddSingleton<IObjectMessageSender, QueuedObjectMessageSender>();
        services.AddSingleton<IChatEventCacheHelper, ChatEventCacheHelper>();
        services.AddTransient<IExceptionProcessor, ExceptionProcessor>();
        services.AddTransient<IPeerHelper, PeerHelper>();
        services.AddSingleton<IHandlerHelper, HandlerHelper>();
        services.AddSingleton<IRpcResultCacheAppService, RpcResultCacheAppService>();

        services.AddSingleton(typeof(IInMemoryRepository<,>), typeof(InMemoryRepository<,>));
        services.AddTransient(typeof(IDataProcessor<>), typeof(DefaultDataProcessor<>));
        //services.AddSingleton(typeof(IMessageQueueProcessor<>), typeof(MessageQueueProcessor<>));
        services.AddSingleton(typeof(IMessageQueueProcessor<>), typeof(MessageQueueProcessor2<>));
        services.AddTransient<IDataProcessor<ISessionMessage>, SessionMessageDataProcessor>();

        //services.AddSingleton<IDomainEventFactory, MyDomainEventFactory>();

        // Filter services
        services.AddSingleton<IBloomFilter, MyBloomFilter>();
        services.AddSingleton<ICuckooFilter, MyCuckooFilter>();
        services.AddTransient<IInMemoryFilterDataLoader, InMemoryFilterDataLoader>();
        services.AddBloomFilter(options => { options.UseInMemory(); });

        services.AddTransient<ITlMessageConverter, TlMessageConverter>();
        services.AddTransient<ITlUpdatesConverter, TlUpdatesConverter>();
        services.AddTransient<ITlChatConverter, TlChatConverter>();
        services.AddTransient<ITlUserConverter, TlUserConverter>();
        services.AddTransient<ITlPhotoConverter, TlPhotoConverter>();
        services.AddTransient<ITlAuthorizationConverter, TlAuthorizationConverter>();
        services.AddTransient<ITlDifferenceConverter, TlDifferenceConverter>();
        services.AddTransient<ITlDialogConverter, TlDialogConverter>();
        services.AddTransient<ITlPeerConverter, TlPeerConverter>();
        services.AddTransient<ITlPollConverter, TlPollConverter>();

        // IdGenerator services
        services.AddSingleton<IHiLoValueGeneratorCache, HiLoValueGeneratorCache>();
        services.AddTransient<IHiLoValueGeneratorFactory, HiLoValueGeneratorFactory>();
        services.AddSingleton<IHiLoStateBlockSizeHelper, HiLoStateBlockSizeHelper>();
        services.AddTransient<IIdGenerator, DefaultIdGenerator>();

        return services;
    }

    public static IServiceCollection RegisterHandlers(this IServiceCollection services,
        Assembly handlerImplTypeInThisAssembly)
    {
        var baseType = typeof(IObjectHandler);
        var baseInterface = typeof(IProcessedHandler);
        var types = handlerImplTypeInThisAssembly.DefinedTypes
            .Where(p => baseType.IsAssignableFrom(p) && baseInterface.IsAssignableFrom(p) && !p.IsAbstract)
            .ToList();
        foreach (var typeInfo in types) services.AddTransient(typeInfo);

        return services;
    }
}