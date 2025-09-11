using MyTelegram.Schema.Premium;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Premium;

///<summary>
/// Obtains info about the boosts that were applied to a certain channel or supergroup (admins only)
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/premium.getBoostsList" />
///</summary>
internal sealed class GetBoostsListHandler : RpcResultObjectHandler<MyTelegram.Schema.Premium.RequestGetBoostsList, MyTelegram.Schema.Premium.IBoostsList>
{
    protected override Task<MyTelegram.Schema.Premium.IBoostsList> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Premium.RequestGetBoostsList obj)
    {
        return Task.FromResult<MyTelegram.Schema.Premium.IBoostsList>(new TBoostsList
        {
            Boosts = [],
            Users = [],
        });
    }
}
