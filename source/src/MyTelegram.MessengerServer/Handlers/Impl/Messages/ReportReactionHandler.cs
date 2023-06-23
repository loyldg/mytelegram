// ReSharper disable All

using MyTelegram.Schema.Messages;

namespace MyTelegram.Handlers.Messages;

public class ReportReactionHandler : RpcResultObjectHandler<RequestReportReaction, IBool>,
    Messages.IReportReactionHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestReportReaction obj)
    {
        throw new NotImplementedException();
    }
}