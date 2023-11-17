// ReSharper disable All

namespace MyTelegram.Handlers.Stats;

///<summary>
/// Get <a href="https://corefork.telegram.org/api/stats">message statistics</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 MESSAGE_ID_INVALID The provided message id is invalid.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/stats.getMessageStats" />
///</summary>
internal sealed class GetMessageStatsHandler : RpcResultObjectHandler<MyTelegram.Schema.Stats.RequestGetMessageStats, MyTelegram.Schema.Stats.IMessageStats>,
    Stats.IGetMessageStatsHandler
{
    protected override Task<MyTelegram.Schema.Stats.IMessageStats> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Stats.RequestGetMessageStats obj)
    {
        throw new NotImplementedException();
    }
}
