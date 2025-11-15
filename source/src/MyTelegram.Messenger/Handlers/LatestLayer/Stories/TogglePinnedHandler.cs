namespace MyTelegram.Messenger.Handlers.LatestLayer.Stories;
/// <summary>
/// Pin or unpin one or more stories
/// Possible errors
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/stories.togglePinned"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class TogglePinnedHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestTogglePinned, TVector<int>>
{
    protected override Task<TVector<int>> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Stories.RequestTogglePinned obj)
    {
        return Task.FromResult<TVector<int>>([]);
    }
}