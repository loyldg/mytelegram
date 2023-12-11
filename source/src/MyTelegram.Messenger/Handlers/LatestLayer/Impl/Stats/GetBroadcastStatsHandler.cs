// ReSharper disable All

namespace MyTelegram.Handlers.Stats;

///<summary>
/// Get <a href="https://corefork.telegram.org/api/stats">channel statistics</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 400 BROADCAST_REQUIRED This method can only be called on a channel, please use stats.getMegagroupStats for supergroups.
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 403 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// See <a href="https://corefork.telegram.org/method/stats.getBroadcastStats" />
///</summary>
internal sealed class GetBroadcastStatsHandler : RpcResultObjectHandler<MyTelegram.Schema.Stats.RequestGetBroadcastStats, MyTelegram.Schema.Stats.IBroadcastStats>,
    Stats.IGetBroadcastStatsHandler
{
    protected override Task<MyTelegram.Schema.Stats.IBroadcastStats> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Stats.RequestGetBroadcastStats obj)
    {
        throw new NotImplementedException();
    }
}
