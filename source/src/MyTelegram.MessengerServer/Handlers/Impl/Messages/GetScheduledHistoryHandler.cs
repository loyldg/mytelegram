using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetScheduledHistoryHandler : RpcResultObjectHandler<RequestGetScheduledHistory, IMessages>,
    IGetScheduledHistoryHandler, IProcessedHandler
{
    protected override Task<IMessages> HandleCoreAsync(IRequestInput input,
        RequestGetScheduledHistory obj)
    {
        return Task.FromResult<IMessages>(new TMessages
        {
            Chats = new TVector<IChat>(), Messages = new TVector<IMessage>(), Users = new TVector<IUser>()
        });
    }
}