namespace MyTelegram.Messenger.Handlers.LatestLayer.Stories;

///<summary>
/// See <a href="https://corefork.telegram.org/method/stories.getPeerMaxIDs" />
///</summary>
internal sealed class GetPeerMaxIDsHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestGetPeerMaxIDs, TVector<int>>
{
    private readonly ILogger<GetPeerMaxIDsHandler> _logger;

    public GetPeerMaxIDsHandler(ILogger<GetPeerMaxIDsHandler> logger)
    {
        _logger = logger;
    }

    protected override Task<TVector<int>> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Stories.RequestGetPeerMaxIDs obj)
    {
        return Task.FromResult(new TVector<int>(obj.Id.Select(p => 0)));
    }
}
