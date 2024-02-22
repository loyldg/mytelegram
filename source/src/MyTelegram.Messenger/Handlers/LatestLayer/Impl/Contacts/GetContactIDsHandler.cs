// ReSharper disable All

namespace MyTelegram.Handlers.Contacts;

///<summary>
/// Get the telegram IDs of all contacts.<br>
/// Returns an array of Telegram user IDs for all contacts (0 if a contact does not have an associated Telegram account or have hidden their account using privacy settings).
/// See <a href="https://corefork.telegram.org/method/contacts.getContactIDs" />
///</summary>
internal sealed class GetContactIDsHandler : RpcResultObjectHandler<MyTelegram.Schema.Contacts.RequestGetContactIDs, TVector<int>>,
    Contacts.IGetContactIDsHandler
{
    private readonly IQueryProcessor _queryProcessor;

    public GetContactIDsHandler(IQueryProcessor queryProcessor)
    {
        _queryProcessor = queryProcessor;
    }

    protected override async Task<TVector<int>> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Contacts.RequestGetContactIDs obj)
    {
        var contactIds = await _queryProcessor.ProcessAsync(new GetContactUserIdListQuery(input.UserId));

        return new TVector<int>(contactIds.Select(p => (int)p));
    }
}
