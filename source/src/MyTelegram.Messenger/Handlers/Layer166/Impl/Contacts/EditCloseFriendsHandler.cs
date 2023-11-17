// ReSharper disable All

namespace MyTelegram.Handlers.Contacts;

///<summary>
/// See <a href="https://corefork.telegram.org/method/contacts.editCloseFriends" />
///</summary>
internal sealed class EditCloseFriendsHandler : RpcResultObjectHandler<MyTelegram.Schema.Contacts.RequestEditCloseFriends, IBool>,
    Contacts.IEditCloseFriendsHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Contacts.RequestEditCloseFriends obj)
    {
        throw new NotImplementedException();
    }
}
