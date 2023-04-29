using EventFlow;
using EventFlow.Extensions;
using EventFlow.MongoDB.Extensions;
using MyTelegram.Core;
using MyTelegram.Domain.EventFlow;
using MyTelegram.MessengerServer.Extensions;
using MyTelegram.MessengerServer.Services.Impl;
using MyTelegram.MessengerServer.Services.Interfaces;

namespace MyTelegram.MessengerServer.GrpcService;

public static class MyTelegramMessengerServerGrpcServiceExtensions
{
    public static void UseMyTelegramMessengerGrpcServer(this IServiceCollection services,
        Action<IEventFlowOptions>? configure = null)
    {
        services.AddEventFlow(options =>
            {
                options.UseMongoDbReadModel<ReadModel.MongoDB.ChatReadModel>();
                options.UseMongoDbReadModel<ReadModel.MongoDB.ChannelMemberReadModel>();

                options.AddQueryHandlers(
                    typeof(QueryHandlers.MongoDB.Chat.GetChatByChatIdQueryHandler),
                    typeof(QueryHandlers.MongoDB.Channel.GetChannelMembersByChannelIdQueryHandler)
                );
                configure?.Invoke(options);
            })
            .AddMyTelegramCoreServices()
            .AddMyTelegramHandlerServices()
            .AddMyTelegramMessengerServer()
            .AddMyTelegramGrpcService()
            .AddMyEventFlow()
            .RegisterAllMappers()
            ;
    }

    private static IServiceCollection AddMyTelegramGrpcService(this IServiceCollection services)
    {
        services.AddSingleton<IRpcResultProcessor, RpcResultProcessor>();

        services.RegisterAllMappers();
        return services;
    }
}
