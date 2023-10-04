using MyTelegram.Handlers.Contacts;
using MyTelegram.Schema.Contacts;

namespace MyTelegram.MessengerServer.Handlers.Impl.Contacts;

public class ImportContactsHandler : RpcResultObjectHandler<RequestImportContacts, IImportedContacts>,
    IImportContactsHandler, IProcessedHandler //, IShouldCacheRequest
{
    protected override Task<IImportedContacts> HandleCoreAsync(IRequestInput input,
        RequestImportContacts obj)
    {
        throw new NotImplementedException();
    }
}