namespace MyTelegram.Messenger.Handlers.LatestLayer.Contacts;
/// <summary>
/// Get most used peers
/// Possible errors
/// Code Type Description
/// 400 TYPES_EMPTY No top peer type was provided.
/// <para><c>See <a href="https://corefork.telegram.org/method/contacts.getTopPeers"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetTopPeersHandler : RpcResultObjectHandler<MyTelegram.Schema.Contacts.RequestGetTopPeers, MyTelegram.Schema.Contacts.ITopPeers>
{
    protected override Task<ITopPeers> HandleCoreAsync(IRequestInput input, RequestGetTopPeers obj)
    {
        ITopPeers r = new TTopPeers
        {
            Categories = [],
            Chats = [],
            Users = []
        };
        return Task.FromResult(r);
    }
}