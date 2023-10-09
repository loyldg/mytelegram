// ReSharper disable All

namespace MyTelegram.Handlers.Stats;

///<summary>
/// Get <a href="https://corefork.telegram.org/api/stats">supergroup statistics</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 403 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 MEGAGROUP_REQUIRED You can only use this method on a supergroup.
/// See <a href="https://corefork.telegram.org/method/stats.getMegagroupStats" />
///</summary>
internal sealed class GetMegagroupStatsHandler : RpcResultObjectHandler<MyTelegram.Schema.Stats.RequestGetMegagroupStats, MyTelegram.Schema.Stats.IMegagroupStats>,
    Stats.IGetMegagroupStatsHandler
{
    protected override Task<MyTelegram.Schema.Stats.IMegagroupStats> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Stats.RequestGetMegagroupStats obj)
    {
        throw new NotImplementedException();
    }
}
