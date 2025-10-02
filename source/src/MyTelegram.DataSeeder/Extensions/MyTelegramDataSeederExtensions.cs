using MyTelegram.EventFlow.MongoDB.Extensions;
using MyTelegram.QueryHandlers.MongoDB.ChatAdmin;
using MyTelegram.QueryHandlers.MongoDB.User;
using MyTelegram.QueryHandlers.MongoDB.UserName;

namespace MyTelegram.DataSeeder.Extensions;

public static class MyTelegramDataSeederExtensions
{
    public static void AddMyTelegramDataSeeder(this IServiceCollection services,
        Action<IEventFlowOptions>? configure = null)
    {
        services.RegisterMongoDbSerializer();
        services.AddEventFlow(options =>
        {
            options.AddDefaults(typeof(Domain.Specs).Assembly);
            options.UseMongoDbEventStore();

            options.UseMongoDbSnapshotStore();
            options.AddMyTelegramMongoDbReadModel();
            options.AddQueryHandlers(typeof(GetUserByIdQueryHandler),
                typeof(GetAllUserNameQueryHandler),
                typeof(GetChatAdminListQueryHandler)
                );
            options.AddSystemTextJson(jsonSerializerOptions =>
            {
                jsonSerializerOptions.AddSingleValueObjects(
                    new SystemTextJsonSingleValueObjectConverter<CacheKey>());
                jsonSerializerOptions.TypeInfoResolverChain.Add(MyJsonSerializeContext.Default);
                jsonSerializerOptions.TypeInfoResolverChain.Add(MyMessengerJsonContext.Default);
            });
            configure?.Invoke(options);
        });

        services.AddTransient(typeof(IDataSeeder<>), typeof(DataSeeder<>));
        services.RegisterServices();
        services.AddReadModelMongoDbContext();
        services.AddEventStoreMongoDbContext();
    }
}