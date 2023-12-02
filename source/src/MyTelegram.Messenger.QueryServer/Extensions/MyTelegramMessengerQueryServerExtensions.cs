using EventFlow.Core.Caching;
using EventFlow.MongoDB.Extensions;
using MyTelegram.Domain.Aggregates.Updates;
using MyTelegram.Domain.CommandHandlers.PushUpdates;
using MyTelegram.Domain.CommandHandlers.RpcResult;
using MyTelegram.Domain.CommandHandlers.Updates;
using MyTelegram.Domain.Commands.Updates;
using MyTelegram.Domain.EventFlow;
using MyTelegram.Domain.Events.PushUpdates;
using MyTelegram.Domain.Events.RpcResult;
using MyTelegram.Domain.Events.Updates;
using MyTelegram.Messenger.NativeAot;
using MyTelegram.Messenger.QueryServer.BackgroundServices;
using MyTelegram.Messenger.QueryServer.EventHandlers;
using MyTelegram.Messenger.QueryServer.Services;
using MyTelegram.QueryHandlers.MongoDB;
using MyTelegram.ReadModel.MongoDB;
using MyTelegram.ReadModel.ReadModelLocators;
using MyTelegram.Services.Extensions;
using MyTelegram.Services.NativeAot;

namespace MyTelegram.Messenger.QueryServer.Extensions;

public static class MyTelegramMessengerQueryServerExtensions
{
    public static void ConfigureEventBus(this IEventBus eventBus)
    {
        eventBus.Subscribe<MessengerQueryDataReceivedEvent, MessengerEventHandler>();
        eventBus.Subscribe<StickerDataReceivedEvent, MessengerEventHandler>();
        eventBus.Subscribe<NewPtsMessageHasSentEvent, PtsEventHandler>();
        eventBus.Subscribe<RpcMessageHasSentEvent, PtsEventHandler>();
        eventBus.Subscribe<AcksDataReceivedEvent, PtsEventHandler>();

        eventBus.Subscribe<UserIsOnlineEvent, UserIsOnlineEventHandler>();

        eventBus.Subscribe<DomainEventMessage, DistributedDomainEventHandler>();
        eventBus.Subscribe<DuplicateCommandEvent, DuplicateOperationExceptionHandler>();
    }

    public static void AddMyTelegramMessengerQueryServer(this IServiceCollection services, Action<IEventFlowOptions>? configure = null)
    {
        services.AddTransient<PtsEventHandler>();
        services.AddTransient<MessengerEventHandler>();
        services.AddTransient<UserIsOnlineEventHandler>();
        services.AddTransient<DistributedDomainEventHandler>();
        services.AddTransient<DuplicateOperationExceptionHandler>();
        services.AddTransient<IPtsForAuthKeyIdReadModelLocator, PtsForAuthKeyIdReadModelLocator>();

        services.AddEventFlow(options =>
        {
            options.AddMessengerMongoDbReadModel();
            options.AddQueryHandlers();
            options.AddEvents(typeof(MyTelegram.Domain.Aggregates.AppCode.AppCodeAggregate).Assembly);
            options.AddEventUpgraders();
            options.AddMongoDbQueryHandlers();
            options.AddSubscribers(Assembly.GetEntryAssembly());

            options.AddCommands(
                typeof(CreateRpcResultCommand),
                typeof(CreatePushUpdatesCommand),
                typeof(CreateUpdatesCommand),
                typeof(CreateEncryptedPushUpdatesCommand),
                typeof(UpdatePtsCommand),
                typeof(PtsAckedCommand),
                typeof(IncrementTempPtsCommand),
                typeof(UpdateQtsCommand),
                typeof(UpdatePtsForAuthKeyIdCommand),
                typeof(UpdateGlobalSeqNoCommand)
                //typeof(CreatePtsCommand)
                );
            options.AddCommandHandlers(
                typeof(CreateRpcResultCommandHandler),
                typeof(CreateUpdatesCommandHandler),
                typeof(CreateEncryptedPushUpdatesCommandHandler),
                typeof(UpdatePtsCommandHandler),
                typeof(PtsAckedCommandHandler),
                //typeof(IncrementTempPtsCommandHandler),
                typeof(UpdateQtsCommandHandler),
                typeof(UpdatePtsForAuthKeyIdCommandHandler),
                typeof(UpdateGlobalSeqNoCommandHandler)
            );
            options.AddEvents(
                typeof(EncryptedPushUpdatesCreatedEvent),
                typeof(PushUpdatesCreatedEvent),
                typeof(RpcResultCreatedEvent),
                typeof(UpdatesCreatedEvent),
                typeof(PtsUpdatedEvent),
                typeof(PtsAckedEvent),
                typeof(TempPtsIncrementedEvent),
                typeof(QtsUpdatedEvent),
                typeof(PtsForAuthKeyIdUpdatedEvent),
                typeof(PtsGlobalSeqNoUpdatedEvent)
                );

            options.AddSnapshots(typeof(PtsSnapshot));


            options.UseMongoDbEventStore();
            options.UseMongoDbSnapshotStore();
            //options.AddPushUpdatesMongoDbReadModel();

            //options.UseMongoDbReadModelWithContext<PtsReadModel, IPtsReadModelLocator, PushReadModelMongoDbContext>();
            //options.UseMongoDbReadModel<PushUpdatesAggregate, PushUpdatesId, PushUpdatesReadModel, PushReadModelMongoDbContext>();
            options.UseMongoDbReadModel<UpdatesAggregate, UpdatesId, UpdatesReadModel, PushReadModelMongoDbContext>();
            //options.UseMongoDbReadModel<PtsAggregate, PtsId, PtsForAuthKeyIdReadModel, PushReadModelMongoDbContext>();
            options.UseMongoDbReadModel<RpcResultAggregate, RpcResultId, RpcResultReadModel, PushReadModelMongoDbContext>();
            options.UseMongoDbReadModel<PtsAggregate, PtsId, PtsReadModel, PushReadModelMongoDbContext>();
            options
                .UseMongoDbReadModel<PtsAggregate, PtsId, PtsForAuthKeyIdReadModel, PushReadModelMongoDbContext>();

            //options.UseMongoDbReadModelWithContext<PtsReadModel, IPtsReadModelLocator, PushReadModelMongoDbContext>();

            //options.UseMongoDbReadModel<BotAggregate, BotId, ReadModel.MongoDB.BotReadModel, BotMongoDbContext>();

            //options.AddMyMongoDbReadModelServices();
             
            configure?.Invoke(options);
        });

        services.AddMyTelegramCoreServices();
        services.AddMyTelegramHandlerServices();
        services.AddMyTelegramMessengerServices();
        services.AddMyEventFlow();
        services.AddMyTelegramIdGeneratorServices();

        services.AddSingleton<PushReadModelMongoDbContext>();

        //services.AddSingleton<IPtsHelper, QueryServerPtsHelper>();
        services.AddTransient<IMongoDbIndexesCreator, QueryServerMongoDbIndexesCreator>();
        //services.AddTransient<IReadModelCacheStrategy, MyTelegramReadModelCacheStrategy>();
        //services.AddTransient<IReadModelUpdateStrategy, QueryServerReadModelUpdateStrategy>();
        //services.AddTransient<IReadModelUpdateManager, MyTelegramQueryServerReadModelUpdateManager>();

        services.AddSingleton(typeof(ICommandExecutor<,,>), typeof(CommandExecutor<,,>));

        services.AddSystemTextJson(options =>
        {
            options.AddSingleValueObjects();
            options.TypeInfoResolverChain.Add(MyJsonSerializeContext.Default);
            options.TypeInfoResolverChain.Add(MyMessengerJsonContext.Default);
        });

        services.AddMyNativeAot();

    }
}