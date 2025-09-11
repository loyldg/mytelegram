namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;

///<summary>
/// See <a href="https://corefork.telegram.org/method/payments.toggleChatStarGiftNotifications" />
///</summary>
internal sealed class ToggleChatStarGiftNotificationsHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestToggleChatStarGiftNotifications, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Payments.RequestToggleChatStarGiftNotifications obj)
    {
        throw new NotImplementedException();
    }
}
