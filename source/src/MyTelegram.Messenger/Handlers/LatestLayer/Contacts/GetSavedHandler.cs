namespace MyTelegram.Messenger.Handlers.LatestLayer.Contacts;
/// <summary>
/// Get all contacts, requires a <a href="https://corefork.telegram.org/api/takeout">takeout session, see here » for more info</a>.
/// <para><c>See <a href="https://corefork.telegram.org/method/contacts.getSaved"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetSavedHandler : RpcResultObjectHandler<MyTelegram.Schema.Contacts.RequestGetSaved, TVector<MyTelegram.Schema.ISavedContact>>
{
    protected override Task<TVector<MyTelegram.Schema.ISavedContact>> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Contacts.RequestGetSaved obj)
    {
        throw new NotImplementedException();
    }
}