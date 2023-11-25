// ReSharper disable All

namespace MyTelegram.Handlers.Contacts;

///<summary>
/// Returns the list of contact statuses.
/// See <a href="https://corefork.telegram.org/method/contacts.getStatuses" />
///</summary>
internal sealed class GetStatusesHandler : RpcResultObjectHandler<MyTelegram.Schema.Contacts.RequestGetStatuses, TVector<MyTelegram.Schema.IContactStatus>>,
    Contacts.IGetStatusesHandler
{
    protected override Task<TVector<IContactStatus>> HandleCoreAsync(IRequestInput input,
        RequestGetStatuses obj)
    {
        return Task.FromResult(new TVector<IContactStatus>());
    }
}
