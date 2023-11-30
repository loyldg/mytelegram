//using System.Text.Json;
//using EventFlow;
//using EventFlow.Core;
//using Microsoft.Extensions.DependencyInjection;

//namespace MyTelegram.Services.NativeAot;

//public static class EventFlowOptionsSystemTextJsonExtensions
//{
//    public static IEventFlowOptions UseSystemTextJson(
//        this IEventFlowOptions eventFlowOptions,
//        Action<JsonSerializerOptions>? configure = default)
//    {
//        eventFlowOptions.ServiceCollection.AddSingleton<IJsonSerializer>(new SystemTextJsonSerializer(configure));
//        eventFlowOptions.ServiceCollection.AddSingleton<ISystemTextJsonSerializer, SystemTextJsonSerializer>();

//        //eventFlowOptions.ServiceCollection.AddSingleton<IEventJsonSerializer, MyEventJsonSerializer>();
//        //eventFlowOptions.ServiceCollection.AddSingleton<ISnapshotSerializer, MySnapshotSerializer>();

//        return eventFlowOptions;
//    }

//    public static IEventFlowOptions UseSystemTextJson(
//        this IEventFlowOptions eventFlowOptions,
//        JsonSerializerOptions? options = default)
//    {
//        eventFlowOptions.ServiceCollection.AddSingleton(options ?? new JsonSerializerOptions());
//        eventFlowOptions.ServiceCollection.AddSingleton<ISystemTextJsonSerializer, SystemTextJsonSerializer>();
//        eventFlowOptions.ServiceCollection.AddSingleton<IJsonSerializer>(new SystemTextJsonSerializer());

//        //eventFlowOptions.ServiceCollection.AddSingleton<IEventJsonSerializer, MyEventJsonSerializer>();
//        //eventFlowOptions.ServiceCollection.AddSingleton<ISnapshotSerializer, MySnapshotSerializer>();

//        return eventFlowOptions;
//    }
//}
