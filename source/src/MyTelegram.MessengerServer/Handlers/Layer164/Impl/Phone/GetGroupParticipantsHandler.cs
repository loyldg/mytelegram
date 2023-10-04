using MyTelegram.Handlers.Phone;
using MyTelegram.Schema.Phone;

namespace MyTelegram.MessengerServer.Handlers.Impl.Phone;

public class GetGroupParticipantsHandler : RpcResultObjectHandler<RequestGetGroupParticipants, IGroupParticipants>,
    IGetGroupParticipantsHandler
{
    protected override Task<IGroupParticipants> HandleCoreAsync(IRequestInput input,
        RequestGetGroupParticipants obj)
    {
        throw new NotImplementedException();
    }
}