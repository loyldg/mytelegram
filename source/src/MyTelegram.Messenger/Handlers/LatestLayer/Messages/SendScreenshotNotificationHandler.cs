namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Notify the other user in a private chat that a screenshot of the chat was taken
/// Possible errors
/// Code Type Description
/// 400 INPUT_USER_DEACTIVATED The specified user was deleted.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 REPLY_MESSAGE_ID_INVALID The specified reply-to message ID is invalid.
/// 400 STORY_ID_INVALID The specified story ID is invalid.
/// 400 YOU_BLOCKED_USER You blocked this user.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.sendScreenshotNotification"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class SendScreenshotNotificationHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSendScreenshotNotification, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestSendScreenshotNotification obj)
    {
        throw new NotImplementedException();
    }
}