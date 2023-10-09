// ReSharper disable All

namespace MyTelegram.Handlers.Stories;

///<summary>
/// See <a href="https://corefork.telegram.org/method/stories.getBoostersList" />
///</summary>
internal sealed class GetBoostersListHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestGetBoostersList, MyTelegram.Schema.Stories.IBoostersList>,
    Stories.IGetBoostersListHandler
{
    protected override Task<MyTelegram.Schema.Stories.IBoostersList> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Stories.RequestGetBoostersList obj)
    {
        throw new NotImplementedException();
    }
}
