using EventFlow.Aggregates;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using EventFlow.Core;

namespace MyTelegram.Services.Services;

public interface IQueuedCommandExecutor<out TAggregate, in TIdentity, TExecutionResult>
    where TAggregate : IAggregateRoot<TIdentity>
    where TIdentity : IIdentity
    where TExecutionResult : IExecutionResult
{
    Task ProcessCommandAsync(CancellationToken cancellationToken = default);

    void Enqueue(ICommand<TAggregate, TIdentity, TExecutionResult> command);
}