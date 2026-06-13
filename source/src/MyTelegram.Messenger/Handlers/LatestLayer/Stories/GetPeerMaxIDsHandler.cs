namespace MyTelegram.Messenger.Handlers.LatestLayer.Stories;
/// <summary>
/// Get the IDs of the maximum read stories for a set of peers.
/// <para><c>See <a href="https://corefork.telegram.org/method/stories.getPeerMaxIDs"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetPeerMaxIDsHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestGetPeerMaxIDs, TVector<MyTelegram.Schema.IRecentStory>>
{
    protected override Task<TVector<MyTelegram.Schema.IRecentStory>> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Stories.RequestGetPeerMaxIDs obj)
    {
        return Task.FromResult(new TVector<IRecentStory>(obj.Id.Select(p => new TRecentStory())));
    }
}