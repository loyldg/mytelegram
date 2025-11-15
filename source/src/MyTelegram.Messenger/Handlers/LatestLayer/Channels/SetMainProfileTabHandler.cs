namespace MyTelegram.Messenger.Handlers.LatestLayer.Channels;
/// <summary>
/// Changes the main profile tab of a channel, see <a href="https://corefork.telegram.org/api/profile#tabs">here »</a> for more info.
/// Possible errors
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/channels.setMainProfileTab"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class SetMainProfileTabHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestSetMainProfileTab, IBool>, IObjectHandler
{
    protected override async Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Channels.RequestSetMainProfileTab obj)
    {
        return new TBoolTrue();
    }
}