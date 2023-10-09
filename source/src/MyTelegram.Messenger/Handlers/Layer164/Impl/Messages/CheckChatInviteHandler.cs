// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Check the validity of a chat invite link and get basic info about it
/// <para>Possible errors</para>
/// Code Type Description
/// 406 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 500 CHAT_MEMBERS_CHANNEL &nbsp;
/// 400 INVITE_HASH_EMPTY The invite hash is empty.
/// 406 INVITE_HASH_EXPIRED The invite link has expired.
/// 400 INVITE_HASH_INVALID The invite hash is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.checkChatInvite" />
///</summary>
internal sealed class CheckChatInviteHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestCheckChatInvite, MyTelegram.Schema.IChatInvite>,
    Messages.ICheckChatInviteHandler
{
    protected override Task<MyTelegram.Schema.IChatInvite> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestCheckChatInvite obj)
    {
        throw new NotImplementedException();
    }
}
