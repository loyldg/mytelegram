using MyTelegram.Schema.Stories;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Stories;
/// <summary>
/// Fetch the <a href="https://corefork.telegram.org/api/stories#pinned-or-archived-stories">stories</a> pinned on a peer's profile.
/// Possible errors
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 USER_ID_INVALID The provided user ID is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/stories.getPinnedStories"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetPinnedStoriesHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestGetPinnedStories, MyTelegram.Schema.Stories.IStories>
{
    protected override Task<MyTelegram.Schema.Stories.IStories> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Stories.RequestGetPinnedStories obj)
    {
        return Task.FromResult<MyTelegram.Schema.Stories.IStories>(new TStories { Stories = new(), Chats = new(), Users = new() });
    }
}