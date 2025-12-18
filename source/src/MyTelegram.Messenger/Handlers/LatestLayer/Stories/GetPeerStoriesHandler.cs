namespace MyTelegram.Messenger.Handlers.LatestLayer.Stories;
/// <summary>
/// Fetch the full active <a href="https://corefork.telegram.org/api/stories#watching-stories">story list</a> of a specific peer.
/// Possible errors
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/stories.getPeerStories"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetPeerStoriesHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestGetPeerStories, MyTelegram.Schema.Stories.IPeerStories>
{
    protected override Task<MyTelegram.Schema.Stories.IPeerStories> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Stories.RequestGetPeerStories obj)
    {
        return Task.FromResult<MyTelegram.Schema.Stories.IPeerStories>(new MyTelegram.Schema.Stories.TPeerStories { Stories = new TPeerStories { Stories = new(), Peer = obj.Peer.ToPeer().ToPeer(), }, Chats = new(), Users = new() });
    }
}