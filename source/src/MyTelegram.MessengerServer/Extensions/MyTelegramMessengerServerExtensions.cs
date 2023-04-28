using MyTelegram.MessengerServer.Services.IdGenerator;
using MyTelegram.MessengerServer.Services.Serialization.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventFlow.Core.Caching;
using EventFlow.MongoDB.Extensions;
using MyTelegram.Caching.Redis;
using MyTelegram.Domain.EventFlow;
using MyTelegram.EventBus.RabbitMQ;
using MyTelegram.QueryHandlers.MongoDB;
using MyTelegram.ReadModel.MongoDB;
using MyTelegram.MessengerServer.BackgroundServices;
using IEventBus = MyTelegram.EventBus.IEventBus;
using MyTelegram.MessengerServer.EventHandlers;
using PtsEventHandler = MyTelegram.MessengerServer.DomainEventHandlers.PtsEventHandler;

namespace MyTelegram.MessengerServer.Extensions
{
    internal static class MyTelegramMessengerServerExtensions
    {
        public static void ConfigureEventBus(this IEventBus eventBus)
        {
            eventBus.Subscribe<MessengerDataReceivedEvent, MessengerEventHandler>();
            eventBus.Subscribe<NewDeviceCreatedEvent, MessengerEventHandler>();
            eventBus.Subscribe<BindUidToAuthKeyIntegrationEvent, MessengerEventHandler>();
            eventBus.Subscribe<AuthKeyUnRegisteredIntegrationEvent, MessengerEventHandler>();
            eventBus.Subscribe<DuplicateCommandEvent, MessengerEventHandler>();
            eventBus.Subscribe<StickerDataReceivedEvent, MessengerEventHandler>();

            eventBus.Subscribe<NewPtsMessageHasSentEvent, EventHandlers.PtsEventHandler>();
            eventBus.Subscribe<RpcMessageHasSentEvent, EventHandlers.PtsEventHandler>();
            eventBus.Subscribe<AcksDataReceivedEvent, EventHandlers.PtsEventHandler>();

            eventBus.Subscribe<UserIsOnlineEvent, UserIsOnlineEventHandler>();
        }

        public static IServiceCollection AddMyTelegramMessengerServer(this IServiceCollection services,
       Action<IEventFlowOptions>? configure = null)
        {
            services.AddTransient<IMongoDbIndexesCreator, MongoDbIndexesCreator>();
            services.AddEventFlow(options =>
            {
                options.AddDefaults(typeof(MyTelegramMessengerServerExtensions).Assembly);
                options.AddDefaults(typeof(EventFlowExtensions).Assembly);
                options.Configure(c => { c.IsAsynchronousSubscribersEnabled = true; });

                options.UseMongoDbEventStore();
                options.UseMongoDbSnapshotStore();

                options.AddMessengerMongoDbReadModel();
                options.AddMongoDbQueryHandlers();
                options.AddPushUpdatesMongoDbReadModel();

                options.UseSystemTextJson(jsonSerializerOptions =>
                {
                    jsonSerializerOptions.AddSingleValueObjects(
                        new SystemTextJsonSingleValueObjectConverter<CacheKey>());
                });
                configure?.Invoke(options);
                //options.UseSpanJson();
            });

            services.AddMyTelegramRabbitMqEventBus();
            services.AddMyTelegramCoreServices();
            services.AddMyTelegramHandlerServices();
            services.AddMyEventFlow();
            services.AddMyTelegramMessengerServices();
            services.AddMyTelegramEventHandlers();
            services.AddMyTelegramIdGeneratorServices();
            

            services.AddHostedService<MyTelegramMessengerServerInitBackgroundService>();
            services.AddHostedService<DataProcessorBackgroundService>();
            services.AddHostedService<ObjectMessageSenderBackgroundService>();
            services.AddHostedService<InvokeAfterMsgProcessorBackgroundService>();

            return services;
        }

        private static IServiceCollection AddMyTelegramEventHandlers(this IServiceCollection services)
        {
            services.AddTransient<EventHandlers.MessengerEventHandler>();
            services.AddTransient<EventHandlers.PtsEventHandler>();
            services.AddTransient<EventHandlers.UserIsOnlineEventHandler>();

            return services;
        }

        private static IServiceCollection AddMyTelegramIdGeneratorServices(this IServiceCollection services)
        {
            services.AddSingleton<IHiLoValueGeneratorCache, HiLoValueGeneratorCache>();
            services.AddTransient<IHiLoValueGeneratorFactory, HiLoValueGeneratorFactory>();
            services.AddSingleton<IHiLoStateBlockSizeHelper, HiLoStateBlockSizeHelper>();
            services.AddSingleton<IHiLoHighValueGenerator, HiLoHighValueGenerator>();

            return services;
        }

        private static IServiceCollection AddMyTelegramMessengerServices(this IServiceCollection services)
        {
            services.AddTransient<IMediaHelper, MediaHelper>();
            services.AddTransient<IIdGenerator, IdGenerator>();
            services.AddSingleton<IResponseCacheAppService, ResponseCacheAppService>();
            services.AddSingleton<IInvokeAfterMsgProcessor, InvokeAfterMsgProcessor>();
            services.AddSingleton<IUserStatusCacheAppService, UserStatusCacheAppService>();
            services.AddSingleton<ILoginTokenCacheAppService, LoginTokenCacheAppService>();

            services.AddTransient<IDataCenterHelper, DataCenterHelper>();
            services.AddTransient<IDataSeeder, DataSeeder>();
            services.AddTransient<IDialogAppService, DialogAppService>();
            services.AddTransient<IMessageAppService, MessageAppService>();
            services.AddTransient<IContactAppService, ContactAppService>();
            services.AddTransient<IPeerSettingsAppService, PeerSettingsAppService>();
            services.AddTransient<IChannelMessageViewsAppService, ChannelMessageViewsAppService>();
            services.AddTransient<IRpcResultProcessor, RpcResultProcessor>();
            services.AddSingleton<ICreatedChatCacheHelper, CreatedChatCacheHelper>();

            services.AddSingleton<IPtsHelper, PtsHelper>();

            services.RegisterHandlers(typeof(MyTelegramMessengerServerExtensions).Assembly);
            services.RegisterAllMappers();

            return services;
        }
    }
}
