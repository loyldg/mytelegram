namespace MyTelegram.Messenger.Handlers.LatestLayer.Stories;
/// <summary>
/// Get the IDs of the maximum read stories for a set of peers.
/// <para><c>See <a href="https://corefork.telegram.org/method/stories.getPeerMaxIDs"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetPeerMaxIDsHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestGetPeerMaxIDs, TVector<int>>
{
    private readonly ILogger<GetPeerMaxIDsHandler> _logger;
    public GetPeerMaxIDsHandler(ILogger<GetPeerMaxIDsHandler> logger)
    {
        _logger = logger;
    }

    protected override Task<TVector<int>> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Stories.RequestGetPeerMaxIDs obj)
    {
        return Task.FromResult(new TVector<int>(obj.Id.Select(p => 0)));
    }
}