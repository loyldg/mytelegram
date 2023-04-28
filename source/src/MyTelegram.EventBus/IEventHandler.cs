namespace MyTelegram.EventBus;

public interface IEventHandler<in TEvent> : IEventHandler
{
    Task HandleEventAsync(TEvent eventData);
}

public interface IEventHandler
{
}
