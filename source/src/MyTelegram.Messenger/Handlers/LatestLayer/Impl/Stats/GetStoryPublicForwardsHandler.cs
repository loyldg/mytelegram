// ReSharper disable All

namespace MyTelegram.Handlers.Stats;

///<summary>
/// See <a href="https://corefork.telegram.org/method/stats.getStoryPublicForwards" />
///</summary>
internal sealed class GetStoryPublicForwardsHandler : RpcResultObjectHandler<MyTelegram.Schema.Stats.RequestGetStoryPublicForwards, MyTelegram.Schema.Stats.IPublicForwards>,
    Stats.IGetStoryPublicForwardsHandler
{
    protected override Task<MyTelegram.Schema.Stats.IPublicForwards> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Stats.RequestGetStoryPublicForwards obj)
    {
        throw new NotImplementedException();
    }
}
