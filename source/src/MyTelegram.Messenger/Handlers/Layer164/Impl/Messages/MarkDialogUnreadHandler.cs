// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Manually mark dialog as unread
/// <para>Possible errors</para>
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.markDialogUnread" />
///</summary>
internal sealed class MarkDialogUnreadHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestMarkDialogUnread, IBool>,
    Messages.IMarkDialogUnreadHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestMarkDialogUnread obj)
    {
        throw new NotImplementedException();
    }
}
