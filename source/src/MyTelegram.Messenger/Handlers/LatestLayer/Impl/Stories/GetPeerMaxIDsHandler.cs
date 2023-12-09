// ReSharper disable All

namespace MyTelegram.Handlers.Stories;

///<summary>
/// See <a href="https://corefork.telegram.org/method/stories.getPeerMaxIDs" />
///</summary>
internal sealed class GetPeerMaxIDsHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestGetPeerMaxIDs, TVector<int>>,
    Stories.IGetPeerMaxIDsHandler
{
    protected override Task<TVector<int>> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Stories.RequestGetPeerMaxIDs obj)
    {
        return Task.FromResult(new TVector<int>());
    }
}