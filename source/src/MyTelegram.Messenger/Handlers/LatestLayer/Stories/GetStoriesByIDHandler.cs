namespace MyTelegram.Messenger.Handlers.LatestLayer.Stories;
/// <summary>
/// Obtain full info about a set of <a href="https://corefork.telegram.org/api/stories">stories</a> by their IDs.
/// Possible errors
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 STORIES_NEVER_CREATED This peer hasn't ever posted any stories.
/// 400 STORY_ID_EMPTY You specified no story IDs.
/// <para><c>See <a href="https://corefork.telegram.org/method/stories.getStoriesByID"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetStoriesByIDHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestGetStoriesByID, MyTelegram.Schema.Stories.IStories>
{
    protected override Task<MyTelegram.Schema.Stories.IStories> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Stories.RequestGetStoriesByID obj)
    {
        throw new NotImplementedException();
    }
}