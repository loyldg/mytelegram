// ReSharper disable All

using MyTelegram.Schema.Premium;

namespace MyTelegram.Handlers.Premium;

///<summary>
/// See <a href="https://corefork.telegram.org/method/premium.getBoostsList" />
///</summary>
internal sealed class GetBoostsListHandler : RpcResultObjectHandler<MyTelegram.Schema.Premium.RequestGetBoostsList, MyTelegram.Schema.Premium.IBoostsList>,
    Premium.IGetBoostsListHandler
{
    protected override Task<MyTelegram.Schema.Premium.IBoostsList> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Premium.RequestGetBoostsList obj)
    {
        return Task.FromResult<MyTelegram.Schema.Premium.IBoostsList>(new TBoostsList
        {
            Boosts = new(),
            Users = new(),
        });
    }
}
