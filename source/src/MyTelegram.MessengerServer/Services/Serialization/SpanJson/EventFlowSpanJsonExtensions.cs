namespace MyTelegram.MessengerServer.Services.Serialization.SpanJson;

public static class EventFlowSpanJsonExtensions
{
    public static IEventFlowOptions UseSpanJson(this IEventFlowOptions options)
    {
        options.ServiceCollection.AddSingleton<IJsonSerializer, SpanJsonSerializer>();
        options.ServiceCollection.AddSingleton<IEventJsonSerializer, MySpanJsonEventJsonSerializer>();
        return options;
    }
}