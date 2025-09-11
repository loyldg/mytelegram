namespace MyTelegram.Messenger.Handlers.LatestLayer.Contacts;

///<summary>
/// Replace the contents of an entire <a href="https://corefork.telegram.org/api/block">blocklist, see here for more info »</a>.
/// See <a href="https://corefork.telegram.org/method/contacts.setBlocked" />
///</summary>
internal sealed class SetBlockedHandler : RpcResultObjectHandler<MyTelegram.Schema.Contacts.RequestSetBlocked, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Contacts.RequestSetBlocked obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}
