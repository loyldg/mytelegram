namespace MyTelegram.Messenger.Handlers.LatestLayer.Contacts;

///<summary>
/// Get the telegram IDs of all contacts.<br>
/// Returns an array of Telegram user IDs for all contacts (0 if a contact does not have an associated Telegram account or have hidden their account using privacy settings).
/// See <a href="https://corefork.telegram.org/method/contacts.getContactIDs" />
///</summary>
internal sealed class GetContactIDsHandler(IQueryProcessor queryProcessor)
    : RpcResultObjectHandler<MyTelegram.Schema.Contacts.RequestGetContactIDs, TVector<int>>
{
    protected override async Task<TVector<int>> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Contacts.RequestGetContactIDs obj)
    {
        var contactIds = await queryProcessor.ProcessAsync(new GetContactUserIdListQuery(input.UserId));

        return [.. contactIds.Select(p => (int)p)];
    }
}
