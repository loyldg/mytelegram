using Microsoft.Extensions.Hosting;

namespace MyTelegram.Messenger.CommandServer.BackgroundServices;

public class MyTelegramInvokeAfterMsgProcessorBackgroundService : BackgroundService
{
    private readonly IInvokeAfterMsgProcessor _invokeAfterMsgProcessor;

    public MyTelegramInvokeAfterMsgProcessorBackgroundService(IInvokeAfterMsgProcessor invokeAfterMsgProcessor)
    {
        _invokeAfterMsgProcessor = invokeAfterMsgProcessor;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return _invokeAfterMsgProcessor.ProcessAsync();
    }
}
