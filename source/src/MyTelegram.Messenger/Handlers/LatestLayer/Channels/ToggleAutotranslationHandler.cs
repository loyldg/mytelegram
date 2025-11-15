namespace MyTelegram.Messenger.Handlers.LatestLayer.Channels;
/// <summary>
/// Toggle autotranslation in a channel, for all users: see <a href="https://corefork.telegram.org/api/translation#autotranslation-for-channels">here »</a> for more info.
/// Possible errors
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/channels.toggleAutotranslation"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class ToggleAutotranslationHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestToggleAutotranslation, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Channels.RequestToggleAutotranslation obj)
    {
        return Task.FromResult<MyTelegram.Schema.IUpdates>(new TUpdates { Chats = [], Updates = [], Users = [], Date = CurrentDate });
    }
}