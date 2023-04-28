namespace MyTelegram.EventBus;

public delegate Task EventHandlerMethodExecutorAsync(IEventHandler target, object parameter);