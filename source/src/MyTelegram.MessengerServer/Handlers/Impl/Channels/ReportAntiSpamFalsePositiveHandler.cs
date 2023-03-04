// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

internal sealed class ReportAntiSpamFalsePositiveHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestReportAntiSpamFalsePositive, IBool>,
    Channels.IReportAntiSpamFalsePositiveHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestReportAntiSpamFalsePositive obj)
    {
        throw new NotImplementedException();
    }
}
