namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Confirms receipt of messages by a client, cancels PUSH-notification sending.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.receivedMessages"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class ReceivedMessagesHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestReceivedMessages, TVector<MyTelegram.Schema.IReceivedNotifyMessage>>
{
    protected override Task<TVector<IReceivedNotifyMessage>> HandleCoreAsync(IRequestInput input, RequestReceivedMessages obj)
    {
        return Task.FromResult(new TVector<IReceivedNotifyMessage> { new TReceivedNotifyMessage { Id = obj.MaxId } });
    }
}