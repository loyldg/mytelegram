using EventFlow.Aggregates.ExecutionResults;
using Microsoft.Extensions.Hosting;

namespace MyTelegram.Messenger.CommandServer.BackgroundServices;

public class QueuedCommandExecutorBackgroundService<TAggregate, TAggregateId>(IQueuedCommandExecutor<TAggregate, TAggregateId, IExecutionResult> commandExecutor)
    : BackgroundService
    where TAggregate : IAggregateRoot<TAggregateId> where TAggregateId : IIdentity
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return commandExecutor.ProcessCommandAsync(stoppingToken);
    }
}
