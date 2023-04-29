// ReSharper disable All

using MyTelegram.Schema.Channels;

namespace MyTelegram.Handlers.Channels;

internal sealed class ReportAntiSpamFalsePositiveHandler :
    RpcResultObjectHandler<RequestReportAntiSpamFalsePositive, IBool>,
    Channels.IReportAntiSpamFalsePositiveHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestReportAntiSpamFalsePositive obj)
    {
        throw new NotImplementedException();
    }
}
