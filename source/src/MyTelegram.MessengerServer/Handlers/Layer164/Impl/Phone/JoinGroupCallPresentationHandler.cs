using MyTelegram.Handlers.Phone;
using MyTelegram.Schema.Phone;

namespace MyTelegram.MessengerServer.Handlers.Impl.Phone;

public class JoinGroupCallPresentationHandler : RpcResultObjectHandler<RequestJoinGroupCallPresentation, IUpdates>,
    IJoinGroupCallPresentationHandler
{
    protected override Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestJoinGroupCallPresentation obj)
    {
        throw new NotImplementedException();
    }
}