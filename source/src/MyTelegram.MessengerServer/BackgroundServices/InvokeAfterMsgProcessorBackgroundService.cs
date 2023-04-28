using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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