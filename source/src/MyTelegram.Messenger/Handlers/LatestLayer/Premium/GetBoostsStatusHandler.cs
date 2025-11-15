using MyTelegram.Schema.Premium;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Premium;
/// <summary>
/// Gets the current <a href="https://corefork.telegram.org/api/boost">number of boosts</a> of a channel/supergroup.
/// Possible errors
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/premium.getBoostsStatus"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetBoostsStatusHandler : RpcResultObjectHandler<MyTelegram.Schema.Premium.RequestGetBoostsStatus, MyTelegram.Schema.Premium.IBoostsStatus>
{
    protected override Task<MyTelegram.Schema.Premium.IBoostsStatus> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Premium.RequestGetBoostsStatus obj)
    {
        var boostsStatus = new TBoostsStatus
        {
            MyBoost = false,
            Level = 50,
            Boosts = 1000,
            CurrentLevelBoosts = 100,
            NextLevelBoosts = 100,
            BoostUrl = "https://t.me/"
        };
        return Task.FromResult<IBoostsStatus>(boostsStatus);
    }
}