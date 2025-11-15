namespace MyTelegram.Messenger.Handlers.LatestLayer.Stories;
/// <summary>
/// Obtain the list of users that have viewed a specific <a href="https://corefork.telegram.org/api/stories">story we posted</a>
/// Possible errors
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 STORY_ID_INVALID The specified story ID is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/stories.getStoryViewsList"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetStoryViewsListHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestGetStoryViewsList, MyTelegram.Schema.Stories.IStoryViewsList>
{
    protected override Task<MyTelegram.Schema.Stories.IStoryViewsList> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Stories.RequestGetStoryViewsList obj)
    {
        throw new NotImplementedException();
    }
}