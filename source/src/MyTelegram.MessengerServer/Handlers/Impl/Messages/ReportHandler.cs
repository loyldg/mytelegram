using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class ReportHandler : RpcResultObjectHandler<RequestReport, IBool>,
    IReportHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestReport obj)
    {
        throw new NotImplementedException();
    }
}