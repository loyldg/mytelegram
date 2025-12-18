namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;
/// <summary>
/// Enables or disables the reception of notifications every time a <a href="https://corefork.telegram.org/api/gifts">gift »</a> is received by the specified channel, can only be invoked by admins with <code>post_messages</code> <a href="https://corefork.telegram.org/constructor/chatAdminRights">admin rights</a>.
/// Possible errors
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/payments.toggleChatStarGiftNotifications"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class ToggleChatStarGiftNotificationsHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestToggleChatStarGiftNotifications, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Payments.RequestToggleChatStarGiftNotifications obj)
    {
        throw new NotImplementedException();
    }
}