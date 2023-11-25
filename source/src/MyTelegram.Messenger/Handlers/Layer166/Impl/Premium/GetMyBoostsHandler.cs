// ReSharper disable All

using MyTelegram.Schema.Premium;

namespace MyTelegram.Handlers.Premium;

///<summary>
/// See <a href="https://corefork.telegram.org/method/premium.getMyBoosts" />
///</summary>
internal sealed class GetMyBoostsHandler : RpcResultObjectHandler<MyTelegram.Schema.Premium.RequestGetMyBoosts, MyTelegram.Schema.Premium.IMyBoosts>,
    Premium.IGetMyBoostsHandler, IProcessedHandler
{
    protected override Task<MyTelegram.Schema.Premium.IMyBoosts> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Premium.RequestGetMyBoosts obj)
    {
        return Task.FromResult<MyTelegram.Schema.Premium.IMyBoosts>(new TMyBoosts
        {
            Chats = new(),
            MyBoosts = new(),
            Users = new(),
        });
    }
}
