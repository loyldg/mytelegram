using Microsoft.Extensions.Hosting;

namespace MyTelegram.Messenger.CommandServer.BackgroundServices;

public class ChannelViewsBackgroundService(IChannelMessageViewsAppService channelMessageViewsAppService,
    //IScheduleAppService scheduleAppService,
    ILogger<ChannelViewsBackgroundService> logger) : BackgroundService
{
    protected override async  Task ExecuteAsync(CancellationToken stoppingToken)
    {
        channelMessageViewsAppService.LoadViewsFilters();

        while (!stoppingToken.IsCancellationRequested)
        {
            var now = DateTime.UtcNow;
            var nextRunTime = now.Date.AddDays(1);
            //var nextRunTime = now.AddMinutes(1);

            var delay = nextRunTime - now;
            await Task.Delay(delay, stoppingToken);
            channelMessageViewsAppService.RotateDaily();
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Saving channel message views filter data.");
        channelMessageViewsAppService.SaveViewsFilters();
        logger.LogInformation("Channel message views filter data saved successfully.");

        await base.StopAsync(cancellationToken);
    }
}