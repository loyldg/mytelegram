namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Confirms receipt of messages in a secret chat by client, cancels push notifications.<br/>
/// The method returns a list of <strong>random_id</strong>s of messages for which push notifications were cancelled.
/// Possible errors
/// Code Type Description
/// 400 MAX_QTS_INVALID The specified max_qts is invalid.
/// 500 MSG_WAIT_FAILED A waiting call returned an error.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.receivedQueue"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class ReceivedQueueHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestReceivedQueue, TVector<long>>
{
    protected override Task<TVector<long>> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestReceivedQueue obj)
    {
        return Task.FromResult(new TVector<long>());
    }
}