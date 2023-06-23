using MyTelegram.Handlers.Contacts;
using MyTelegram.Schema.Contacts;

namespace MyTelegram.MessengerServer.Handlers.Impl.Contacts;

public class AddContactHandler : RpcResultObjectHandler<RequestAddContact, IUpdates>,
    IAddContactHandler
{
    protected override Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestAddContact obj)
    {
        throw new NotImplementedException();
    }
}