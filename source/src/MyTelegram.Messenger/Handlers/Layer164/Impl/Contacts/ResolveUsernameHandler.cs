// ReSharper disable All

namespace MyTelegram.Handlers.Contacts;

///<summary>
/// Resolve a @username to get peer info
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CONNECTION_LAYER_INVALID Layer invalid.
/// 400 USERNAME_INVALID The provided username is not valid.
/// 400 USERNAME_NOT_OCCUPIED The provided username is not occupied.
/// See <a href="https://corefork.telegram.org/method/contacts.resolveUsername" />
///</summary>
internal sealed class ResolveUsernameHandler : RpcResultObjectHandler<MyTelegram.Schema.Contacts.RequestResolveUsername, MyTelegram.Schema.Contacts.IResolvedPeer>,
    Contacts.IResolveUsernameHandler
{
    protected override Task<MyTelegram.Schema.Contacts.IResolvedPeer> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Contacts.RequestResolveUsername obj)
    {
        throw new NotImplementedException();
    }
}
