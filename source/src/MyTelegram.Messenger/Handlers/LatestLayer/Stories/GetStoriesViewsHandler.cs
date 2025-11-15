namespace MyTelegram.Messenger.Handlers.LatestLayer.Stories;
/// <summary>
/// Obtain info about the view count, forward count, reactions and recent viewers of one or more <a href="https://corefork.telegram.org/api/stories">stories</a>.
/// Possible errors
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 STORY_ID_EMPTY You specified no story IDs.
/// <para><c>See <a href="https://corefork.telegram.org/method/stories.getStoriesViews"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetStoriesViewsHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestGetStoriesViews, MyTelegram.Schema.Stories.IStoryViews>
{
    protected override Task<MyTelegram.Schema.Stories.IStoryViews> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Stories.RequestGetStoriesViews obj)
    {
        throw new NotImplementedException();
    }
}