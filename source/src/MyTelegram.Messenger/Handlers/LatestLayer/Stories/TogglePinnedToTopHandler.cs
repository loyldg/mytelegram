namespace MyTelegram.Messenger.Handlers.LatestLayer.Stories;
/// <summary>
/// Pin some stories to the top of the profile, see <a href="https://corefork.telegram.org/api/stories#pinned-or-archived-stories">here »</a> for more info.
/// Possible errors
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 STORY_ID_INVALID The specified story ID is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/stories.togglePinnedToTop"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class TogglePinnedToTopHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestTogglePinnedToTop, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Stories.RequestTogglePinnedToTop obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}