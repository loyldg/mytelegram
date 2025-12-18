namespace MyTelegram.Messenger.Handlers.LatestLayer.Stories;
/// <summary>
/// Increment the view counter of one or more stories.
/// Possible errors
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 STORY_ID_EMPTY You specified no story IDs.
/// <para><c>See <a href="https://corefork.telegram.org/method/stories.incrementStoryViews"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class IncrementStoryViewsHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestIncrementStoryViews, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Stories.RequestIncrementStoryViews obj)
    {
        throw new NotImplementedException();
    }
}