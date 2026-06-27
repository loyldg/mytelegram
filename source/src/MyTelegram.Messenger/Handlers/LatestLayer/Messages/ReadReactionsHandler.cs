namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Mark <a href="https://corefork.telegram.org/api/reactions">message reactions »</a> as read
/// Possible errors
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.readReactions"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class ReadReactionsHandler(IPtsHelper ptsHelper, IPeerHelper peerHelper, IQueryProcessor queryProcessor) : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestReadReactions, MyTelegram.Schema.Messages.IAffectedHistory>
{
    private readonly IQueryProcessor _queryProcessor = queryProcessor;
    protected override async Task<MyTelegram.Schema.Messages.IAffectedHistory> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestReadReactions obj)
    {
        var peer = peerHelper.GetPeer(obj.Peer, input.UserId);
        return new TAffectedHistory
        {
            Pts = ptsHelper.GetCachedPts(peer.PeerId),
            PtsCount = 0
        };
    }
}