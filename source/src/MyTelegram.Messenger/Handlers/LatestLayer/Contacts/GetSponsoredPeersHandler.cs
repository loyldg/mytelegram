namespace MyTelegram.Messenger.Handlers.LatestLayer.Contacts;
/// <summary>
/// Obtain a list of sponsored peer search results for a given query
/// Possible errors
/// Code Type Description
/// 400 SEARCH_QUERY_EMPTY The search query is empty.
/// <para><c>See <a href="https://corefork.telegram.org/method/contacts.getSponsoredPeers"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetSponsoredPeersHandler : RpcResultObjectHandler<MyTelegram.Schema.Contacts.RequestGetSponsoredPeers, MyTelegram.Schema.Contacts.ISponsoredPeers>
{
    protected override Task<MyTelegram.Schema.Contacts.ISponsoredPeers> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Contacts.RequestGetSponsoredPeers obj)
    {
        return Task.FromResult<ISponsoredPeers>(new TSponsoredPeersEmpty());
    }
}