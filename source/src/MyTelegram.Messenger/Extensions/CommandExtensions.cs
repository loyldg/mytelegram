namespace MyTelegram.Messenger.Extensions;

public static class CommandBusExtensions
{
    public static Task<TResult> PublishAsync<TAggregate, TIdentity, TResult>(this ICommandBus commandBus, ICommand<TAggregate, TIdentity, TResult> command) where TAggregate : IAggregateRoot<TIdentity> where TIdentity : IIdentity where TResult : IExecutionResult
    {
        return commandBus.PublishAsync(command, default);
    }
}