namespace MyTelegram.Messenger.Handlers.LatestLayer.Stats;
/// <summary>
/// Obtains a list of messages, indicating to which other public channels was a channel message forwarded.<br/>
/// Will return a list of <a href="https://corefork.telegram.org/constructor/message">messages</a> with <code>peer_id</code> equal to the public channel to which this message was forwarded.
/// Possible errors
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 MESSAGE_ID_INVALID The provided message id is invalid.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/stats.getMessagePublicForwards"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetMessagePublicForwardsHandler : RpcResultObjectHandler<MyTelegram.Schema.Stats.RequestGetMessagePublicForwards, MyTelegram.Schema.Stats.IPublicForwards>
{
    protected override Task<MyTelegram.Schema.Stats.IPublicForwards> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Stats.RequestGetMessagePublicForwards obj)
    {
        throw new NotImplementedException();
    }
}