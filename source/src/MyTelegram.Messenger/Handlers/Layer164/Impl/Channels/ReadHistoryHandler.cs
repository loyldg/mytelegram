// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// Mark <a href="https://corefork.telegram.org/api/channel">channel/supergroup</a> history as read
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 406 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// See <a href="https://corefork.telegram.org/method/channels.readHistory" />
///</summary>
internal sealed class ReadHistoryHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestReadHistory, IBool>,
    Channels.IReadHistoryHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestReadHistory obj)
    {
        throw new NotImplementedException();
    }
}
