// ReSharper disable All

namespace MyTelegram.Handlers.Contacts;

///<summary>
/// Resolve a phone number to get user info, if their privacy settings allow it.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 PHONE_NOT_OCCUPIED No user is associated to the specified phone number.
/// See <a href="https://corefork.telegram.org/method/contacts.resolvePhone" />
///</summary>
internal sealed class ResolvePhoneHandler : RpcResultObjectHandler<MyTelegram.Schema.Contacts.RequestResolvePhone, MyTelegram.Schema.Contacts.IResolvedPeer>,
    Contacts.IResolvePhoneHandler
{
    protected override Task<MyTelegram.Schema.Contacts.IResolvedPeer> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Contacts.RequestResolvePhone obj)
    {
        throw new NotImplementedException();
    }
}
