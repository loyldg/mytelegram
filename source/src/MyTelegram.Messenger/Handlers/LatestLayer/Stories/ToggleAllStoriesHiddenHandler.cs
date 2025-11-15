namespace MyTelegram.Messenger.Handlers.LatestLayer.Stories;
/// <summary>
/// Hide the active stories of a specific peer, preventing them from being displayed on the action bar on the homescreen.
/// <para><c>See <a href="https://corefork.telegram.org/method/stories.toggleAllStoriesHidden"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class ToggleAllStoriesHiddenHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestToggleAllStoriesHidden, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Stories.RequestToggleAllStoriesHidden obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}