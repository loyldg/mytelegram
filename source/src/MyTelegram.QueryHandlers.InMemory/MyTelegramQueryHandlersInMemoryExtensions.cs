namespace MyTelegram.QueryHandlers.InMemory;

public static class MyTelegramQueryHandlersInMemoryExtensions
{
    public static IEventFlowOptions AddInMemoryQueryHandlers(this IEventFlowOptions options)
    {
        options.AddQueryHandlers(typeof(MyTelegramQueryHandlersInMemoryExtensions).Assembly);
        return options;
    }
}
