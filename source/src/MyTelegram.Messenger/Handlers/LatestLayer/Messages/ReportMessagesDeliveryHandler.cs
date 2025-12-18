namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Used for <a href="https://telegram.org/blog/star-messages-gateway-2-0-and-more#save-even-more-on-user-verification">Telegram Gateway verification messages »</a>: indicate to the server that one or more <a href="https://corefork.telegram.org/constructor/message">message</a>s were received by the client, if requested by the <a href="https://corefork.telegram.org/constructor/message">message</a>.<strong>report_delivery_until_date</strong> flag or the equivalent flag in <a href="https://corefork.telegram.org/api/push-updates">push notifications</a>.
/// Possible errors
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.reportMessagesDelivery"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class ReportMessagesDeliveryHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestReportMessagesDelivery, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestReportMessagesDelivery obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}