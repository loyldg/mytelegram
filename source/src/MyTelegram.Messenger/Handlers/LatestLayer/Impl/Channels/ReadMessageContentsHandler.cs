// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// Mark <a href="https://corefork.telegram.org/api/channel">channel/supergroup</a> message contents as read
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 406 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// See <a href="https://corefork.telegram.org/method/channels.readMessageContents" />
///</summary>
internal sealed class ReadMessageContentsHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestReadMessageContents, IBool>,
    Channels.IReadMessageContentsHandler
{
    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestReadMessageContents obj)
    {
        return new TBoolTrue();
    }
}
