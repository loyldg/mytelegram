namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Sends one or more <a href="https://corefork.telegram.org/api/reactions#paid-reactions">paid Telegram Star reactions »</a>, transferring <a href="https://corefork.telegram.org/api/stars">Telegram Stars »</a> to a channel's balance.
/// Possible errors
/// Code Type Description
/// 400 BALANCE_TOO_LOW The transaction cannot be completed because the current <a href="https://corefork.telegram.org/api/stars">Telegram Stars balance</a> is too low.
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 400 MESSAGE_ID_INVALID The provided message id is invalid.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 RANDOM_ID_EMPTY Random ID empty.
/// 400 RANDOM_ID_EXPIRED The specified <code>random_id</code> was expired (most likely it didn't follow the required <code>uint64_t random_id = (time() &lt; &lt; 32) | ((uint64_t)random_uint32_t())</code> format, or the specified time is too far in the past).
/// 400 REACTIONS_COUNT_INVALID The specified number of reactions is invalid.
/// 400 SEND_AS_PEER_INVALID You can't send messages as the specified peer.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.sendPaidReaction"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class SendPaidReactionHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSendPaidReaction, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestSendPaidReaction obj)
    {
        throw new NotImplementedException();
    }
}