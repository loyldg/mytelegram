using EventFlow.Aggregates.ExecutionResults;
using Microsoft.Extensions.Hosting;

namespace MyTelegram.Messenger.QueryServer.BackgroundServices;

public class QueuedCommandExecutorBackgroundService(
    IQueuedCommandExecutor<PtsAggregate, PtsId, IExecutionResult> ptsCommandExecutor)
    : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return ptsCommandExecutor.ProcessCommandAsync(stoppingToken);
    }
}