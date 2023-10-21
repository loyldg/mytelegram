using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using MyTelegram.Core;
using MyTelegram.Services.Services;

namespace MyTelegram.Services.Extensions
{
    public static class MyTelegramHandlerExtensions
    {
        public static IServiceCollection AddMyTelegramHandlerServices(this IServiceCollection services)
        {
            services.AddSingleton<IAckCacheService, AckCacheService>();
            services.AddTransient<IGZipHelper, GZipHelper>();
            services.AddSingleton<IMessageIdGenerator, MessageIdGenerator>();
            services.AddSingleton<IScheduleAppService, ScheduleAppService>();
            services.AddSingleton<IObjectMessageSender, QueuedObjectMessageSender>();

            services.AddTransient<IExceptionProcessor, ExceptionProcessor>();
            services.AddTransient<IPeerHelper, PeerHelper>();
            services.AddSingleton<IHandlerHelper, HandlerHelper>();
            services.AddSingleton<IRpcResultCacheAppService, RpcResultCacheAppService>();

            services.AddSingleton(typeof(IInMemoryRepository<,>), typeof(InMemoryRepository<,>));
            services.AddTransient(typeof(IDataProcessor<>), typeof(DefaultDataProcessor<>));
            services.AddSingleton(typeof(IMessageQueueProcessor<>), typeof(MessageQueueProcessor2<>));
            services.AddTransient<IDataProcessor<ISessionMessage>, SessionMessageDataProcessor>();

            services.AddSingleton<ICacheSerializer, CacheSerializer>();
            services.AddSingleton<IInvokeAfterMsgProcessor, InvokeAfterMsgProcessor>();

            return services;
        }

        public static IServiceCollection RegisterHandlers(this IServiceCollection services, Assembly handlerImplTypeInThisAssembly)
        {
            var baseType = typeof(IObjectHandler);
            //var baseInterface = typeof(IProcessedHandler);
            var types = handlerImplTypeInThisAssembly.DefinedTypes
                .Where(p => baseType.IsAssignableFrom(p) /*&& baseInterface.IsAssignableFrom(p)*/ && !p.IsAbstract)
                .ToList();
            foreach (var typeInfo in types)
            {
                services.AddTransient(typeInfo);
            }

            return services;
        }
    }
}
