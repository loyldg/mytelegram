using Microsoft.Extensions.Hosting;

namespace MyTelegram.Messenger.CommandServer.BackgroundServices;

public class MyTelegramInvokeAfterMsgProcessorBackgroundService(IInvokeAfterMsgProcessor invokeAfterMsgProcessor)
    : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return invokeAfterMsgProcessor.ProcessAsync(stoppingToken);
    }
}
