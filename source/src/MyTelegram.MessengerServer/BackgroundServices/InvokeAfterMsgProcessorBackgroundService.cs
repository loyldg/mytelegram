using Microsoft.Extensions.Hosting;

namespace MyTelegram.MessengerServer.BackgroundServices;

public class InvokeAfterMsgProcessorBackgroundService : BackgroundService
{
    private readonly IInvokeAfterMsgProcessor _invokeAfterMsgProcessor;

    public InvokeAfterMsgProcessorBackgroundService(IInvokeAfterMsgProcessor invokeAfterMsgProcessor)
    {
        _invokeAfterMsgProcessor = invokeAfterMsgProcessor;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return _invokeAfterMsgProcessor.ProcessAsync();
    }
}
