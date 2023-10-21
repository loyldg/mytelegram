using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;

namespace MyTelegram.Messenger.QueryServer.Services;

public interface ICommandExecutor<out TAggregate, in TIdentity, TExecutionResult>
    where TAggregate : IAggregateRoot<TIdentity>
    where TIdentity : IIdentity
    where TExecutionResult : IExecutionResult
{
    Task ProcessCommandAsync();

    void Enqueue(ICommand<TAggregate, TIdentity, TExecutionResult> command);
}