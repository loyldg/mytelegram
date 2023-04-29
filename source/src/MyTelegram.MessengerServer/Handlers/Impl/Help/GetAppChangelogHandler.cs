using MyTelegram.Handlers.Help;

namespace MyTelegram.MessengerServer.Handlers.Impl.Help;

public class GetAppChangelogHandler : RpcResultObjectHandler<RequestGetAppChangelog, IUpdates>,
    IGetAppChangelogHandler
{
    protected override Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestGetAppChangelog obj)
    {
        throw new NotImplementedException();
    }
}
