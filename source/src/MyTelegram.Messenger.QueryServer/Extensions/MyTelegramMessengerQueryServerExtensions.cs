using EventFlow.MongoDB.Extensions;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MyTelegram.Domain.Aggregates.AppCode;
using MyTelegram.Domain.Aggregates.Updates;
using MyTelegram.EventBus.Extensions;
using MyTelegram.EventFlow.MongoDB.Extensions;
using MyTelegram.EventFlow.MongoDB.ReadStores;
using MyTelegram.EventFlow.ReadStores;
using MyTelegram.Messenger.Extensions;
using MyTelegram.Messenger.QueryServer.EventHandlers;
using MyTelegram.QueryHandlers.MongoDB;
using MyTelegram.ReadModel.MongoDB;

namespace MyTelegram.Messenger.QueryServer.Extensions;

public static class MyTelegramMessengerQueryServerExtensions
{

    public static void AddEventHandlers(this IServiceCollection services)
    {
        services.AddSubscription<MessengerQueryDataReceivedEvent, MessengerEventHandler>();
        services.AddSubscription<StickerDataReceivedEvent, MessengerEventHandler>();

        services.AddSubscription<UserIsOnlineEvent, UserIsOnlineEventHandler>();

        services.AddSubscription<DomainEventMessage, DistributedDomainEventHandler>();
        services.AddSubscription<DuplicateCommandEvent, DuplicateOperationExceptionHandler>();
    }

    public static void AddMyTelegramMessengerQueryServer(this IServiceCollection services, Action<IEventFlowOptions>? configure = null)
    {
        services.RegisterServices();
        services.AddEventFlow(options =>
        {
            options.AddMyTelegramMongoDbReadModel();
            options.AddQueryHandlers();
            options.AddEvents(typeof(AppCodeAggregate).Assembly);
            options.AddEventUpgraders();
            options.AddMongoDbQueryHandlers();
            options.AddSubscribers(Assembly.GetEntryAssembly());

            options.AddCommands(
                typeof(CreateRpcResultCommand),
                typeof(CreateUpdatesCommand),
                typeof(DeleteRpcResultCommand)
                );
            options.AddCommandHandlers(
                typeof(CreateRpcResultCommandHandler),
                typeof(CreateUpdatesCommandHandler),
                typeof(DeleteRpcResultCommandHandler)
            );
            options.AddEvents(
                typeof(RpcResultCreatedEvent),
                typeof(UpdatesCreatedEvent),
                typeof(RpcResultDeletedEvent)
                );

            options.UseMongoDbEventStore();
            options.UseMongoDbSnapshotStore();
            options.UseMongoDbReadModel<UpdatesAggregate, UpdatesId, UpdatesReadModel>();
            options.UseMongoDbReadModel<RpcResultAggregate, RpcResultId, RpcResultReadModel>();
            options.UseMongoDbReadModel<PtsAggregate, PtsId, PtsReadModel>();
            options.UseMongoDbReadModel<PtsAggregate, PtsId, PtsForAuthKeyIdReadModel>();
            configure?.Invoke(options);
        });

        services.AddMyTelegramMessengerServices();
        services.AddTransient<IQueryOnlyReadModelStore<IAccessHashReadModel>, MongoDbQueryOnlyReadModelStore<IAccessHashReadModel>>();
        BsonSerializer.RegisterSerializer(typeof(IAccessHashReadModel), new ImpliedImplementationInterfaceSerializer<IAccessHashReadModel, AccessHashReadModel>());

        services.AddReadModelMongoDbContext();
        services.AddEventStoreMongoDbContext<DefaultReadModelMongoDbContext>();
        services.AddEventHandlers();
    }
}