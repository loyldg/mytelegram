// ReSharper disable All

namespace MyTelegram.Handlers.Contacts;

///<summary>
/// Get contact by telegram IDs
/// See <a href="https://corefork.telegram.org/method/contacts.getContactIDs" />
///</summary>
internal sealed class GetContactIDsHandler : RpcResultObjectHandler<MyTelegram.Schema.Contacts.RequestGetContactIDs, TVector<int>>,
    Contacts.IGetContactIDsHandler
{
    protected override Task<TVector<int>> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Contacts.RequestGetContactIDs obj)
    {
        throw new NotImplementedException();
    }
}
