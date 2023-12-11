// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Delete all revoked chat invites
/// <para>Possible errors</para>
/// Code Type Description
/// 400 ADMIN_ID_INVALID The specified admin ID is invalid.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.deleteRevokedExportedChatInvites" />
///</summary>
internal sealed class DeleteRevokedExportedChatInvitesHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestDeleteRevokedExportedChatInvites, IBool>,
    Messages.IDeleteRevokedExportedChatInvitesHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestDeleteRevokedExportedChatInvites obj)
    {
        throw new NotImplementedException();
    }
}
