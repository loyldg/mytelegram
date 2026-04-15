
namespace MyTelegram.Messenger.Handlers.Messages;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class ReportReadMetricsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestReportReadMetrics, IBool>, IObjectHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestReportReadMetrics obj)
    {
        throw new NotImplementedException();
    }
}

