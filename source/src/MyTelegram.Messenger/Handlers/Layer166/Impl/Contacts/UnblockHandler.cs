// ReSharper disable All

namespace MyTelegram.Handlers.Contacts;

///<summary>
/// Deletes the user from the blacklist.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CONTACT_ID_INVALID The provided contact ID is invalid.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/contacts.unblock" />
///</summary>
internal sealed class UnblockHandler : RpcResultObjectHandler<MyTelegram.Schema.Contacts.RequestUnblock, IBool>,
    Contacts.IUnblockHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Contacts.RequestUnblock obj)
    {
        throw new NotImplementedException();
    }
}
