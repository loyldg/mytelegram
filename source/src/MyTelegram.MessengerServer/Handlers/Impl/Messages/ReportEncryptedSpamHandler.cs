using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class ReportEncryptedSpamHandler : RpcResultObjectHandler<RequestReportEncryptedSpam, IBool>,
    IReportEncryptedSpamHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestReportEncryptedSpam obj)
    {
        throw new NotImplementedException();
    }
}
