namespace MyTelegram.EventBus;

// taken from abp
public class DistributedEventHandlerMethodExecutor<TEvent> : IEventHandlerMethodExecutor
    where TEvent : class
{
    public EventHandlerMethodExecutorAsync ExecutorAsync => (target, parameter) => ((IEventHandler<TEvent>)target).HandleEventAsync((TEvent)parameter);

    public Task ExecuteAsync(IEventHandler target, TEvent parameters)
    {
        return ExecutorAsync(target, parameters);
    }
}