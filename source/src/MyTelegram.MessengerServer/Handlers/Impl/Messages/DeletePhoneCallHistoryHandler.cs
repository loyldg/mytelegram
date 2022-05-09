using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class DeletePhoneCallHistoryHandler :
    RpcResultObjectHandler<RequestDeletePhoneCallHistory, IAffectedFoundMessages>,
    IDeletePhoneCallHistoryHandler
{
    protected override Task<IAffectedFoundMessages> HandleCoreAsync(IRequestInput input,
        RequestDeletePhoneCallHistory obj)
    {
        throw new NotImplementedException();
    }
}
