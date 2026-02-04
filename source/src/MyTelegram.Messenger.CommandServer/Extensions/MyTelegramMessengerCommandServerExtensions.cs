using EventFlow.Core.Caching;
using EventFlow.MongoDB.Extensions;
using MyTelegram.EventBus.Extensions;
using MyTelegram.EventFlow.MongoDB.Extensions;
using MyTelegram.Messenger.CommandServer.EventHandlers;
using MyTelegram.Messenger.CommandServer.DomainEventHandlers;
using MyTelegram.Messenger.NativeAot;
using MyTelegram.QueryHandlers.MongoDB;
using MyTelegram.ReadModel.MongoDB;
using MyTelegram.Services.Extensions;

namespace MyTelegram.Messenger.CommandServer.Extensions;
public static class MyTelegramMessengerCommandServerExtensions
{
    public static void AddEventHandlers(this IServiceCollection services)
    {
        services.AddSubscription<MessengerCommandDataReceivedEvent, MessengerEventHandler>();
        services.AddSubscription<NewDeviceCreatedEvent, MessengerEventHandler>();
        services.AddSubscription<BindUserIdToAuthKeyIntegrationEvent, MessengerEventHandler>();
        services.AddSubscription<AuthKeyUnRegisteredIntegrationEvent, MessengerEventHandler>();

        services.AddSubscription<NewPtsMessageHasSentEvent, MyTelegram.Messenger.CommandServer.EventHandlers.PtsEventHandler>();
        services.AddSubscription<RpcMessageHasSentEvent, MyTelegram.Messenger.CommandServer.EventHandlers.PtsEventHandler>();
        services.AddSubscription<AcksDataReceivedEvent, MyTelegram.Messenger.CommandServer.EventHandlers.PtsEventHandler>();

    }

    public static void AddMyTelegramMessengerCommandServer(this IServiceCollection services,
        Action<IEventFlowOptions>? configure = null)
    {
        services.RegisterServices();
        services.AddMyTelegramMessengerServices();

        services.AddEventFlow(options =>
        {
            options.AddDefaults(typeof(MyTelegramMessengerServerExtensions).Assembly);
            options.AddDefaults(typeof(MyTelegram.Domain.Specs).Assembly);
            options.Configure(c => { c.IsAsynchronousSubscribersEnabled = true; });

            options.UseMongoDbEventStore();
            options.UseMongoDbSnapshotStore();

            options.AddMyTelegramMongoDbReadModel();
            options.AddMongoDbQueryHandlers();

            options.AddSystemTextJson(jsonSerializerOptions =>
            {
                jsonSerializerOptions.AddSingleValueObjects(
                    new EventFlow.SystemTextJsonSingleValueObjectConverter<CacheKey>());
                jsonSerializerOptions.TypeInfoResolverChain.Add(MyMessengerJsonContext.Default);
            });
            configure?.Invoke(options);
        });
		services.AddEventStoreMongoDbContext();
        services.AddReadModelMongoDbContext();
        services.AddEventHandlers();

        // Register admin command handler for official account
        services.AddTransient<OfficialAccountCommandHandler>();
    }
}
