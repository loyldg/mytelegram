// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Confirms receipt of messages in a secret chat by client, cancels push notifications.<br>
/// The method returns a list of <strong>random_id</strong>s of messages for which push notifications were cancelled.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 MAX_QTS_INVALID The specified max_qts is invalid.
/// 500 MSG_WAIT_FAILED A waiting call returned an error.
/// See <a href="https://corefork.telegram.org/method/messages.receivedQueue" />
///</summary>
internal sealed class ReceivedQueueHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestReceivedQueue, TVector<long>>,
    Messages.IReceivedQueueHandler
{
    protected override Task<TVector<long>> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestReceivedQueue obj)
    {
        throw new NotImplementedException();
    }
}
