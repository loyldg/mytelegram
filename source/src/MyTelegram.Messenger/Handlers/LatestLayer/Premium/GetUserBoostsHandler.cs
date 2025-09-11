using MyTelegram.Schema.Premium;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Premium;

///<summary>
/// Returns the lists of boost that were applied to a channel/supergroup by a specific user (admins only)
/// <para>Possible errors</para>
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/premium.getUserBoosts" />
///</summary>
internal sealed class GetUserBoostsHandler : RpcResultObjectHandler<MyTelegram.Schema.Premium.RequestGetUserBoosts, MyTelegram.Schema.Premium.IBoostsList>
{
    protected override Task<MyTelegram.Schema.Premium.IBoostsList> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Premium.RequestGetUserBoosts obj)
    {
        return Task.FromResult<MyTelegram.Schema.Premium.IBoostsList>(new TBoostsList
        {
            Boosts = [],
            Users = [],
        });
    }
}
