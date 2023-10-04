using MyTelegram.Handlers.Contacts;
using MyTelegram.Schema.Contacts;

namespace MyTelegram.MessengerServer.Handlers.Impl.Contacts;

public class GetContactsHandler : RpcResultObjectHandler<RequestGetContacts, IContacts>,
    IGetContactsHandler, IProcessedHandler
{
    protected override Task<IContacts> HandleCoreAsync(IRequestInput input,
        RequestGetContacts obj)
    {
        return Task.FromResult<IContacts>(new TContacts
        {
            Contacts = new TVector<IContact>(),
            Users = new TVector<IUser>()
        });
        //return Task.FromResult<IContacts>(r);
    }
}