using MyTelegram.Handlers.Channels;
using MyTelegram.Schema.Channels;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Channels;

public class DeleteUserHistoryHandler : RpcResultObjectHandler<RequestDeleteUserHistory, IAffectedHistory>,
    IDeleteUserHistoryHandler
{
    protected override Task<IAffectedHistory> HandleCoreAsync(IRequestInput input,
        RequestDeleteUserHistory obj)
    {
        throw new NotImplementedException();
    }
}
