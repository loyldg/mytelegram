// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Delete a chat invite
/// <para>Possible errors</para>
/// Code Type Description
/// 400 INVITE_HASH_EXPIRED The invite link has expired.
/// 400 INVITE_REVOKED_MISSING The specified invite link was already revoked or is invalid.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.deleteExportedChatInvite" />
///</summary>
internal sealed class DeleteExportedChatInviteHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestDeleteExportedChatInvite, IBool>,
    Messages.IDeleteExportedChatInviteHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestDeleteExportedChatInvite obj)
    {
        throw new NotImplementedException();
    }
}
