using Microsoft.Extensions.DependencyInjection;
using Rebus.Config;
using Rebus.Handlers;
using Rebus.Pipeline;
using Rebus.Pipeline.Receive;
using Rebus.Serialization.Json;
using System.Text.Json;

namespace MyTelegram.EventBus.Rebus;

public static class Extensions
{
    public static IServiceCollection AddRebusEventBus(this IServiceCollection services, Action<RebusConfigurer>? configureAction = null)
    {
        services.AddSingleton<RebusEventBus>();
        services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
        services.AddSingleton<IEventHandlerInvoker, EventHandlerInvoker>();

        services.AddTransient<IEventBus, RebusEventBus>();
        services.AddTransient(typeof(IHandleMessages<>), typeof(RebusDistributedEventHandlerAdapter<>));
        services.AddRebus(configure =>
        {
            configure.Options(options =>
            {
                options.Decorate<IPipeline>(d =>
                {
                    var step = new RebusEventHandlerStep();
                    var pipeline = d.Get<IPipeline>();

                    return new PipelineStepInjector(pipeline).OnReceive(step, PipelineRelativePosition.After,
                        typeof(ActivateHandlersStep));
                });
            });
            //configure.Serialization(x => x.UseMsgPack());
            //MessagePack.MessagePackSerializer.Deserialize(new ReadOnlySequence<byte>())
            configureAction?.Invoke(configure);
            return configure;
        });

        return services;
    }

    public static void AddSystemTextJson(this RebusConfigurer rebusConfigurer,
        Action<JsonSerializerOptions>? configureAction = null)
    {
        var jsonOptions = new JsonSerializerOptions(JsonSerializerOptions.Default);
        configureAction?.Invoke(jsonOptions);
        Console.WriteLine($"jsonOptions.TypeInfoResolverChain.Count:{jsonOptions.TypeInfoResolverChain.Count}");
        
        rebusConfigurer.Serialization(x => x.UseSystemTextJson(jsonOptions));
    }
}