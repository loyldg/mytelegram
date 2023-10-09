// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Notify the other user in a private chat that a screenshot of the chat was taken
/// <para>Possible errors</para>
/// Code Type Description
/// 400 INPUT_USER_DEACTIVATED The specified user was deleted.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 YOU_BLOCKED_USER You blocked this user.
/// See <a href="https://corefork.telegram.org/method/messages.sendScreenshotNotification" />
///</summary>
internal sealed class SendScreenshotNotificationHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSendScreenshotNotification, MyTelegram.Schema.IUpdates>,
    Messages.ISendScreenshotNotificationHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestSendScreenshotNotification obj)
    {
        throw new NotImplementedException();
    }
}
