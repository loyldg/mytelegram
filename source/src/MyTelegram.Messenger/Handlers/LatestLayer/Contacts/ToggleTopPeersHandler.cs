namespace MyTelegram.Messenger.Handlers.LatestLayer.Contacts;

///<summary>
/// Enable/disable <a href="https://corefork.telegram.org/api/top-rating">top peers</a>
/// See <a href="https://corefork.telegram.org/method/contacts.toggleTopPeers" />
///</summary>
internal sealed class ToggleTopPeersHandler : RpcResultObjectHandler<MyTelegram.Schema.Contacts.RequestToggleTopPeers, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Contacts.RequestToggleTopPeers obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}
