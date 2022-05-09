using MyTelegram.Handlers.Contacts;
using MyTelegram.Schema.Contacts;

namespace MyTelegram.MessengerServer.Handlers.Impl.Contacts;

public class DeleteContactsHandler : RpcResultObjectHandler<RequestDeleteContacts, IUpdates>,
    IDeleteContactsHandler
{
    protected override Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestDeleteContacts obj)
    {
        throw new NotImplementedException();
    }
}
