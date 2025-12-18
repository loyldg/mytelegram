namespace MyTelegram.Messenger.Handlers.LatestLayer.Channels;
/// <summary>
/// Mark <a href="https://corefork.telegram.org/api/channel">channel/supergroup</a> message contents as read, emitting an <a href="https://corefork.telegram.org/constructor/updateChannelReadMessagesContents">updateChannelReadMessagesContents</a>.
/// Possible errors
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 406 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// <para><c>See <a href="https://corefork.telegram.org/method/channels.readMessageContents"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class ReadMessageContentsHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestReadMessageContents, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Channels.RequestReadMessageContents obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}