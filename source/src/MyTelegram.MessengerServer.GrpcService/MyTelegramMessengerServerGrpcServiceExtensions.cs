using EventFlow.Extensions;
using EventFlow.MongoDB.Extensions;
using MyTelegram.MessengerServer.Extensions;
using MyTelegram.MessengerServer.Services.Impl;
using MyTelegram.MessengerServer.Services.Interfaces;
using MyTelegram.ReadModel.MongoDB;
using GetChannelMembersByChannelIdQueryHandler = MyTelegram.QueryHandlers.MongoDB.Channel.GetChannelMembersByChannelIdQueryHandler;
using GetChatByChatIdQueryHandler = MyTelegram.QueryHandlers.MongoDB.Chat.GetChatByChatIdQueryHandler;

namespace MyTelegram.MessengerServer.GrpcService;

public static class MyTelegramMessengerServerGrpcServiceExtensions
{
    public static IServiceCollection AddMyTelegramGrpcService(this IServiceCollection services)
    {
        services.AddSingleton<IRpcResultProcessor, RpcResultProcessor>();

        services.RegisterAllMappers();
        return services;
    }

    public static IServiceCollection AddMongoDbGrpcServiceEventFlow(this IServiceCollection services)
    {
        services.AddEventFlow(options => {
            options.UseMongoDbReadModel<ChatReadModel>();
            options.UseMongoDbReadModel<ChannelMemberReadModel>();

            options.AddQueryHandlers(
                typeof(GetChatByChatIdQueryHandler),
                typeof(GetChannelMembersByChannelIdQueryHandler)
            );
            options.ConfigureMongoDb("tg-abp-messenger");
        });
        return services;
    }
}
