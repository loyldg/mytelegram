using MyTelegram.Handlers.Help;
using MyTelegram.Schema.Help;

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
