// ReSharper disable All

namespace MyTelegram.Handlers.Stories;

///<summary>
/// See <a href="https://corefork.telegram.org/method/stories.getBoostsStatus" />
///</summary>
internal sealed class GetBoostsStatusHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestGetBoostsStatus, MyTelegram.Schema.Stories.IBoostsStatus>,
    Stories.IGetBoostsStatusHandler
{
    protected override Task<MyTelegram.Schema.Stories.IBoostsStatus> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Stories.RequestGetBoostsStatus obj)
    {
        throw new NotImplementedException();
    }
}
