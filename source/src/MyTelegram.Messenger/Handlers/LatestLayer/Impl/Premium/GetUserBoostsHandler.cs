// ReSharper disable All

using MyTelegram.Schema.Premium;

namespace MyTelegram.Handlers.Premium;

///<summary>
/// See <a href="https://corefork.telegram.org/method/premium.getUserBoosts" />
///</summary>
internal sealed class GetUserBoostsHandler : RpcResultObjectHandler<MyTelegram.Schema.Premium.RequestGetUserBoosts, MyTelegram.Schema.Premium.IBoostsList>,
    Premium.IGetUserBoostsHandler
{
    protected override Task<MyTelegram.Schema.Premium.IBoostsList> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Premium.RequestGetUserBoosts obj)
    {
        return Task.FromResult<MyTelegram.Schema.Premium.IBoostsList>(new TBoostsList
        {
            Boosts = new(),
            Users = new(),
        });
    }
}
