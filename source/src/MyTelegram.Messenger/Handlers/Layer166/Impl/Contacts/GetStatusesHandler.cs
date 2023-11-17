// ReSharper disable All

namespace MyTelegram.Handlers.Contacts;

///<summary>
/// Returns the list of contact statuses.
/// See <a href="https://corefork.telegram.org/method/contacts.getStatuses" />
///</summary>
internal sealed class GetStatusesHandler : RpcResultObjectHandler<MyTelegram.Schema.Contacts.RequestGetStatuses, TVector<MyTelegram.Schema.IContactStatus>>,
    Contacts.IGetStatusesHandler
{
    protected override Task<TVector<MyTelegram.Schema.IContactStatus>> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Contacts.RequestGetStatuses obj)
    {
        throw new NotImplementedException();
    }
}
