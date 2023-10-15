using EventFlow.Core.Caching;
using EventFlow.MongoDB;
using EventFlow.MongoDB.Extensions;
using EventFlow.MongoDB.ReadStores;
using EventFlow.ReadStores.InMemory.Queries;
using EventFlow.ReadStores.InMemory;
using EventFlow.ReadStores;
using EventFlow.Sagas;
using MyTelegram.Domain.EventFlow;
using MyTelegram.Messenger.NativeAot;
using MyTelegram.Messenger.Services;
using MyTelegram.Messenger.Services.Caching;
using MyTelegram.Messenger.Services.Filters;
using MyTelegram.Messenger.Services.Impl;
using MyTelegram.Messenger.Services.Interfaces;
using MyTelegram.QueryHandlers.MongoDB;
using MyTelegram.ReadModel.InMemory;
using MyTelegram.ReadModel.MongoDB;
using MyTelegram.ReadModel.ReadModelLocators;
using MyTelegram.Services.NativeAot;
using PtsForAuthKeyIdReadModel = MyTelegram.ReadModel.MongoDB.PtsForAuthKeyIdReadModel;
using PtsReadModel = MyTelegram.ReadModel.MongoDB.PtsReadModel;
//using PushUpdatesReadModel = MyTelegram.ReadModel.MongoDB.PushUpdatesReadModel;

namespace MyTelegram.Messenger.Extensions;

public static class MyTelegramMessengerServerExtensions
{
    //public static void ConfigureEventBus(this IEventBus eventBus)
    //{

    //}

    public static void UseMyTelegramMessengerServer(this IServiceCollection services,
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

            //options.AddInMemoryReadModels();
            options.AddMessengerMongoDbReadModel();
            options.AddMongoDbQueryHandlers();

            //options.AddPushUpdatesMongoDbReadModel();


            options.UseSystemTextJson(jsonSerializerOptions =>
            {
                jsonSerializerOptions.AddSingleValueObjects(
                    new SystemTextJsonSingleValueObjectConverter<CacheKey>());
            });
            configure?.Invoke(options);
            //options.UseSpanJson();
        })
            .AddMyTelegramCoreServices()
            .AddMyTelegramHandlerServices()
            .AddMyTelegramMessengerServices()
            .AddMyTelegramIdGeneratorServices()
            .AddMyEventFlow()
            ;
        services.AddSingleton<IJsonSerializer, MyNativeAotSystemTextJsonSerializer>();
        services.AddMyNativeAot();
    }


    public static IServiceCollection AddMyTelegramIdGeneratorServices(this IServiceCollection services)
    {
        services.AddSingleton<IHiLoValueGeneratorCache, HiLoValueGeneratorCache>();
        services.AddTransient<IHiLoValueGeneratorFactory, HiLoValueGeneratorFactory>();
        services.AddSingleton<IHiLoStateBlockSizeHelper, HiLoStateBlockSizeHelper>();
        //services.AddSingleton<IHiLoHighValueGenerator, HiLoHighValueGenerator>();
        services.AddSingleton<IRedisIdGenerator, RedisIdGenerator>();
        //services.AddSingleton<IHiLoHighValueGenerator, RedisHighValueGenerator>();
        services.AddTransient<IHiLoHighValueGenerator, MongoDbHighValueGenerator>();
        services.AddSingleton<IMongoDbIdGenerator, MongoDbIdGenerator>();
        return services;
    }

    public static IServiceCollection AddMyTelegramMessengerServices(this IServiceCollection services)
    {
        services.AddLayeredServices();

        //services.AddSingleton<IJsonContextProvider, MyJsonContextProvider>();
        services.AddSingleton<IMediaHelper, NullMediaHelper>();

        services.AddSingleton<IResponseCacheAppService, ResponseCacheAppService>();
        services.AddSingleton<IUserStatusCacheAppService, UserStatusCacheAppService>();
        services.AddSingleton<ILoginTokenCacheAppService, LoginTokenCacheAppService>();

        //services.AddSingleton<ISrpHelper, SrpHelper>();
        services.AddSingleton<IPrivacyHelper, PrivacyHelper>();
        services.AddSingleton<IPrivacyAppService, PrivacyAppService>();
        //services.AddSingleton<IPasswordAppService, PasswordAppService>();
        services.AddSingleton<IDataCenterHelper, DataCenterHelper>();
        services.AddSingleton<IDataSeeder, DataSeeder>();
        services.AddSingleton<IDialogAppService, DialogAppService>();
        services.AddSingleton<IMessageAppService, MessageAppService>();
        services.AddSingleton<IContactAppService, ContactAppService>();
        services.AddSingleton<IPeerSettingsAppService, PeerSettingsAppService>();
        services.AddSingleton<IChannelMessageViewsAppService, ChannelMessageViewsAppService>();
        services.AddSingleton<IChatEventCacheHelper, ChatEventCacheHelper>();

        services.AddSingleton<IPtsHelper, PtsHelper>();

        // Filter services
        services.AddSingleton<IBloomFilter, MyBloomFilter>();
        services.AddSingleton<ICuckooFilter, MyCuckooFilter>();
        services.AddTransient<IInMemoryFilterDataLoader, InMemoryFilterDataLoader>();

        services.RegisterHandlers(typeof(MyTelegramMessengerServerExtensions).Assembly);
        services.RegisterAllMappers();


        services.AddTransient<IMediaHelper, MediaHelper>();
        services.AddTransient<IIdGenerator, IdGenerator>();

        services.AddSingleton<IJsonContextProvider, MyJsonContextProvider>();
        //services.AddTransient<IRabbitMqSerializer, NativeAotUtf8JsonRabbitMqSerializer>();

        services.AddSingleton<ISagaStore, MySagaAggregateStore>();

        services.AddSingleton(typeof(IDomainEventCacheHelper<>), typeof(DomainEventCacheHelper<>));
        services.AddSingleton<IAccessHashHelper, AccessHashHelper>();
        services.AddSingleton<ILanguageManager, LanguageManager>();
        services.AddTransient<IPhotoAppService, PhotoAppService>();

        services.AddTransient<IDomainEventMessageFactory, DomainEventMessageFactory>();

        services.AddTransient<IOffsetHelper, OffsetHelper>();
        //services.AddTransient<IReadModelCacheStrategy, MyTelegramReadModelCacheStrategy>();
        services.AddSingleton<IBlockCacheAppService, BlockCacheAppService>();

        return services;
    }
}