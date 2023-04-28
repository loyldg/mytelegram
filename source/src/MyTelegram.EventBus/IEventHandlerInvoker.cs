namespace MyTelegram.EventBus;

public interface IEventHandlerInvoker
{
    Task InvokeAsync(IEventHandler eventHandler, object eventData, Type eventType);
}