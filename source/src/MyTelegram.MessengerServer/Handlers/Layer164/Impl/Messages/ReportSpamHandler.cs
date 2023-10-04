using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;
using RequestReportSpam = MyTelegram.Schema.Messages.RequestReportSpam;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class ReportSpamHandler : RpcResultObjectHandler<RequestReportSpam, IBool>,
    IReportSpamHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestReportSpam obj)
    {
        throw new NotImplementedException();
    }
}