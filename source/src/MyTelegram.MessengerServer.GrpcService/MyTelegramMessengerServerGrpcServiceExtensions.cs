using EventFlow;
using EventFlow.Extensions;
using EventFlow.MongoDB.Extensions;
using MyTelegram.Domain.EventFlow;
using MyTelegram.QueryHandlers.MongoDB.Channel;
using MyTelegram.QueryHandlers.MongoDB.Chat;
using MyTelegram.ReadModel.MongoDB;

namespace MyTelegram.MessengerServer.GrpcService;

public static class MyTelegramMessengerServerGrpcServiceExtensions
{
    private static IServiceCollection AddMyTelegramGrpcService(this IServiceCollection services)
    {
        return services;
    }

    public static void UseMyTelegramMessengerGrpcServer(this IServiceCollection services,
        Action<IEventFlowOptions>? configure = null)
    {
        services.AddEventFlow(options =>
            {
                options.UseMongoDbReadModel<ChatReadModel>();
                options.UseMongoDbReadModel<ChannelMemberReadModel>();

                options.AddQueryHandlers(
                    typeof(GetChatByChatIdQueryHandler),
                    typeof(GetChannelMembersByChannelIdQueryHandler)
                );
                configure?.Invoke(options);
            })
            //.AddMyTelegramCoreServices()
            //.AddMyTelegramHandlerServices()
            //.AddMyTelegramMessengerServer()
            .AddMyTelegramGrpcService()
            .AddMyEventFlow()
            //.RegisterAllMappers()
            ;
    }
}
