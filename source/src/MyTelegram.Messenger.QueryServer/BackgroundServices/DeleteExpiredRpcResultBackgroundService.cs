using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace MyTelegram.Messenger.QueryServer.BackgroundServices;

public class DeleteExpiredRpcResultBackgroundService(ICommandBus commandBus,
    IOptionsMonitor<MyTelegramMessengerServerOptions> options,
    IQueryProcessor queryProcessor,
    ILogger<DeleteExpiredRpcResultBackgroundService> logger) : BackgroundService
{
    private readonly int _pageSize = 5000;
    private static readonly int IntervalSeconds = 300;
    private readonly PeriodicTimer _timer = new(TimeSpan.FromSeconds(IntervalSeconds));
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (await _timer.WaitForNextTickAsync(stoppingToken))
        {
            await DeleteExpiredRpcResultAsync();
        }
    }

    private async Task DeleteExpiredRpcResultAsync()
    {
        try
        {
            var minDate = DateTime.UtcNow.AddMinutes(-options.CurrentValue.RpcResultExpirationMinutes).ToTimestamp();
            var items = await queryProcessor.ProcessAsync(new GetRpcResultListQuery(minDate, _pageSize));
            foreach (var item in items)
            {
                var command = new DeleteRpcResultCommand(RpcResultId.Create(item.UserId, item.ReqMsgId));
                await commandBus.PublishAsync(command, CancellationToken.None);
            }

            if (items.Count > 0)
            {
                logger.LogInformation("Delete RpcResult successfully, count: {Count}", items.Count);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "DeleteExpiredRpcResultAsync error");
        }
    }
}