using EventFlow.Aggregates.ExecutionResults;
using Microsoft.Extensions.Hosting;
using MyTelegram.Messenger.QueryServer.Services;

namespace MyTelegram.Messenger.QueryServer.BackgroundServices;

public class CommandExecutorBackgroundService : BackgroundService
{
    private readonly ICommandExecutor<PtsAggregate, PtsId, IExecutionResult> _ptsCommandExecutor;

    public CommandExecutorBackgroundService(ICommandExecutor<PtsAggregate, PtsId, IExecutionResult> ptsCommandExecutor)
    {
        _ptsCommandExecutor = ptsCommandExecutor;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return _ptsCommandExecutor.ProcessCommandAsync();
    }
}