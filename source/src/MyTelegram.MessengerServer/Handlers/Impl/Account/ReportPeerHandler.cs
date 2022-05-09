using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class ReportPeerHandler : RpcResultObjectHandler<RequestReportPeer, IBool>,
    IReportPeerHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestReportPeer obj)
    {
        throw new NotImplementedException();
    }
}
