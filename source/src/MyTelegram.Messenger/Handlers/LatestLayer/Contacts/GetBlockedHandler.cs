namespace MyTelegram.Messenger.Handlers.LatestLayer.Contacts;

///<summary>
/// Returns the list of blocked users.
/// See <a href="https://corefork.telegram.org/method/contacts.getBlocked" />
///</summary>
internal sealed class GetBlockedHandler : RpcResultObjectHandler<MyTelegram.Schema.Contacts.RequestGetBlocked, MyTelegram.Schema.Contacts.IBlocked>
{
    protected override Task<MyTelegram.Schema.Contacts.IBlocked> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Contacts.RequestGetBlocked obj)
    {
        return Task.FromResult<IBlocked>(new TBlocked
        {
            Blocked = new(),
            Chats = new(),
            Users = new()
        });
    }
}
