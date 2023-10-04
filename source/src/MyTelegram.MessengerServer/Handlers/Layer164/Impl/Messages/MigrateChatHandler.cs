using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class MigrateChatHandler : RpcResultObjectHandler<RequestMigrateChat, IUpdates>,
    IMigrateChatHandler
{
    protected override Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestMigrateChat obj)
    {
        throw new NotImplementedException();
    }
}