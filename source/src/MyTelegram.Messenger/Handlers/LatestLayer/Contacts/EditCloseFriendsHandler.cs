namespace MyTelegram.Messenger.Handlers.LatestLayer.Contacts;

///<summary>
/// Edit the <a href="https://corefork.telegram.org/api/privacy">close friends list, see here »</a> for more info.
/// See <a href="https://corefork.telegram.org/method/contacts.editCloseFriends" />
///</summary>
internal sealed class EditCloseFriendsHandler : RpcResultObjectHandler<MyTelegram.Schema.Contacts.RequestEditCloseFriends, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Contacts.RequestEditCloseFriends obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}
