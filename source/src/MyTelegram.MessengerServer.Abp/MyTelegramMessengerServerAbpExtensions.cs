using EventFlow.Core.Caching;
using MyTelegram.Domain.EventFlow;
using MyTelegram.MessengerServer.Services.Serialization.SystemTextJson;

namespace MyTelegram.MessengerServer.Abp;

public static class MyTelegramMessengerServerAbpExtensions
{
    public static void UseMyTelegramMessengerServer(this IServiceCollection services,
        Action<IEventFlowOptions>? configure = null)
    {
        services.AddTransient<IMongoDbIndexesCreator, MongoDbIndexesCreator>();
        services.AddEventFlow(options =>
            {
                options.AddDefaults(typeof(MyTelegramMessengerServerExtensions).Assembly);
                options.AddDefaults(typeof(EventFlowExtensions).Assembly);
                options.Configure(c => { c.IsAsynchronousSubscribersEnabled = true; });
                // mongodb
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
            })
            .AddMyTelegramCoreServices()
            .AddMyTelegramHandlerServices()
            .AddMyTelegramMessengerServices()
            .AddMyTelegramIdGeneratorServices()
            .AddMyTelegramAbpServices()
            ;
    }

    private static void AddMyTelegramAbpServices(this IServiceCollection services)
    {
        services.AddSingleton<IIdGenerator, AbpIdGenerator>();
        services.AddTransient<IMediaHelper, AbpMediaHelper>();
        services.AddSingleton<IEventBus, AbpEventBus>();
        services.AddSingleton(typeof(ICacheManager<>), typeof(AbpCacheManager<>));
    }

    private static IServiceCollection AddMyTelegramIdGeneratorServices(this IServiceCollection services)
    {
        services.AddSingleton<IHiLoValueGeneratorCache, HiLoValueGeneratorCache>();
        services.AddTransient<IHiLoValueGeneratorFactory, HiLoValueGeneratorFactory>();
        services.AddSingleton<IHiLoStateBlockSizeHelper, HiLoStateBlockSizeHelper>();
        services.AddSingleton<IHiLoHighValueGenerator, HiLoHighValueGenerator>();

        return services;
    }
}
