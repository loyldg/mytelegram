using MyTelegram.Handlers.Channels;
using MyTelegram.Schema.Channels;

namespace MyTelegram.MessengerServer.Handlers.Impl.Channels;

public class ReportSpamHandler : RpcResultObjectHandler<RequestReportSpam, IBool>,
    IReportSpamHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestReportSpam obj)
    {
        throw new NotImplementedException();
    }
}
