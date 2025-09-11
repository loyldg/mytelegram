namespace MyTelegram.Messenger.Handlers.LatestLayer.Contacts;

///<summary>
/// See <a href="https://corefork.telegram.org/method/contacts.getSponsoredPeers" />
///</summary>
internal sealed class GetSponsoredPeersHandler : RpcResultObjectHandler<MyTelegram.Schema.Contacts.RequestGetSponsoredPeers, MyTelegram.Schema.Contacts.ISponsoredPeers>
{
    protected override Task<MyTelegram.Schema.Contacts.ISponsoredPeers> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Contacts.RequestGetSponsoredPeers obj)
    {
        return Task.FromResult<ISponsoredPeers>(new TSponsoredPeersEmpty());
    }
}