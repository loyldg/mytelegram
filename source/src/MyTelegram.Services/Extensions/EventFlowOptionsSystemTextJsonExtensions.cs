using EventFlow;
using EventFlow.Core;
using Microsoft.Extensions.DependencyInjection;
using MyTelegram.Services.NativeAot;
using System.Text.Json;

namespace MyTelegram.Services.Extensions;

public static class EventFlowOptionsSystemTextJsonExtensions
{
    public static IEventFlowOptions AddSystemTextJson(
        this IEventFlowOptions eventFlowOptions,
        Action<JsonSerializerOptions>? configure = default)
    {
        eventFlowOptions.ServiceCollection.AddSystemTextJson(configure);

        return eventFlowOptions;
    }

    public static IServiceCollection AddSystemTextJson(this IServiceCollection services,
        Action<JsonSerializerOptions>? configure = default)
    {
        var serializer = new SystemTextJsonSerializer((jsonOptions) =>
        {
            //jsonOptions.TypeInfoResolver = new PolymorphicTypeResolver();
            jsonOptions.Converters.Add(new BitArrayConverter());
            configure?.Invoke(jsonOptions);
        });
        services.AddSingleton<IJsonSerializer>(serializer);

        return services;
    }
}