namespace MyTelegram.Messenger.Handlers.LatestLayer.Stories;
/// <summary>
/// Hide the active stories of a user, preventing them from being displayed on the action bar on the homescreen, see <a href="https://corefork.telegram.org/api/stories#hiding-stories-of-other-users">here »</a> for more info.
/// Possible errors
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/stories.togglePeerStoriesHidden"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class TogglePeerStoriesHiddenHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestTogglePeerStoriesHidden, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Stories.RequestTogglePeerStoriesHidden obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}