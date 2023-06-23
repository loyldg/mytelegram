using MyTelegram.Handlers.Contacts;
using MyTelegram.Schema.Contacts;

namespace MyTelegram.MessengerServer.Handlers.Impl.Contacts;

public class AcceptContactHandler : RpcResultObjectHandler<RequestAcceptContact, IUpdates>,
    IAcceptContactHandler
{
    protected override Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestAcceptContact obj)
    {
        throw new NotImplementedException();
    }
}