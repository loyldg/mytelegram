// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

public class ReportReactionHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestReportReaction, IBool>,
    Messages.IReportReactionHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestReportReaction obj)
    {
        throw new NotImplementedException();
    }
}
