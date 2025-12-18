namespace MyTelegram.Messenger.Handlers.LatestLayer.Stories;
/// <summary>
/// Mark all stories up to a certain ID as read, for a given peer; will emit an <a href="https://corefork.telegram.org/constructor/updateReadStories">updateReadStories</a> update to all logged-in sessions.
/// Possible errors
/// Code Type Description
/// 400 MAX_ID_INVALID The provided max ID is invalid.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 STORIES_NEVER_CREATED This peer hasn't ever posted any stories.
/// <para><c>See <a href="https://corefork.telegram.org/method/stories.readStories"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class ReadStoriesHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestReadStories, TVector<int>>
{
    protected override Task<TVector<int>> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Stories.RequestReadStories obj)
    {
        throw new NotImplementedException();
    }
}