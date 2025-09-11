using MyTelegram.Schema.Premium;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Premium;

///<summary>
/// Obtain which peers are we currently <a href="https://corefork.telegram.org/api/boost">boosting</a>, and how many <a href="https://corefork.telegram.org/api/boost">boost slots</a> we have left.
/// See <a href="https://corefork.telegram.org/method/premium.getMyBoosts" />
///</summary>
internal sealed class GetMyBoostsHandler : RpcResultObjectHandler<MyTelegram.Schema.Premium.RequestGetMyBoosts, MyTelegram.Schema.Premium.IMyBoosts>
{
    protected override Task<MyTelegram.Schema.Premium.IMyBoosts> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Premium.RequestGetMyBoosts obj)
    {
        return Task.FromResult<MyTelegram.Schema.Premium.IMyBoosts>(new TMyBoosts
        {
            Chats = [],
            MyBoosts = [],
            Users = [],
        });
    }
}
