
//using MyTelegram.MessengerServer.Services.BloomFilter.InMemory;

namespace MyTelegram.MessengerServer;

public static class MyTelegramMessengerServerExtensions
{
    public static IServiceCollection AddMyTelegramMessengerServices(this IServiceCollection services)
    {
        //services.AddSingleton<IJsonContextProvider, MyJsonContextProvider>();
        services.AddTransient<IMediaHelper, NullMediaHelper>();

        //services.AddSingleton<IBloomFilter, InMemoryBloomFilter>();
        //services.AddSingleton<IDeletableBloomFilter, DeletableBloomFilter>();
        //services.AddSingleton<ICuckooFilter, InMemoryCuckooFilter>();

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