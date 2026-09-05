using EventFlow;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using EventFlow.Core;
using System.Threading.Channels;

namespace MyTelegram.Services.Services;

public class QueuedCommandExecutor<TAggregate, TIdentity, TExecutionResult>(ICommandBus commandBus, ILogger<QueuedCommandExecutor<TAggregate, TIdentity, TExecutionResult>> logger)
    : IQueuedCommandExecutor<TAggregate, TIdentity, TExecutionResult>
    where TAggregate : IAggregateRoot<TIdentity>
    where TIdentity : IIdentity
    where TExecutionResult : IExecutionResult
{
    private readonly Channel<ICommand<TAggregate, TIdentity, TExecutionResult>> _commands = Channel.CreateUnbounded<ICommand<TAggregate, TIdentity, TExecutionResult>>();

    public async Task ProcessCommandAsync(
        CancellationToken cancellationToken = default)
    {
        try
        {
            while (await _commands.Reader
                       .WaitToReadAsync(cancellationToken)
                       .ConfigureAwait(false))
            {
                while (_commands.Reader.TryRead(out var command))
                {
                    try
                    {
                        await commandBus.PublishAsync(
                            command,
                            cancellationToken);
                    }
                    catch (OperationCanceledException)
                        when (cancellationToken.IsCancellationRequested)
                    {
                        // Normal shutdown.
                        return;
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(
                            ex,
                            "Publish command failed");
                    }
                }
            }
        }
        catch (OperationCanceledException)
            when (cancellationToken.IsCancellationRequested)
        {
            // Normal shutdown.
        }
    }

    public void Enqueue(
        ICommand<TAggregate, TIdentity, TExecutionResult> command)
    {
        _commands.Writer.TryWrite(command);
    }
}