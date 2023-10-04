using MyTelegram.Handlers.Phone;
using MyTelegram.Schema.Phone;

namespace MyTelegram.MessengerServer.Handlers.Impl.Phone;

public class EditGroupCallParticipantHandler : RpcResultObjectHandler<RequestEditGroupCallParticipant, IUpdates>,
    IEditGroupCallParticipantHandler
{
    protected override Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestEditGroupCallParticipant obj)
    {
        throw new NotImplementedException();
    }
}